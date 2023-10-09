using Godot;
using System;

[Tool]
public class Title : Node
{
    [Export]
    public string Text
    {
        get => label.Text;
        set { if (Engine.EditorHint) label.Text = value; }
    }
    [Export]
    private NodePath pathLabel;
    private Label _label = null;
    private Label label => _label ?? (_label = GetNode<Label>(pathLabel));
}
