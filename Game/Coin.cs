using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Coin : GameObject
{
    Random _random = new Random();
    private (int X, int Y) _position;
    public (int X, int Y) Position { get { return _position; } }

    private bool isGetall = false;

    public bool isGetAll
    {
        get { return isGetall; } 
        set { isGetall = value; }
    }
    public Coin(Scene scene) : base(scene)
    {
        Name = "Coin";
    }

    public override void Draw(ScreenBuffer buffer)
    {
        if (isGetall == false)
        {
            buffer.SetCell(_position.X + 35, _position.Y + 0, 'C', ConsoleColor.Yellow);
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
