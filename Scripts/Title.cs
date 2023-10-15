using Godot;
using System;
using System.Collections.Generic;

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
    public string Icon;
    [Export]
    private NodePath pathLabel;
    [Export]
    private NodePath pathIconL;
    [Export]
    private NodePath pathIconR;
    private Label _label = null;
    private Label label => _label ?? (_label = GetNode<Label>(pathLabel));
    private Icon _iconL = null;
    private Icon iconL => _iconL ?? (_iconL = GetNode<Icon>(pathIconL));
    private Icon _iconR = null;
    private Icon iconR => _iconR ?? (_iconR = GetNode<Icon>(pathIconR));
}
