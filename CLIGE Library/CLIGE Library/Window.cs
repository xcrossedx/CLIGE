using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CLIGE
{
    public class Window : Game
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int MAXIMIZE = 3;

        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_SIZE = 0xF000;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_RESTORE = 0xF120;

        public static ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
        public static Vector[] directions = new Vector[4] { new Vector(-1, 0, 1), new Vector(0, 1, 1), new Vector(1, 0, 1), new Vector(0, -1, 1) };

        public static List<Bulletin> bulletins;

        public static Position _position;

        public static Ticker frameBuffer;

        public static int _height;
        public static int _width;

        public static void Init(bool soft)
        {
            if (!soft)
            {
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                ShowWindow(GetConsoleWindow(), MAXIMIZE);
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_RESTORE, MF_BYCOMMAND);
            }

            ResetBuffer();
            _height = Console.WindowHeight - 2;
            if (Console.WindowWidth % 2 == 0) { _width = Console.WindowWidth / 2; }
            else { _width = (Console.WindowWidth - 1) / 2; }

            bulletins = new List<Bulletin>();

            _position = new Position((grid.Length - _height) / 2, (grid[0].Length - _width) / 2);

            frameBuffer = new Ticker();
        }

        public static void ResetBuffer()
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
        }

        public static void Update()
        {
            if (frameBuffer.Check())
            {
                Console.SetCursorPosition(0, 1);

                string update = "";

                Sprite lastSprite = new Sprite("  ", 0, 15);

                for (int r = 0; r < _height; r++)
                {
                    for (int c = 0; c < _width; c++)
                    {
                        Position checkPos = new Position(r + _position.r, c + _position.c);

                        Sprite sprite = new Sprite("  ", grid[checkPos.r][checkPos.c], 15);

                        int spriteRow = 0;
                        int spriteCol = 0;

                        for (int l = 0; l < objects.Length; l++)
                        {
                            if (objects[l].Exists(x => checkPos.r >= x.position.r && checkPos.r < x.position.r + x.height && checkPos.c >= x.position.c && checkPos.c < x.position.c + x.width))
                            {
                                GameObject go = objects[l].Find(x => checkPos.r >= x.position.r && checkPos.r < x.position.r + x.height && checkPos.c >= x.position.c && checkPos.c < x.position.c + x.width);
                                sprite = go.sprite;
                                spriteRow = checkPos.r - go.position.r;
                                spriteCol = checkPos.c - go.position.c;
                                break;
                            }
                        }

                        if (!sprite.ColorMatch(lastSprite))
                        {
                            Console.BackgroundColor = colors[lastSprite.bColor];
                            Console.ForegroundColor = colors[lastSprite.fColor];
                            Console.Write(update);
                            update = "";
                        }

                        update += sprite.texture[spriteRow][spriteCol];
                        lastSprite = sprite;
                    }

                    if (r != _height - 1)
                    {
                        update += "\n";
                    }
                }

                Console.BackgroundColor = colors[lastSprite.bColor];
                Console.ForegroundColor = colors[lastSprite.fColor];
                Console.Write(update);

                BulletinUpdate();
            }
        }

        public static bool CheckEdge(Position position, int height, int width, Vector direction)
        {
            bool atEdge = false;

            if ((position.r - _position.r == 4 && direction == directions[0]) || (position.r + height - _position.r == _height - 5 && direction == directions[2]) || (position.c - _position.c == 4 && direction == directions[3]) || (position.c + width - _position.c == _width - 5 && direction == directions[1]))
            {
                atEdge = true;
            }

            return atEdge;
        }

        public static bool Move(Vector direction)
        {
            bool canMove = true; 

            if (_position.r + direction.r >= 0 && _position.r + direction.r < grid.Length - _height && _position.c + direction.c >= 0 && _position.c + direction.c < grid[0].Length - _width)
            {
                canMove = false;
                _position.Update(direction);
            }

            return canMove;
        }

        public static void HUDUpdate(HUD hud)
        {
            if (hud.left.Length + hud.center.Length + hud.right.Length + 2 <= _width)
            {
                Console.SetCursorPosition(0, 0);

                string buffer = "";

                for (int b = 0; b < (_width - ((hud.left.Length + hud.center.Length + hud.right.Length) / 2)) / 4; b++) { buffer += "  "; }

                Console.Write(buffer + hud.left + buffer + hud.center + buffer + hud.right + buffer);
            }
        }

        private static void BulletinUpdate()
        {
            if (bulletins[0].shown)
            {
                if (DateTime.Now >= bulletins[0].showTime.AddSeconds(3))
                {
                    bulletins.RemoveAt(0);
                    ClearBulletin();
                }
            }
            else if (bulletins[0].textA.Length + bulletins[0].highlight.Length + bulletins[0].textB.Length + 2 < _width)
            {
                bulletins[0].Show();

                int buffer = (_width - (bulletins[0].textA.Length / 2)) / 2;

                if (bulletins[0].highlight != "")
                {
                    buffer = (_width - ((bulletins[0].textA.Length + bulletins[0].highlight.Length + bulletins[0].textB.Length + 2) / 2)) / 2;
                }

                Console.SetCursorPosition(buffer, _height + 1);
                Console.BackgroundColor = colors[0];
                Console.ForegroundColor = colors[15];
                Console.Write(bulletins[0].textA);

                if (bulletins[0].highlight != "")
                {
                    Console.ForegroundColor = colors[bulletins[0].highlightColor];
                    Console.Write($" {bulletins[0].highlight} ");
                    Console.ForegroundColor = colors[15];
                    Console.Write(bulletins[0].textB);
                }
            }
            else
            {
                bulletins.RemoveAt(0);
            }
        }

        private static void ClearBulletin()
        {
            string clear = "";
            for (int c = 1; c < _width; c++)
            {
                clear += "  ";
            }
            Console.SetCursorPosition(0, _height + 1);
            Console.BackgroundColor = colors[0];
            Console.Write(clear);
        }

        public static void Clear(bool keepBackground)
        {
            Console.SetCursorPosition(0, 1);

            string clear = "";

            int lastColor = 0;

            for (int r = 0; r < _height; r++)
            {
                for (int c = 0; c < _width; c++)
                {
                    clear += "  ";

                    if (keepBackground && lastColor != grid[_position.r + r][_position.c + c])
                    {
                        Console.BackgroundColor = colors[lastColor];
                        Console.Write(clear);
                        clear = "";
                        lastColor = grid[_position.r + r][_position.c + c];
                    }
                }

                if (r != _height - 1)
                {
                    clear += "\n";
                }
            }

            Console.BackgroundColor = colors[0];
            Console.Write(clear);
        }

        public static void Clear(Position position)
        {
            int viewRow = position.r - position.r;
            int viewCol = position.c - position.c;

            if (viewRow >= 0 && viewRow < _height && viewCol >= 0 && viewCol < _width)
            {
                Console.SetCursorPosition(viewCol * 2, viewRow + 1);
                Console.BackgroundColor = colors[grid[position.r][position.c]];
                Console.Write("  ");
            }
        }

        public static void Clear(Position position, int height, int width)
        {
            int startRow = position.r - _position.r;
            int startCol = position.c - _position.c;

            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    if (startRow + r >= 0 && startRow + r < _height && startCol + c >= 0 && startCol + c < _width)
                    {
                        Console.SetCursorPosition((startCol + c) * 2, (startRow + r) + 1);
                        Console.BackgroundColor = colors[grid[position.r][position.c]];
                        Console.Write("  ");
                    }
                }
            }
        }
    }
}
