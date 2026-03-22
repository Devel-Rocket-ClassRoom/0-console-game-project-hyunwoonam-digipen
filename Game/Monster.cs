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

    private PathFinding _pathFinding;

    private float _moveTimer = 0f;
    private float _moveInterval = 0.1f; // 0.1초마다 한 칸 이동

    public Monster(Scene scene, int[,] grid, Player player) : base(scene)
    {
        Name = "Monster";
        _grid = grid;
        _targetPlayer = player;

        _pathFinding = new PathFinding(_grid);
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
        _moveTimer += deltaTime;

        // 쿨타임이 찼을 때만 길찾기 및 이동 수행
        if (_moveTimer >= _moveInterval)
        {
            _moveTimer = 0f;

            Point start = new Point(PosX, PosY);
            Point end = new Point(_targetPlayer.PosX, _targetPlayer.PosY);

            // PathFinding 클래스의 GetPath 호출
            List<Point> path = _pathFinding.GetPath(start, end);

            // 경로가 존재하면 다음 칸으로 이동
            if (path != null && path.Count > 0)
            {
                PosX = path[0].X;
                PosY = path[0].Y;
            }
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.SetCell(PosX + 35, PosY, 'M', ConsoleColor.Magenta);
    }
}