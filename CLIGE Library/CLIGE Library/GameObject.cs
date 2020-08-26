using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIGE
{
    public class GameObject : Game
    {
        public int layer;
        public int height;
        public int width;
        public Sprite sprite;
        public Position position;
        public Vector direction;
        public Ticker actionDelay;

        public GameObject(int layer, Position position, string texture, int backgroundColor, int foregroundColor)
        {
            height = 1;
            width = 1;
            this.layer = layer;
            this.position = position;
            sprite = new Sprite(texture, backgroundColor, foregroundColor);
            direction = new Vector(0, 0, 0);
            actionDelay = new Ticker();
            objects[layer].Add(this);
        }

        public GameObject(int height, int width, int layer, Position position, string texture, int backgroundColor, int foregroundColor)
        {
            this.height = height;
            this.width = width;
            this.layer = layer;
            this.position = position;
            sprite = new Sprite(texture, height, width, backgroundColor, foregroundColor);
            direction = new Vector(0, 0, 0);
            actionDelay = new Ticker();
            objects[layer].Add(this);
        }

        public GameObject(int height, int width, int layer, Position position, string[][] texture, int backgroundColor, int foregroundColor)
        {
            this.height = height;
            this.width = width;
            this.layer = layer;
            this.position = position;
            sprite = new Sprite(texture, height, width, backgroundColor, foregroundColor);
            direction = new Vector(0, 0, 0);
            actionDelay = new Ticker();
            objects[layer].Add(this);
        }

        public GameObject(GameObject source)
        {
            sprite = source.sprite;
            position = source.position;
            direction = source.direction;
            actionDelay = new Ticker(source.actionDelay);
        }

        public void ChangeDirection(int direction)
        {
            if (direction == 0) { this.direction = new Vector(-1, 0); }
            else if (direction == 1) { this.direction = new Vector(0, 1); }
            else if (direction == 2) { this.direction = new Vector(1, 0); }
            else if (direction == 3) { this.direction = new Vector(0, -1); }
        }

        public void ChangeDirection(ConsoleKey input)
        {
            if (input == ConsoleKey.UpArrow || input == ConsoleKey.W) { direction = new Vector(-1, 0); }
            else if (input == ConsoleKey.RightArrow || input == ConsoleKey.D) { direction = new Vector(0, 1); }
            else if (input == ConsoleKey.DownArrow || input == ConsoleKey.S) { direction = new Vector(1, 0); }
            else if (input == ConsoleKey.LeftArrow || input == ConsoleKey.A) { direction = new Vector(0, -1); }
        }

        public bool Move()
        {
            bool canMove = false;

            if (actionDelay.Check() && position.r + direction.r >= 0 && position.r + height + direction.r < grid.Length && position.c + direction.c >= 0 && position.c + width + direction.c < grid[0].Length)
            {
                canMove = true;
                position.Update(direction);

                if (layer == 0)
                {
                    if (Window.CheckEdge(position, height, width, direction))
                    {
                        Window.Move(direction);
                    }

                    direction.Stop();
                }
            }

            return canMove;
        }

        public void Delete()
        {
            Window.Clear(position);
            objects[layer].Remove(this);
        }
    }
}
