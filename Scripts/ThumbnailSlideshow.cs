using Godot;
using System;
using System.Collections.Generic;

[Tool]
public class ThumbnailSlideshow : TextureRect
{
    [Export]
    public List<Texture> Thumbnails;
    [Export]
    private float beginDelay;
    [Export]
    private float displayTime;
    [Export]
    private float transitionTime;
    [Export]
    private NodePath pathAltRect;
    private TextureRect altRect;
    private Interpolator interpolator = new Interpolator();
    private int currentImage = 0;
    private bool transitioning = false;
    public string wa;

    public override void _Ready()
    {
        base._Ready();
        if (!Engine.EditorHint)
        {
            AddChild(interpolator);
            altRect = GetNode<TextureRect>(pathAltRect);
        }
    }

    public void Begin()
    {
        if ((Thumbnails?.Count ?? -1) < 2)
        {
            return;
        }
        if (transitioning)
        {
            interpolator.OnFinish = () =>
            {
                transitioning = false;
                Begin();
            };
        }
        interpolator.Delay(beginDelay);
        interpolator.OnFinish = NextImage;
    }

    public void End()
    {
        if ((Thumbnails?.Count ?? -1) < 2)
        {
            return;
        }
        if (transitioning)
        {
            interpolator.OnFinish = () =>
            {
                transitioning = false;
                End();
            };
        }
        transitioning = true;
        altRect.Texture = Texture;
        altRect.SelfModulate = new Color(1, 1, 1, 1);
        Texture = Thumbnails[currentImage = 0];
        interpolator.Interpolate(transitionTime,
            new Interpolator.InterpolateObject(
                a => altRect.SelfModulate = new Color(1, 1, 1, a),
                1,
                0));
        interpolator.OnFinish = () => transitioning = false;
    }

    private void NextImage()
    {
        transitioning = true;
        altRect.Texture = Texture;
        altRect.SelfModulate = new Color(1, 1, 1, 1);
        Texture = Thumbnails[currentImage = (currentImage + 1) % Thumbnails.Count];
        interpolator.Interpolate(transitionTime,
            new Interpolator.InterpolateObject(
                a => altRect.SelfModulate = new Color(1, 1, 1, a),
                1,
                0));
        interpolator.OnFinish = () =>
        {
            transitioning = false;
            interpolator.Delay(displayTime);
            interpolator.OnFinish = NextImage;
        };
    }
}
