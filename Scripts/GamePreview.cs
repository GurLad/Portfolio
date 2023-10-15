using Godot;
using System;
using System.Collections.Generic;

[Tool]
public class GamePreview : PanelContainer
{
    [Export(PropertyHint.MultilineText)]
    public string GameName { get => title.Text; set { if (Engine.EditorHint) title.Text = value; } }
    [Export]
    public Texture Image; // TBA
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
    public string Team { get => gameDatas[3].Text; set { if (Engine.EditorHint) gameDatas[3].Text = value; } }
    [Export(PropertyHint.MultilineText)]
    public string Description { get => description.Text; set { if (Engine.EditorHint) description.Text = value; } }

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
}
