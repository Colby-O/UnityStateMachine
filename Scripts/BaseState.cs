using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected bool requireTimeToExit;
    protected bool instantTransition;
    protected string name;

    public BaseState(bool requireTimeToExit = false, bool instantTransition = false)
    {
        this.requireTimeToExit = requireTimeToExit;
        this.instantTransition = instantTransition;
        this.Init();
    }

    public bool HasExitTime() => requireTimeToExit;
    public bool HasInstantExit() => instantTransition;
    public string GetName() => name;
    public void SetName(string name) => this.name = name;

    public virtual void Init() { }

    public abstract void Enter();

    public abstract void Update();

    public abstract void FixedUpdate();

    public abstract void ExitRequest();

    public abstract void Exit();
}