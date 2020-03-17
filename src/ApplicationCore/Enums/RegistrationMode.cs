using System.ComponentModel;

namespace Vnit.ApplicationCore.Enums
{
    public enum RegistrationMode
    {
        [Description("Immediate")]
        Immediate,
        [Description("WithActivationEmail")]
        WithActivationEmail,
        [Description("ManualApproval")]
        ManualApproval
    }
}
