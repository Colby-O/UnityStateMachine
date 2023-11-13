using System;
using Timer = System.Diagnostics;
using UnityEngine;

public class State : BaseState
{
    protected readonly Action<State> onEnter;
    protected readonly Action<State> onUpdate;
    protected readonly Action<State> onFixedUpdate;
    protected readonly Action<State> onExit;
    protected readonly Func<State, bool> canExit;

    protected FiniteStateMachine fsm;
    protected readonly Timer.Stopwatch timer;

    public State(
        Action<State> onEnter = null, 
        Action<State> onUpdate = null,
        Action<State> onFixedUpdate = null,
        Action<State> onExit = null,
        Func<State, bool> canExit = null, 
        bool requireTimeToExit = false,
        bool instantTransition = false
    ) : base(requireTimeToExit, instantTransition)
    {
        this.onEnter = onEnter;
        this.onUpdate = onUpdate;
        this.onExit = onExit;
        this.canExit = canExit;

        this.timer = new Timer.Stopwatch();
    }

    public virtual void SetParnetStateMachine(FiniteStateMachine fsm) => this.fsm = fsm;

    public virtual int GetElapsedTime() => timer.Elapsed.Milliseconds;

    public override void Enter()
    {
        timer.Reset();
        timer.Start();
        onEnter?.Invoke(this);
    }

    public override void Update()
    {
        if (
            requireTimeToExit &&
            canExit != null &&
            fsm != null &&
            fsm.HasPendingTransition() &&
            canExit(this)
        ) fsm.StateCanExit();
        else onUpdate?.Invoke(this);
    }

    public override void FixedUpdate()
    {
        onFixedUpdate?.Invoke(this);
    }

    public override void Exit()
    {
        onExit?.Invoke(this);
    }

    public override void ExitRequest()
    {
        if (canExit != null && canExit(this)) fsm.StateCanExit();
    }
}
