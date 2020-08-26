using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIGE
{
    public struct Position
    {
        public int r;
        public int c;

        public Position(string location)
        {
            if (location.ToLower() == "center")
            {
                r = Game.grid.Length / 2;
                c = Game.grid[0].Length / 2;
            }
            else
            {
                r = 0;
                c = 0;
            }
        }

        public Position(int row, int col)
        {
            r = row;
            c = col;
        }

        public void Update(Vector direction)
        {
            r += direction.r;
            c += direction.c;
        }

        public static bool operator ==(Position pos1, Position pos2)
        {
            return pos1.Equals(pos2);
        }
        public static bool operator !=(Position pos1, Position pos2)
        {
            return !pos1.Equals(pos2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
