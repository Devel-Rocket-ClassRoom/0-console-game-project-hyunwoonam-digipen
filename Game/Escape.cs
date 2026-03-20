using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Escape : GameObject
{
    Random _random = new Random();
    private (int X, int Y) _position;
    public (int X, int Y) Position { get { return _position; } }

    public int CoinCount;
    public Escape(Scene scene) : base(scene)
    {
        Name = "Escape";
        CoinCount = 0;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        if (CoinCount >= 3)
        {
            buffer.SetCell(_position.X + 40, _position.Y + 0, 'E', ConsoleColor.Red);
        }
        else
        {
            buffer.SetCell(_position.X + 40, _position.Y + 0, 'E', ConsoleColor.Yellow);
        }
    }

    public override void Update(float deltaTime)
    {

    }

    public void Spawn(int[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        while (true)
        {
            int x = _random.Next(0, cols);
            int y = _random.Next(0, rows);

            if (grid[y, x] == 0)
            {
                _position = (x, y);
                break;
            }
        }
    }
}
