using Godot;
using System;
using System.Collections.Generic;

[Tool]
public class LinkButton : Button
{
    private static readonly List<string> OVERRIDES = new List<string> { "normal", "hover", "pressed", "focus", "disabled" };

    [Export]
    private string url;
    [Export]
    private Color color
    {
        get => ((StyleBoxTexture)GetStylebox("normal")).ModulateColor;
        set
        {
            StyleBoxTexture modified = (StyleBoxTexture)baseStyle.Duplicate();
            modified.ModulateColor = value;
            OVERRIDES.ForEach(a => AddStyleboxOverride(a, modified));
        }
    }
    [Export]
    private float expandTime;
    [Export]
    private float expandSize;
    [Export]
    private StyleBoxTexture baseStyle;

    private Interpolator interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        if (!Engine.EditorHint)
        {
            AddChild(interpolator);
            RectPivotOffset = RectSize / 2;
        }
    }

    public void OnMouseEnter()
    {
        if (!Engine.EditorHint)
        {
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
        if (OS.HasFeature("JavaScript"))
        {
            JavaScript.Eval("window.open('" + url + "', '_blank').focus();");
        }
        else
        {
            OS.ShellOpen(url);
        }
    }
}
