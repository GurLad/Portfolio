using Godot;
using System;
using System.Collections.Generic;

[Tool]
public class Title : Node
{
    private const string ICON_PATH = "res://Textures/Icons/";
    private const string ICON_PREFIX = "/000";
    private const string ICON_POSTFIX = ".png";

    [Export]
    public string Text
    {
        get => label.Text;
        set { if (Engine.EditorHint) label.Text = value; }
    }
    [Export]
    public string Icon;
    [Export]
    private NodePath pathLabel;
    [Export]
    private NodePath pathIconL;
    [Export]
    private NodePath pathIconR;
    private Label _label = null;
    private Label label => _label ?? (_label = GetNode<Label>(pathLabel));
    private TextureRect _iconL = null;
    private TextureRect iconL => _iconL ?? (_iconL = GetNode<TextureRect>(pathIconL));
    private TextureRect _iconR = null;
    private TextureRect iconR => _iconR ?? (_iconR = GetNode<TextureRect>(pathIconR));
    private Dictionary<string, Texture> iconCache = new Dictionary<string, Texture>();

    public override void _Process(float delta)
    {
        base._Process(delta);
        iconL.Texture = iconR.Texture = SafeGetIcon((Mathf.FloorToInt(Time.GetTicksMsec() * 5 / 100) / 5) % 5);
        //GD.Print((Mathf.FloorToInt(Time.GetTicksMsec() * 5 / 100) / 5) % 5);
    }

    private Texture SafeGetIcon(int index)
    {
        if (!iconCache.ContainsKey(Icon + index))
        {
            string fullName = ICON_PATH + Icon + ICON_PREFIX + index + ICON_POSTFIX;
            iconCache.Add(Icon + index, (Texture)GD.Load(fullName));
        }
        return iconCache[Icon + index];
    }
}
