using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.MediaAggregate
{
    /// <summary>
    /// Picture Size
    /// </summary>
    public class PictureSize : IDisposable
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public string Name { get; set; }

        public void Dispose()
        {

        }
    }
}
