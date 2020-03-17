using Vnit.ApplicationCore.Interfaces;
using Vnit.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vnit.Api.ViewModels.Account;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Vnit.Api.Controllers.Api
{
    
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBasketService _basketService;
        private readonly IAppLogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IBasketService basketService,
            IAppLogger<AccountController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _basketService = basketService;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// SignIn
        /// FromBody attribute is for JSON/XML
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        // POST: /Account/SignIn
        [HttpPost("SignIn")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([FromBody] LoginViewModel model, [FromRoute] string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return RespondFailure(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
            }
            if (result.Succeeded)
            {
                string anonymousBasketId = Request.Cookies[Constants.BASKET_COOKIENAME];
                if (!String.IsNullOrEmpty(anonymousBasketId))
                {
                    await _basketService.TransferBasketAsync(anonymousBasketId, model.Email);
                    Response.Cookies.Delete(Constants.BASKET_COOKIENAME);
                }
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, model.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken
                (
                    issuer: _configuration["Token:Issuer"],
                    audience: _configuration["Token:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(60),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                        SecurityAlgorithms.HmacSha256)
                );

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with email '{model.Email}'.");
                }
               
                return RespondSuccess(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        result,
                        user
                });
            }
            VerboseReporter.ReportError(string.Empty, "Invalid login attempt.");
            return RespondFailure(model);
        }
      

        [HttpPost("LoginWith2fa")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return RespondFailure(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RespondSuccess(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RespondFailure("Lockout");
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return RespondFailure();
            }
        }

       
        [HttpPost("SignOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RespondSuccess("Catalog");
        }

      
        [HttpPost("Register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    var emailClaim = new Claim("email", user.Email.ToString(), ClaimValueTypes.String);
                    await _userManager.AddClaimAsync(user, emailClaim);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RespondSuccess(returnUrl);
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            return RespondFailure(model);
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return RedirectToAction(nameof(CatalogController.Index), "Catalog");
        //    }
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        throw new ApplicationException($"Unable to load user with ID '{userId}'.");
        //    }
        //    var result = await _userManager.ConfirmEmailAsync(user, code);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult ResetPassword(string code = null)
        //{
        //    if (code == null)
        //    {
        //        throw new ApplicationException("A code must be supplied for password reset.");
        //    }
        //    var model = new ResetPasswordViewModel { Code = code };
        //    return View(model);
        //}

        //private IActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction(nameof(CatalogController.Index), "Catalog");
        //    }
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
