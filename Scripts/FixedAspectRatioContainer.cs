using Godot;
using System;
using System.Collections.Generic;

public class FixedAspectRatioContainer : Container
{
    [Export]
    private Vector2 ratio;
    [Export]
    private List<NodePath> pathChildrenToResize;
    [Export]
    private List<NodePath> pathChildrenWithTopRightOffset;

    private List<Control> children;
    private List<Control> fixOffset;

    public override void _Ready()
    {
        base._Ready();
        fixOffset = pathChildrenWithTopRightOffset.ConvertAll(a => GetNode<Control>(a));
        children = pathChildrenToResize.ConvertAll(a => GetNode<Control>(a));
    }

    public override void _Draw()
    {
        base._Draw();
        RectMinSize = new Vector2(0, RectSize.x / (ratio.x / ratio.y));
        RectSize = new Vector2(RectSize.x, RectMinSize.y);
        children.ForEach(a => { a.RectMinSize = RectMinSize; a.RectSize = RectSize; });
        fixOffset.ForEach(a => a.RectGlobalPosition = RectGlobalPosition);
    }
}
