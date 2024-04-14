namespace Snake_Game.Entities
{
    internal class Food : Point
    {
        public Food(Snake snake)
        {
            RandomPosition(snake);
        }

        public Food(int x, int y)
        {
            X = x;
            Y = y;

            Game.DrawObject(X, Y, ConsoleColor.Red);
        }

        public void RandomPosition(Snake snake)
        {
            Random rnd = new Random();

            int x = rnd.Next(0, Game.GameWidth);
            int y = rnd.Next(0, Game.GameHeight);

            // Searches for a random position that is not occupied by the snake
            while (snake.Parts.Exists(p => p.X == x && p.Y == y)
                || X == 1 && Y == 1)
            {
                x = rnd.Next(0, Game.GameWidth);
                y = rnd.Next(0, Game.GameHeight);
            }

            X = x;
            Y = y;

            Game.DrawObject(X, Y, ConsoleColor.Red);
        }
    }
}
