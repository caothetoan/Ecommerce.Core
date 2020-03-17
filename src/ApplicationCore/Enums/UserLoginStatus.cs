using System.ComponentModel.DataAnnotations;

namespace Vnit.ApplicationCore.Enums
{
    public enum UserLoginStatus
    {
        [Display(Name = "Đăng nhập thành công")]
        Success,
        [Display(Name = "Tài khoản chưa kích hoạt")]
        FailedInactiveUser,
        [Display(Name = "Không có quyền truy cập")]
        FailedIsAdmin,
        [Display(Name = "Tài khoản đã bị xóa")]
        FailedDeletedUser,
        [Display(Name = "Tên đăng nhập không đúng")]
        FailedUserNotExists,
        [Display(Name = "Tên đăng nhập hoặc mật khẩu không đúng")]
        UserOrPasswordInCorrect,
        [Display(Name = "Thất bại")]
        Failed
    }
}
