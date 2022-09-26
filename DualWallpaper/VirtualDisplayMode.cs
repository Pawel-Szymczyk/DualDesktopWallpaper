using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualWallpaper
{
    public class VirtualDisplayMode
    {
        private int Key { get; set; }
        private string Value { get; set; }

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
