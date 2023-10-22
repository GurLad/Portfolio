using Godot;
using System;
using System.Collections.Generic;

[Tool]
public class Icon : TextureRect
{
    private const string ICON_PATH = "res://Textures/Icons/";
    private const string ICON_PREFIX = "/000";
    private const string ICON_POSTFIX = ".png";

    [Export]
    public string IconName;
    private Dictionary<string, Texture> iconCache = new Dictionary<string, Texture>();
    //private float prevMili = 0;
    //private int prevInd = 0;

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (IconName.Length > 0)
        {
            Texture = SafeGetIcon(AnimationController.CurrentFrame);
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
            string fullName = ICON_PATH + IconName + ICON_PREFIX + index + ICON_POSTFIX;
            iconCache.Add(IconName + index, (Texture)GD.Load(fullName));
        }
        return iconCache[IconName + index];
    }
}
