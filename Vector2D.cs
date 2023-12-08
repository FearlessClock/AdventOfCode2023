using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Vector2D
    {
        public int x, y;

        public Vector2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            Vector2D other = (Vector2D)obj;

            return other.x.Equals(x) && other.y.Equals(y);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format($"({x}:{y})");
        }
    }
}
