using Godot;
using System;

[Tool]
public class GameData : HBoxContainer
{
    [Export]
    public string Text
    {
        get => number.Text + " " + label.Text;
        set
        {
            if (Engine.EditorHint)
            {
                string[] parts = value.Split(" ");
                if (parts.Length != 2)
                {
                    Text = "Err Error";
                    return;
                }
                number.Text = parts[0];
                label.Text = parts[1];
            }
        }
    }
    [Export]
    public string IconName { get => icon.IconName; set { if (Engine.EditorHint) icon.IconName = value; } }
    [Export]
    private NodePath pathLabel;
    private Label _label = null;
    private Label label => _label ?? (_label = GetNode<Label>(pathLabel));
    [Export]
    private NodePath pathNumber;
    private Label _number = null;
    private Label number => _number ?? (_number = GetNode<Label>(pathNumber));
    [Export]
    private NodePath pathIcon;
    private Icon _icon = null;
    private Icon icon => _icon ?? (_icon = GetNode<Icon>(pathIcon));
}
