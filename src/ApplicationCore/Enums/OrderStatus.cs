using System.ComponentModel;

namespace Vnit.ApplicationCore.Enums
{
    public enum OrderStatus
    {
        [Description("Chờ xử lý")]
        Pending = 1,
        [Description("Đang xử lý")]
        Processing = 2,
        [Description("Đã đóng")]
        Closed = 4,
        [Description("Refunded")]
        Refunded = 8,
        Cancelled
    }
}
