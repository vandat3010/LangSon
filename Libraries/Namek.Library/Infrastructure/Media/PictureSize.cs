using System;

namespace Namek.Library.Infrastructure.Media
{
    public class PictureSize : IDisposable
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public string Name { get; set; }

        public void Dispose() { }
    }
}