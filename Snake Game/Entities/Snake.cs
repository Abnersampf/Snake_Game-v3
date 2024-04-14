namespace Snake_Game.Entities
{
    internal class Snake
    {
        // "Parts" is the snake, it's a list with all the body parts of the snake.
        public List<BodyPart> Parts { get; set; } = [new BodyPart(Game.Center.X, Game.Center.Y)];
        public BodyPart LastPart { get; set; } = new BodyPart(Game.Center.X, Game.Center.Y);
        public int PartsLastIndex { get; set; } = 0;

        public Snake()
        {
            Game.DrawObject(Parts[0].X, Parts[0].Y, ConsoleColor.DarkMagenta);
        }

        public void MoveParts(ConsoleKey key)
        {
            int x = Parts[0].X, y = Parts[0].Y;

            // Defines where the head should move to
            if (key == ConsoleKey.UpArrow)
            {
                y = Parts[0].Y - 1;
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                x = Parts[0].X - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                y = Parts[0].Y + 1;
            }
            else
            {
                x = Parts[0].X + 1;
            }

            LastPart.X = Parts[PartsLastIndex].X;
            LastPart.Y = Parts[PartsLastIndex].Y;

            // Deletes the last part of the snake
            Game.DrawObject(Parts[PartsLastIndex].X, Parts[PartsLastIndex].Y, ConsoleColor.Black);
            Parts.Remove(Parts[PartsLastIndex]);

            // Teleports the snake
            if (y == -1)
            {
                y = Game.GameHeight - 1;
            }
            else if (x == -1)
            {
                x = Game.GameWidth - 1;
            }
            else if (y == Game.GameHeight)
            {
                y = 0;
            }
            else if (x == Game.GameWidth)
            {
                x = 0;
            }

            // Adds a new part to the snake, in front of the head
            Parts.Insert(0, new BodyPart(x, y));
            Game.DrawObject(Parts[0].X, Parts[0].Y, ConsoleColor.DarkMagenta);
        }

        public void AddPart()
        {
            PartsLastIndex++;

            // Adds a new part to the snake, at the last position
            Parts.Add(new BodyPart(LastPart.X, LastPart.Y));
            Game.DrawObject(LastPart.X, LastPart.Y, ConsoleColor.DarkMagenta);
        }
    }
}
