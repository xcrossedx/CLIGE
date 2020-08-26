using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIGE
{
    public class Ticker : Game
    {
        DateTime lastUpdate;

        public Ticker()
        {
            lastUpdate = DateTime.Now;
        }

        public Ticker(Ticker source)
        {
            lastUpdate = source.lastUpdate;
        }

        public bool Check()
        {
            bool result = false;

            if (DateTime.Now >= lastUpdate.AddSeconds(updateRate))
            {
                result = true;
                lastUpdate = DateTime.Now;
            }

            return result;
        }

        public bool Check(int velocity)
        {
            bool result = false;

            if (DateTime.Now >= lastUpdate.AddSeconds(updateRate / velocity))
            {
                result = true;
                lastUpdate = DateTime.Now;
            }

            return result;
        }
    }
}
