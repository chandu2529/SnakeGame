using SnakeGame;

Coord gridDimentions = new Coord(50, 20);
Coord snakePos  = new Coord(10, 1);
Random rand = new Random();
Coord applePos = new Coord(rand.Next(1, gridDimentions.X-1), rand.Next(1, gridDimentions.Y-1));
int frameDelayMilli = 100;
Direction movementDirection = Direction.Down;

List<Coord> SnakePosHistory = new List<Coord>();
int tailLength = 1;
int score = 0;

while (true)
{
    Console.Clear();
    Console.WriteLine("score : " + score);
    snakePos.ApplyMovementDirection(movementDirection);

    for (int y = 0; y < gridDimentions.Y; y++)
    {
        for (int x = 0; x < gridDimentions.X; x++)
        {
            Coord CurrentCoord = new Coord(x, y);

            if (snakePos.Equals(CurrentCoord) || (SnakePosHistory.Contains(CurrentCoord)))
            {
                Console.Write("■");
            }
            else if (applePos.Equals(CurrentCoord))
            {
                Console.Write("a");
            }
            else if (x == 0 || y == 0 || x == gridDimentions.X - 1 || y == gridDimentions.Y - 1)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(" ");
            }

            
        }
        Console.WriteLine(" ");
    }

    if (snakePos.Equals(applePos))
    {
        tailLength++;
        applePos = new Coord(rand.Next(1, gridDimentions.X - 1), rand.Next(1, gridDimentions.Y - 1));
        score++;
    }
    else if (snakePos.X == 0 || snakePos.Y == 0 || snakePos.X == gridDimentions.X - 1 || snakePos.Y == gridDimentions.Y - 1 || SnakePosHistory.Contains(snakePos))
    {
        tailLength = 1;
        score = 0;
        snakePos = new Coord(10, 1);
        SnakePosHistory.Clear();
        movementDirection = Direction.Down;
        continue;
    }



    SnakePosHistory.Add(new Coord(snakePos.X, snakePos.Y));
    if (SnakePosHistory.Count > tailLength)
    {
        SnakePosHistory.RemoveAt(0);
    }

    
    DateTime time = DateTime.Now;

    while ((DateTime.Now - time).Milliseconds < frameDelayMilli)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    movementDirection = Direction.Left; break;
                case ConsoleKey.RightArrow:
                    movementDirection = Direction.Right; break;
                case ConsoleKey.UpArrow:
                    movementDirection = Direction.Up; break;
                case ConsoleKey.DownArrow:
                    movementDirection = Direction.Down; break;
            }
        }
    }
    

}

