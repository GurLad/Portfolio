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
    private NodePath pathLabel;
    private Label _label = null;
    private Label label => _label ?? (_label = GetNode<Label>(pathLabel));
    [Export]
    private NodePath pathNumber;
    private Label _number = null;
    private Label number => _number ?? (_number = GetNode<Label>(pathNumber));
}
