using System.ComponentModel;

namespace Vnit.ApplicationCore.Entities.Security
{
    public enum LoginStatus
    {
        [Description("Thành công")]
        Success,
        [Description("Tài khoản chưa kích hoạt")]
        FailedInactiveUser,
        [Description("Tài khoản không có quyền truy cập")]
        FailedAdminUser,
        [Description("Tài khoản đã xóa")]
        FailedDeletedUser,
        [Description("Tài khoản không tồn tại")]
        FailedUserNotExists,
        [Description("Thất bại")]
        Failed
    }
}
