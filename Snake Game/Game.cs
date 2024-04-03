using Snake_Game.Entities;

internal class Game
{
    // Properties of the Game class
    public static int GameWidth { get; set; } = Console.WindowWidth;
    public static int GameHeight { get; set; } = Console.WindowHeight;
    public static Point Center { get; set; } = new Point(GameWidth / 2, GameHeight / 2);
    public static int Score { get; set; } = 0;
    public static int MaxPossibleScore { get; set; } = (GameWidth * GameHeight) - 1;
    public static int MaxScoreAchieved { get; set; } = 0;
    public static bool Win { get; set; } = false;
    public static bool ContinueGame { get; set; } = true;

    // Draws an object on the screen at a specific position
    public static void DrawObject(int x, int y, string obj)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(obj);
    }

    // Gets and sets the new dimensions of the screen if the user resizes the window
    public static void SetGameDimensions()
    {
        GameWidth = Console.WindowWidth;
        GameHeight = Console.WindowHeight;

        Center.X = GameWidth / 2;
        Center.Y = GameHeight / 2;
    }

    public static void EndGame()
    {
        Thread.Sleep(1000);

        Console.Clear();

        // Positions the cursor at a specific location on the screen to display a message
        Console.SetCursorPosition(Center.X - 4, Center.Y - 1);

        if (Win)
        {
            Console.Write($"You Win!");

            // Resets the score
            Win = false;
            Score = 0;
            MaxScoreAchieved = 0;
        }
        else
        {
            Console.Write($"You Lose");
        }

        // Positions the cursor at a specific location on the screen to display a message
        Console.SetCursorPosition(Center.X - 19, Center.Y);
        Console.Write($"Score: {Score} | Max Score Achieved: {MaxScoreAchieved}");

        Console.SetCursorPosition(Center.X - 9, Center.Y + 1);
        Console.Write($"Play agin? (y/n): ");

        char key = Console.ReadKey(true).KeyChar;

        // Waits until the user enters a valid option
        while (key != 'y' && key != 'n')
        {
            key = Console.ReadKey(true).KeyChar;
        }

        ContinueGame = (key == 'y') ? true : false;

        Console.Clear();
    }

    public static void Main(string[] args)
    {
        while (ContinueGame)
        {
            // Instantiating/creating variables
            Snake snake = new Snake();
            Food food = new Food(snake);

            ConsoleKey key = ConsoleKey.RightArrow, lastPressedKey = key;

            Console.CursorVisible = false;
            Score = 0;

            while (true)
            {
                SetGameDimensions();

                // Blocks the snake from moving backwards
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow && lastPressedKey == ConsoleKey.DownArrow)
                    {
                        key = ConsoleKey.DownArrow;
                    }
                    else if (key == ConsoleKey.LeftArrow && lastPressedKey == ConsoleKey.RightArrow)
                    {
                        key = ConsoleKey.RightArrow;
                    }
                    else if (key == ConsoleKey.DownArrow && lastPressedKey == ConsoleKey.UpArrow)
                    {
                        key = ConsoleKey.UpArrow;
                    }
                    else if (key == ConsoleKey.RightArrow && lastPressedKey == ConsoleKey.LeftArrow)
                    {
                        key = ConsoleKey.LeftArrow;
                    }

                    lastPressedKey = key;
                }

                // Moves all parts of the snake
                snake.MoveParts(key);

                // Checks if the snake has found the apple
                if (snake.Parts[0].X == food.X && snake.Parts[0].Y == food.Y)
                {
                    Score++;

                    if (Score == MaxPossibleScore)
                    {
                        Win = true;
                        break;
                    }

                    snake.AddPart();

                    food.RandomPosition(snake);
                }

                // Checks if the snake has collided with itself
                if (snake.Parts.Exists(p => p != snake.Parts[0]
                && p.X == snake.Parts[0].X && p.Y == snake.Parts[0].Y))
                {
                    if (Score > MaxScoreAchieved)
                    {
                        MaxScoreAchieved = Score;
                    }

                    break;
                }

                // Controls the speed of the snake's movement
                Thread.Sleep(100);
            }

            EndGame();
        }
    }
}