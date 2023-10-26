using Godot;
using System;

[Tool]
public class AnimationController : Node
{
    // Use consts instead of autoload so that it will work in the editor as well
    public const int NUM_FRAMES = 5;
    public const int ICON_OFFSET = 3;
    public const float RATE = 5;

    public static int CurrentFrame => GetCurrentFrame(NUM_FRAMES, RATE, ICON_OFFSET);
    private static AnimationController current;

    [Export]
    private ShaderMaterial noiseMaterial;

    public override void _Ready()
    {
        base._Ready();
        if (!Engine.EditorHint)
        {
            if (current != null)
            {
                QueueFree();
            }
            else
            {
                current = this;
            }
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        noiseMaterial.SetShaderParam("currentFrame", GetCurrentFrame(NUM_FRAMES, RATE));
    }

    public static int GetCurrentFrame(int numFrames, float rate, int offset = 0)
    {
        if (numFrames <= 0)
        {
            numFrames = NUM_FRAMES;
        }
        if (rate <= 0)
        {
            rate = RATE;
        }
        float time = (float)Time.GetTicksMsec() / 1000;
        return (Mathf.FloorToInt(time * rate) + offset) % numFrames;
    }
}
