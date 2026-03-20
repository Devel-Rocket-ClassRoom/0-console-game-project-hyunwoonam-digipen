using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MazeGenerator
{
    private int roomRows;
    private int roomCols;

    private int mapWidth;
    private int mapHeight;

    public int[,] map;
    private Random rand = new Random();

    private int[] directionX = { 0, 0, -2, 2 };
    private int[] directionY = { -2, 2, 0, 0 };

    public MazeGenerator(int w, int h)
    {
        roomRows = w;
        roomCols = h;
        mapWidth = w * 2 + 1;
        mapHeight = h * 2 + 1;

        map = new int[mapHeight, mapWidth];
        InitializeMap();
    }

    private void InitializeMap()
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                map[y, x] = 1;
            }
        }
    }

    public void GenerateMaze()
    {
        int startX = rand.Next(roomRows) * 2 + 1;
        int startY = rand.Next(roomCols) * 2 + 1;

        GenerateRecursive(startX, startY);
        GenerateBraidMaze();
    }

    public void GenerateRecursive(int startX, int startY)
    {
        Stack<(int x, int y)> stack = new Stack<(int x, int y)>();


        map[startY, startX] = 0;
        stack.Push((startX, startY));

        while (stack.Count > 0)
        {
            var current = stack.Peek();
            int x = current.x;
            int y = current.y;

            int[] directions = { 0, 1, 2, 3 };
            directions = directions.OrderBy(d => rand.Next()).ToArray();

            bool moved = false; 

            foreach (int dir in directions)
            {
                int nx = x + directionX[dir];
                int ny = y + directionY[dir];

                if (nx > 0 && nx < mapWidth - 1 && ny > 0 && ny < mapHeight - 1 
                    && map[ny, nx] == 1)
                {
                    int wallX = x + (directionX[dir] / 2);
                    int wallY = y + (directionY[dir] / 2);

                    map[wallY, wallX] = 0;

                    map[ny, nx] = 0;

                    stack.Push((nx, ny));
                    moved = true;
                    break; 
                }
            }

            if (moved == false)
            {
                stack.Pop();
            }
        }
    }

    public void GenerateBraidMaze(double probability = 1.0)
    {
        for (int y = 1; y < mapHeight; y += 2)
        {
            for (int x = 1; x < mapWidth; x += 2)
            {
                if (IsDeadEnd(x, y))
                {
                    if (rand.NextDouble() <= probability)
                    {
                        RemoveRandomWall(x, y);
                    }
                }
            }
        }
    }

    private bool IsDeadEnd(int x, int y)
    {
        int canMove = 0;

        if (map[y - 1, x] == 0)
        {
            canMove++;
        }
        if (map[y + 1, x] == 0)
        {
            canMove++;
        }
        if (map[y, x - 1] == 0)
        {
            canMove++;
        }
        if (map[y, x + 1] == 0)
        {
            canMove++;
        }
        return canMove == 1;
    }

    private void RemoveRandomWall(int x, int y)
    {
        List<int[]> validWalls = new List<int[]>();

        if (y > 1 && map[y - 1, x] == 1)
        {
            validWalls.Add(new int[] { x, y - 1 });
        }
        if (y < mapHeight - 2 && map[y + 1, x] == 1)
        {
            validWalls.Add(new int[] { x, y + 1 });
        }
        if (x > 1 && map[y, x - 1] == 1)
        {
            validWalls.Add(new int[] { x - 1, y });
        }
        if (x < mapWidth - 2 && map[y, x + 1] == 1)
        {
            validWalls.Add(new int[] { x + 1, y });
        }

        if (validWalls.Count > 0)
        {
            int[] wall = validWalls[rand.Next(validWalls.Count)];

            map[wall[1], wall[0]] = 0; 
        }
    }
}
