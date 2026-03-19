using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

public class PlayScene : Scene
{
    private Player player;
    private Coin coin;

    private int coinCount;
    private float timeCount;
    private bool isGameOver;

    private int[,] grid;

    public event GameAction PlayAgainRequested;

    public PlayScene()
    {
        grid = new int[,] {
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,1,1,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,1,1},
                {1,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1},
                {1,0,0,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };
    }

    public override void Load()
    {
        coinCount = 0;
        isGameOver = false;

        player = new Player(this, grid);
        coin = new Coin(this);
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        if (isGameOver)
        {
            if (Input.IsKeyDown(ConsoleKey.Enter))
            {
                PlayAgainRequested?.Invoke();
            }
            return;
        }

        UpdateGameObjects(deltaTime);

        if (timeCount > 10f)
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

        //if (snake.HeadPosition == food.Position)
        //{
        //    snake.Grow();
        //    score += 10;
        //    food.Spawn(snake.Body, Wall.Left, Wall.Right, Wall.Top, Wall.Bottom);
        //}
    }

    public override void Draw(ScreenBuffer buffer)
    {
        int startX = 40;
        int startY = 5;

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

        buffer.WriteText(1, 19, "Arrow Keys: Move", ConsoleColor.DarkGray);

        if (isGameOver)
        {
            buffer.WriteTextCentered(8, "GAME OVER", ConsoleColor.Red);
            buffer.WriteTextCentered(12, "Press ENTER to Retry", ConsoleColor.White);
        }
    }
}
