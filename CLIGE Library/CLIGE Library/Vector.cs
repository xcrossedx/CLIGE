using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIGE
{
    public struct Vector
    {
        public int r;
        public int c;
        public int v;

        public Vector(int row, int col)
        {
            r = row;
            c = col;
            v = 1;
        }

        public Vector(int row, int col, int velocity)
        {
            r = row;
            c = col;
            v = velocity;
        }

        public void Update(int row, int col)
        {
            r = row;
            c = col;
        }

        public void Update(int row, int col, int velocity)
        {
            r = row;
            c = col;
            v = velocity;
        }

        public void Stop()
        {
            v = 0;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.Equals(v2);
        }
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !v1.Equals(v2);
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
