using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIGE
{
    public class Game
    {
        public static int[][] grid;
        public static List<GameObject>[] objects;
        public static float updateRate = 0.1f;

        public static void Init(int gameHeight, int gameWidth, int layers)
        {
            if (gameHeight < 200) { gameHeight = 200; }
            if (gameWidth < 200) { gameWidth = 200; }

            InitGrid(gameHeight, gameWidth);
            Window.Init(false);
            objects = new List<GameObject>[layers];
            for (int l = 0; l < layers; l++)
            {
                objects[l] = new List<GameObject>();
            }
        }

        public static void Reset()
        {
            InitGrid(grid.Length, grid[0].Length);
            Window.Init(true);
            for (int l = 0; l < objects.Length; l++)
            {
                objects[l].Clear();
            }
        }

        private static void InitGrid(int height, int width)
        {
            grid = new int[height][];
            for (int r = 0; r < height; r++)
            {
                grid[r] = new int[width];
            }
        }

        public static void Update(int excludedLayers)
        {
            for (int l = excludedLayers; l < objects.Length; l++)
            {
                foreach (GameObject gameObject in objects[l])
                {
                    if (gameObject.direction.v != 0)
                    {
                        gameObject.Move();
                    }
                }
            }
        }
    }
}
