using Godot;
using System;

public class SceneController : Control
{
    private enum State { Idle, FadeOut, FadeIn }
    public static SceneController Current;

    [Export]
    private float transitionTime = 2;
    [Export]
    private NodePath pathScenesNode;
    [Export]
    private NodePath pathBackButton;
    [Export]
    private NodePath pathFirstScene;
    [Export]
    private NodePath pathScrollContainer;

    private State state;
    private Control currentScene = null;
    private Action midTransition;
    private Action postTransition;
    private Timer timer = new Timer();
    private Node scenesNode;
    private BackButton backButton;
    private ScrollContainer scrollContainer;
    private int oldScroll = 0;
    private bool updateOldScroll = false; // Bad fix

    public override void _Ready()
    {
        base._Ready();
        Current = this;
        AddChild(timer);
        timer.OneShot = true;
        timer.WaitTime = transitionTime / 2;
        scenesNode = GetNode(pathScenesNode);
        backButton = GetNode<BackButton>(pathBackButton);
        currentScene = GetNode<Control>(pathFirstScene);
        scrollContainer = GetNode<ScrollContainer>(pathScrollContainer);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        switch (state)
        {
            case State.Idle:
                break;
            case State.FadeOut:
                currentScene.Modulate = new Color(currentScene.Modulate, 1 - timer.Percent());
                if (timer.TimeLeft <= 0)
                {
                    FinishFadeOut();
                }
                break;
            case State.FadeIn:
                if (updateOldScroll)
                {
                    scrollContainer.ScrollVertical = oldScroll;
                    updateOldScroll = false;
                }
                currentScene.Modulate = new Color(currentScene.Modulate, timer.Percent());
                if (timer.TimeLeft <= 0)
                {
                    FinishFadeIn();
                }
                break;
            default:
                break;
        }
    }

    private void FinishFadeOut()
    {
        currentScene.Modulate = new Color(currentScene.Modulate, 1);
        currentScene.Visible = false;
        state = State.FadeIn;
        midTransition?.Invoke();
        timer.Start();
        currentScene.Visible = true;
    }

    private void FinishFadeIn()
    {
        currentScene.Modulate = new Color(currentScene.Modulate, 1);
        state = State.Idle;
        postTransition?.Invoke();
    }

    public void Transition(Action midTransition, Action postTransition)
    {
        this.midTransition = midTransition;
        this.postTransition = postTransition;
        state = State.FadeOut;
        timer.Start();
    }

    public void TransitionToNewScene(PackedScene scene, bool clearCurrent = false)
    {
        if (state != State.Idle)
        {
            return;
        }
        Transition(() =>
        {
            if (clearCurrent)
            {
                ClearCurrentScene();
            }
            scenesNode.AddChild(currentScene = scene.Instance<Control>());
            currentScene.Modulate = new Color(currentScene.Modulate, 0);
            backButton.ShowButton();
            oldScroll = scrollContainer.ScrollVertical;
            GD.Print(oldScroll);
            scrollContainer.ScrollVertical = 0;
        }, null);
    }

    public void TransitionToOldScene(Control scene, bool clearCurrent = true)
    {
        if (state != State.Idle)
        {
            return;
        }
        Transition(() =>
        {
            if (clearCurrent)
            {
                ClearCurrentScene();
            }
            currentScene = scene;
            currentScene.Modulate = new Color(currentScene.Modulate, 0);
            backButton.HideButton();
            updateOldScroll = true;
        }, null);
    }

    private void ClearCurrentScene()
    {
        if (currentScene != null)
        {
            currentScene.QueueFree();
            currentScene = null;
        }
    }
}
