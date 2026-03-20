using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class Monster : GameObject
{
    private Random _random = new Random();
    public int PosX { get; private set; }
    public int PosY { get; private set; }

    private int[,] _grid;
    private Player _targetPlayer;

    public Monster(Scene scene, int[,] grid, Player player) : base(scene)
    {
        Name = "Monster";
        _grid = grid;
        _targetPlayer = player;
    }

    public void Spawn()
    {
        int rows = _grid.GetLength(0);
        int cols = _grid.GetLength(1);

        int maxDistance = -1;
        int spawnX = 0;
        int spawnY = 0;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (_grid[y, x] == 0)
                {
                    int distance = Math.Abs(x - _targetPlayer.PosX) + Math.Abs(y - _targetPlayer.PosY);

                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        spawnX = x;
                        spawnY = y;
                    }
                }
            }
        }

        PosX = spawnX;
        PosY = spawnY;
    }
    public override void Update(float deltaTime)
    {
        
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.SetCell(PosX + 40, PosY, 'M', ConsoleColor.Magenta);
    }
}

