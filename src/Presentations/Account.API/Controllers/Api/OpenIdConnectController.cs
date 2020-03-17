using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vnit.Api.Controllers.Api
{
    //Next, it’s necessary to register OpenIddict types in our ConfigureServices method in our Startup type.This can be done with a call like this:

    //services.AddOpenIddict<ApplicationDbContext>()
    //.AddMvcBinders()
    //.EnableTokenEndpoint("/connect/token")
    //.UseJsonWebTokens()
    //.AllowPasswordFlow()
    //.AddSigningCertificate(jwtSigningCert);

    // applicationSettings.json
    //    {
    //    "issuer": "http://localhost:5000/",
    //    "jwks_uri": "http://localhost:5000/.well-known/jwks",
    //    "token_endpoint": "http://localhost:5000/connect/token",
    //    "code_challenge_methods_supported": [ "S256" ],
    //    "grant_types_supported": [ "password" ],
    //    "subject_types_supported": [ "public" ],
    //    "scopes_supported": [ "openid", "profile", "email", "phone", "roles" ],
    //    "id_token_signing_alg_values_supported": [ "RS256" ]
    //}
    //Request

    //POST /connect/token HTTP/1.1
    //Host: localhost:5000
    //Cache-Control: no-cache
    //Postman-Token: f1bb8681-a963-2282-bc94-03fdaea5da78
    //Content-Type: application/x-www-form-urlencoded

    //grant_type = password & username = Mike % 40Fabrikam.com&password=MikePassword1!&scope=openid+email+name+profile+roles

    /// <summary>
    /// Configure OpenIddict
    /// </summary>
    public class OpenIdConnectController : BaseApiController
    {
        //    /// <summary>
        //    /// OpenIdConnect/Token 
        //    /// </summary>
        //    /// <param name="request"></param>
        //    /// <returns></returns>
        //    [HttpPost]
        //    public async Task<IActionResult> Token(OpenIdConnectRequest request)
        //    {
        //        if (!request.IsPasswordGrantType())
        //        {
        //            // Return bad request if the request is not for password grant type
        //            return BadRequest(new OpenIdConnectResponse
        //            {
        //                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
        //                ErrorDescription = "The specified grant type is not supported."
        //            });
        //        }

        //        var user = await _userManager.FindByNameAsync(request.Username);
        //        if (user == null)
        //        {
        //            // Return bad request if the user doesn't exist
        //            return BadRequest(new OpenIdConnectResponse
        //            {
        //                Error = OpenIdConnectConstants.Errors.InvalidGrant,
        //                ErrorDescription = "Invalid username or password"
        //            });
        //        }

        //        // Check that the user can sign in and is not locked out.
        //        // If two-factor authentication is supported, it would also be appropriate to check that 2FA is enabled for the user
        //        if (!await _signInManager.CanSignInAsync(user) || (_userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(user)))
        //        {
        //            // Return bad request is the user can't sign in
        //            return BadRequest(new OpenIdConnectResponse
        //            {
        //                Error = OpenIdConnectConstants.Errors.InvalidGrant,
        //                ErrorDescription = "The specified user cannot sign in."
        //            });
        //        }

        //        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        //        {
        //            // Return bad request if the password is invalid
        //            return BadRequest(new OpenIdConnectResponse
        //            {
        //                Error = OpenIdConnectConstants.Errors.InvalidGrant,
        //                ErrorDescription = "Invalid username or password"
        //            });
        //        }

        //        // The user is now validated, so reset lockout counts, if necessary
        //        if (_userManager.SupportsUserLockout)
        //        {
        //            await _userManager.ResetAccessFailedCountAsync(user);
        //        }

        //        // Create the principal
        //        var principal = await _signInManager.CreateUserPrincipalAsync(user);

        //        // Claims will not be associated with specific destinations by default, so we must indicate whether they should
        //        // be included or not in access and identity tokens.
        //        foreach (var claim in principal.Claims)
        //        {
        //            // For this sample, just include all claims in all token types.
        //            // In reality, claims' destinations would probably differ by token type and depending on the scopes requested.
        //            claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken, OpenIdConnectConstants.Destinations.IdentityToken);
        //        }

        //        // Create a new authentication ticket for the user's principal
        //        var ticket = new AuthenticationTicket(
        //            principal,
        //            new AuthenticationProperties(),
        //            OpenIdConnectServerDefaults.AuthenticationScheme);

        //        // Include resources and scopes, as appropriate
        //        var scope = new[]
        //        {
        //    OpenIdConnectConstants.Scopes.OpenId,
        //    OpenIdConnectConstants.Scopes.Email,
        //    OpenIdConnectConstants.Scopes.Profile,
        //    OpenIdConnectConstants.Scopes.OfflineAccess,
        //    OpenIddictConstants.Scopes.Roles
        //}.Intersect(request.GetScopes());

        //        ticket.SetResources("http://localhost:5000/");
        //        ticket.SetScopes(scope);

        //        // Sign in the user
        //        return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        //    }
    }
}
