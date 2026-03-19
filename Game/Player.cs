using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Player : GameObject
{
    public int PosX { get; set; }
    public int PosY { get; set; }

    public bool Alive { get; private set; } = true;

    public Player(Scene scene, int[,] map) : base(scene)
    {

    }

    public override void Draw(ScreenBuffer buffer)
    {
        
    }

    public override void Update(float deltaTime)
    {
        //앞
        if (Input.IsKey(ConsoleKey.W) || Input.IsKey(ConsoleKey.UpArrow))
        {
            
        }
        //뒤
        if (Input.IsKey(ConsoleKey.S) || Input.IsKey(ConsoleKey.DownArrow))
        {

        }
        //좌
        if (Input.IsKey(ConsoleKey.A) || Input.IsKey(ConsoleKey.LeftArrow))
        {

        }
        //우
        if (Input.IsKey(ConsoleKey.D) || Input.IsKey(ConsoleKey.RightArrow))
        {

        }
    }
}
