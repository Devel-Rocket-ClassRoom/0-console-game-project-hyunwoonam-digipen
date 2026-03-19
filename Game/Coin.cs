using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Coin : GameObject
{
    Random _random = new Random();
    private (int X, int Y) _position;
    public (int X, int Y) Position { get { return _position; } }
    public Coin(Scene scene) : base(scene)
    {
        Name = "Coin";
    }

    public override void Draw(ScreenBuffer buffer)
    {
        
    }

    public override void Update(float deltaTime)
    {
        
    }

    public void Spawn(LinkedList<(int X, int Y)> excludedCells,
        int left, int right, int top, int bottom)
    {
        do
        {
            _position.X = _random.Next(left, right + 1);
            _position.Y = _random.Next(top, bottom + 1);
        }
        while (excludedCells.Contains(_position));


    }
}
