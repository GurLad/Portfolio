using Godot;
using System;
using System.Collections.Generic;

[Tool]
public class GamePreview : PanelContainer
{
    [Export(PropertyHint.MultilineText)]
    public string GameName { get => title.Text; set { if (Engine.EditorHint) title.Text = value; } }
    //[Export]
    //public Texture Image { get => thumbnail.Texture; set { if (Engine.EditorHint) thumbnail.Texture = value; } }
    [Export]
    public List<Texture> Thumbnails
    {
        get => thumbnail.Thumbnails;
        set
        {
            if (Engine.EditorHint)
            {
                thumbnail.Thumbnails = value;
                if ((value?.Count ?? -1) > 0)
                {
                    thumbnail.Texture = value[0];
                }
            }
        }
    }
    [Export]
    public string EngineName
    {
        get => gameDatas[0].Text.Trim();
        set
        {
            if (Engine.EditorHint)
            {
                gameDatas[0].Text = " " + value;
                gameDatas[0].IconName = value;
            }
        }
    }
    [Export]
    public string Date { get => gameDatas[1].Text; set { if (Engine.EditorHint) gameDatas[1].Text = value; } }
    [Export]
    public string Time { get => gameDatas[2].Text; set { if (Engine.EditorHint) gameDatas[2].Text = value; } }
    [Export]
    public string Team
    {
        get => gameDatas[3].Text;
        set
        {
            if (Engine.EditorHint)
            {
                gameDatas[3].Text = value;
                gameDatas[3].IconName = value.Contains("1 ") ? "Solo" : "Team";
            }
        }
    }
    [Export(PropertyHint.MultilineText)]
    public string Description { get => description.Text; set { if (Engine.EditorHint) description.Text = value; } }
    [Export]
    public Color Color { get => background.SelfModulate; set { if (Engine.EditorHint) background.SelfModulate = value; } }
    [Export]
    public PackedScene GamePage;

    [Export]
    private float expandTime;
    [Export]
    private float expandSize;

    [Export]
    private NodePath pathTitle;
    private Label _title = null;
    private Label title => _title ?? (_title = GetNode<Label>(pathTitle));
    [Export]
    private List<NodePath> pathGameDatas;
    private List<GameData> _gameDatas = null;
    private List<GameData> gameDatas => _gameDatas ?? (_gameDatas = pathGameDatas.ConvertAll(a => GetNode<GameData>(a)));
    [Export]
    private NodePath pathDescription;
    private Label _description = null;
    private Label description => _description ?? (_description = GetNode<Label>(pathDescription));
    [Export]
    private NodePath pathBackground;
    private PanelContainer _background = null;
    private PanelContainer background => _background ?? (_background = GetNode<PanelContainer>(pathBackground));
    [Export]
    private NodePath pathThumbnail;
    private ThumbnailSlideshow _thumbnail = null;
    private ThumbnailSlideshow thumbnail => _thumbnail ?? (_thumbnail = GetNode<ThumbnailSlideshow>(pathThumbnail));

    private Interpolator interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
        RectPivotOffset = RectSize / 2;
    }

    public void OnMouseEnter()
    {
        if (!Engine.EditorHint)
        {
            thumbnail.Begin();
            interpolator.Interpolate(expandTime,
                new Interpolator.InterpolateObject(
                    (a) => RectScale = Vector2.One * a,
                    RectScale.x,
                    expandSize,
                    Easing.EaseOutQuart));
        }
    }

    public void OnMouseLeave()
    {
        if (!Engine.EditorHint)
        {
            thumbnail.End();
            interpolator.Interpolate(expandTime,
                new Interpolator.InterpolateObject(
                    (a) => RectScale = Vector2.One * a,
                    RectScale.x,
                    1,
                    Easing.EaseOutQuart));
        }
    }

    public void Click()
    {
        SceneController.Current.TransitionToNewScene(GamePage);
    }

    public void GUIInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouse mouseEvent && mouseEvent.IsPressed() && (mouseEvent.ButtonMask & (int)ButtonList.MaskLeft) != 0)
        {
            Click();
        }
    }
}
