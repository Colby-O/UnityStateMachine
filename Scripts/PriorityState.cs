using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityState : State
{
    public PriorityLevel priority { get; protected set; }

    public PriorityState(
        PriorityLevel priority = PriorityLevel.Low,
        Action<State> onEnter = null,
        Action<State> onUpdate = null,
        Action<State> onFixedUpdate = null,
        Action<State> onExit = null,
        Func<State, bool> canExit = null,
        bool requireTimeToExit = false,
        bool instantTransition = false
    ) : base(onEnter, onUpdate, onFixedUpdate, onExit, canExit, requireTimeToExit, instantTransition)
    {
        this.priority = priority;
    }
}
