using System.ComponentModel.DataAnnotations;

namespace Vnit.ApplicationCore.Enums
{
    public enum UserRegistrationStatus
    {
        [Display(Name = "Đăng ký thành công")]
        Success,
        [Display(Name = "Thất bại. Tài khoản đã tồn tại")]
        FailedAsEmailAlreadyExists
    }
}
