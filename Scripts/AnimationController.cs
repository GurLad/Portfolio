using Godot;
using System;

[Tool]
public class AnimationController : Node
{
    // Use consts instead of autoload so that it will work in the editor as well
    private const int NUM_FRAMES = 5;
    private const int ICON_OFFSET = 3;
    private const float RATE = 5;

    public static int CurrentFrame
    {
        get
        {
            float time = (float)Time.GetTicksMsec() / 1000;
            return (Mathf.FloorToInt(time * RATE) + ICON_OFFSET) % NUM_FRAMES;
        }
    }
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
        noiseMaterial.SetShaderParam("currentFrame", (CurrentFrame + NUM_FRAMES - ICON_OFFSET));
    }
}
