using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIGE
{
    public struct HUD
    {
        public string left;
        public string center;
        public string right;

        public HUD(string left, string center, string right)
        {
            this.left = left;
            this.center = center;
            this.right = right;
        }

        public void Update(string update, int placement)
        {
            if (placement == 0) { left = update; }
            else if (placement == 1) { center = update; }
            else { right = update; }
        }

        public void Update(string left, string center, string right)
        {
            this.left = left;
            this.center = center;
            this.right = right;
        }
    }
}
