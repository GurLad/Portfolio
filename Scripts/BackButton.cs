using Godot;
using System;

public class BackButton : Control
{
    [Export]
    private NodePath pathIndex;
    [Export]
    private float showTime;
    [Export]
    private float expandTime;
    [Export]
    private float expandSize;

    private Interpolator interpolator = new Interpolator();
    private bool active;
    private float showDist;
    private Control index;

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
        index = GetNode<Control>(pathIndex);
        showDist = RectSize.y;
        RectPosition = new Vector2(0, -showDist);
        RectPivotOffset = RectSize / 2;
    }

    public void ShowButton()
    {
        interpolator.Interpolate(showTime,
            new Interpolator.InterpolateObject(
                (a) => RectPosition = new Vector2(0, a),
                RectPosition.y,
                0,
                Easing.EaseOutQuart));
        interpolator.OnFinish = () => active = true;
    }

    public void HideButton()
    {
        active = false;
        interpolator.Interpolate(showTime,
            new Interpolator.InterpolateObject(
                (a) => RectPosition = new Vector2(0, a),
                RectPosition.y,
                -showDist,
                Easing.EaseOutQuart));
    }

    public void Click()
    {
        if (active)
        {
            OnMouseLeave();
            active = false;
            SceneController.Current.TransitionToOldScene(index);
        }
    }

    public void OnMouseEnter()
    {
        if (active)
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
        if (active)
        {
            interpolator.Interpolate(expandTime,
                new Interpolator.InterpolateObject(
                    (a) => RectScale = Vector2.One * a,
                    RectScale.x,
                    1,
                    Easing.EaseOutQuart));
        }
    }
}
