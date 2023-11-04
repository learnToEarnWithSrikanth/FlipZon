using System;
namespace FlipZon.Utilities
{
    public class PlatformConfig
    {
        public static PlatformConfig Instance { get; } = new PlatformConfig();
        private PlatformConfig()
        {
        }

        public object ParentWindow { get; set; }
    }
}

