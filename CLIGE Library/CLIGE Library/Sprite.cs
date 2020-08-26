using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIGE
{
    public struct Sprite
    {
        public string[][] texture;

        public int height;
        public int width;

        public int bColor;
        public int fColor;

        public Sprite(string texture, int backgroundColor, int foregroundColor)
        {
            this.texture = new string[1][] { new string[1] { texture } };

            height = 1;
            width = 1;

            bColor = backgroundColor;
            fColor = foregroundColor;
        }

        public Sprite(string texture, int height, int width, int backgroundColor, int foregroundColor)
        {
            this.texture = new string[height][];

            for (int r = 0; r < height; r++)
            {
                this.texture[r] = new string[width];

                for (int c = 0; c < width; c++)
                {
                    this.texture[r][c] = texture;
                }
            }

            this.height = height;
            this.width = width;

            bColor = backgroundColor;
            fColor = foregroundColor;
        }

        public Sprite(string[][] texture, int height, int width, int backgroundColor, int foregroundColor)
        {
            this.texture = texture;

            this.height = height;
            this.width = width;

            bColor = backgroundColor;
            fColor = foregroundColor;
        }

        public bool ColorMatch(Sprite sprite)
        {
            bool match = false;
            if (bColor == sprite.bColor && fColor == sprite.fColor) { match = true; }
            return match;
        }
    }
}
