using System;
using System.Collections.Generic;

namespace DualWallpaper
{
    public class VirtualDisplayMode
    {
        private int Key { get; set; }
        private string Value { get; set; }

        public VirtualDisplayMode()
        {
            this.Key = 0;
            this.Value = String.Empty;
        }

        private List<VirtualDisplayMode> ModesList()
        {
            var list = new List<VirtualDisplayMode>()
            {
                new VirtualDisplayMode() { Key = 0, Value = "Split" },
                new VirtualDisplayMode() { Key = 1, Value = "Merge" },
            };

            return list;
        }
    }
}
