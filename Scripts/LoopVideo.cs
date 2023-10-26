using Godot;
using System;

public class LoopVideo : VideoPlayer
{
    public void Finished()
    {
        Paused = false;
        Play();
    }
}
