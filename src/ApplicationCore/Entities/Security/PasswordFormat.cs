using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Security
{
    /// <summary>
    /// Thuật toán mã hóa
    /// </summary>
    public enum PasswordFormat
    {
        Md5Hashed = 1,
        Sha1Hashed = 2,
        Sha256Hashed = 3,
    }
}
