using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

public class Player : GameObject
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public bool Alive { get; private set; } = true;
    private int[,] _grid;

    public Player(Scene scene, int[,] map) : base(scene)
    {
        _grid = map;
        PosX = 1;
        PosY = 1;
        Name = "Player";
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.SetCell(PosX + 35, PosY + 0, 'P', ConsoleColor.Cyan);
    }

    public override void Update(float deltaTime)
    {
        int nextX = PosX;
        int nextY = PosY;
        //앞
        if (Input.IsKey(ConsoleKey.W) || Input.IsKey(ConsoleKey.UpArrow))
        {
            nextY--;
        }
        //뒤
        if (Input.IsKey(ConsoleKey.S) || Input.IsKey(ConsoleKey.DownArrow))
        {
            nextY++;
        }
        //좌
        if (Input.IsKey(ConsoleKey.A) || Input.IsKey(ConsoleKey.LeftArrow))
        {
            nextX--;
        }
        //우
        if (Input.IsKey(ConsoleKey.D) || Input.IsKey(ConsoleKey.RightArrow))
        {
            nextX++;
        }

        if (nextY >= 0 && nextY < _grid.GetLength(0) && nextX >= 0 && nextX < _grid.GetLength(1))
        {
            if (_grid[nextY, nextX] == 0)
            {
                PosX = nextX;
                PosY = nextY;
            }
        }
    }
}
