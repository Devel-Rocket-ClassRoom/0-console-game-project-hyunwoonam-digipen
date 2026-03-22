using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Linq;

public class PlayScene : Scene
{
    private Player player;
    private Coin coin;
    private Escape escape;
    private Monster monster;

    private int coinCount;
    private float timeCount = default;
    private float timeLimit = 120f;
    private bool isGameOver;
    private bool isWin;

    private int[,] grid;

    public event GameAction PlayAgainRequested;

    public PlayScene()
    {
        MazeGenerator maze = new MazeGenerator(25, 10);
        maze.GenerateMaze();
        grid = maze.map;
    }

    public override void Load()
    {
        coinCount = 0;
        isGameOver = false;

        player = new Player(this, grid);
        monster = new Monster(this, grid, player);

        coin = new Coin(this);
        escape = new Escape(this);

        monster.Spawn();
        coin.Spawn(grid);
        escape.Spawn(grid);

        AddGameObject(player);
        AddGameObject(monster);

        AddGameObject(coin);
        AddGameObject(escape);
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        if (isGameOver || isWin)
        {
            if (Input.IsKeyDown(ConsoleKey.Enter))
            {
                PlayAgainRequested?.Invoke();
            }
            return;
        }

        UpdateGameObjects(deltaTime);

        if (timeCount > timeLimit)
        {
            isGameOver = true;
            return;
        }

        timeCount += deltaTime;

        if (!player.Alive)
        {
            isGameOver = true;
            return;
        }

        if (player.PosX == coin.Position.X && player.PosY == coin.Position.Y)
        {
            coinCount++;

            escape.CoinCount = coinCount;

            if (coinCount < 3)
            {
                coin.Spawn(grid);
            }
        }

        if (coinCount == 3)
        {
            coin.isGetAll = true;
        }

        if (player.PosX == escape.Position.X && player.PosY == escape.Position.Y)
        {
            if (coinCount >= 3)
            {
                isWin = true;
                return;
            }
        }

        if (player.PosX == monster.PosX && player.PosY == monster.PosY)
        {
            isGameOver = true;
            return;
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        int startX = 35;
        int startY = 0;

        int remainingTime = (int)Math.Max(0, timeLimit - timeCount);

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++) 
            {
                if (grid[y, x] == 1)
                {
                    buffer.SetCell(startX + x, startY + y, '█', ConsoleColor.DarkGray);
                }
                else
                {
                    buffer.SetCell(startX + x, startY + y, ' ', ConsoleColor.Gray);
                }
            }
        }

        DrawGameObjects(buffer);

        buffer.WriteText(1, 17, "Need Coins: 3, Get Coins: " + coinCount, ConsoleColor.DarkGray);
        buffer.WriteText(1, 18, "Time Limit: " + remainingTime + "seconds", ConsoleColor.DarkGray);
        buffer.WriteText(1, 19, "Arrow Keys: Move", ConsoleColor.DarkGray);

        if (isGameOver)
        {
            buffer.WriteTextCentered(8, "GAME OVER", ConsoleColor.Red);
            buffer.WriteTextCentered(12, "Press ENTER to Retry", ConsoleColor.White);
        }
        if (isWin)
        {
            buffer.WriteTextCentered(8, "GAME Win", ConsoleColor.Red);
            buffer.WriteTextCentered(12, "Press ENTER to Retry", ConsoleColor.White);
        }
    }
}
