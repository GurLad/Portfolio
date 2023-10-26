using Godot;
using System;
using System.Collections.Generic;

[Tool]
public class Icon : TextureRect
{
    //private const string ICON_PATH = "res://Textures/Icons/";
    private const string ICON_PREFIX = "/";
    private const string ICON_POSTFIX = ".png";

    [Export]
    public string IconName;
    [Export(PropertyHint.Dir)]
    public string Path = "res://Textures/Icons/";
    [Export]
    public int NumFrames = -1;
    [Export]
    public float Rate = -1;
    private Dictionary<string, Texture> iconCache = new Dictionary<string, Texture>();
    //private float prevMili = 0;
    //private int prevInd = 0;

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (IconName.Length > 0)
        {
            Texture = SafeGetIcon(AnimationController.GetCurrentFrame(NumFrames, Rate));
        }
        else
        {
            Texture = null;
        }
        //if (index != prevInd)
        //{
        //    prevInd = index;
        //    GD.Print("Diff: " + (Time.GetTicksMsec() - prevMili));
        //    prevMili = Time.GetTicksMsec();
        //}
    }

    private Texture SafeGetIcon(int index)
    {
        if (!iconCache.ContainsKey(IconName + index))
        {
            string fullName = Path + @"\" + IconName + ICON_PREFIX + index.ToString().PadLeft(4, '0') + ICON_POSTFIX;
            iconCache.Add(IconName + index, (Texture)GD.Load(fullName));
        }
        return iconCache[IconName + index];
    }
}
