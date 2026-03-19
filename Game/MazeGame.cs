using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class MazeGame : GameApp
{
    private readonly SceneManager<Scene> _scenes = new SceneManager<Scene>();
    public MazeGame() : base(110, 30)
    {
    }

    protected override void Initialize()
    {
        ChangeToTitle();
    }

    protected override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Escape))
        {
            Quit();
            return;
        }

        _scenes.CurrentScene?.Update(deltaTime);
    }

    protected override void Draw()
    {
        _scenes.CurrentScene?.Draw(Buffer);
    }

    private void ChangeToTitle()
    {
        var title = new TitleScene();
        //title.StartRequestd += ChangeToPlay;
        _scenes.ChangeScene(title);
    }
    //private void ChangeToPlay()
    //{
    //    var play = new PlayScene();
    //    play.PlayAgainRequested += ChangeToTitle;
    //    _scenes.ChangeScene(play);
    //}
}