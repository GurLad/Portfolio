using Godot;
using System;

public class LinkLinkButton : Godot.LinkButton
{
    [Export]
    private string url;

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
