using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransitionCallback
{
    public abstract void BeforeTransition();
    public abstract void AfterTransition();
}

public abstract class BaseTransition : ITransitionCallback
{
    public string from;
    public string to;
    public bool forceTransition;

    public BaseTransition(string from, string to, bool forceTransition = false)
    {
        this.from = from;
        this.to = to;
        this.forceTransition = forceTransition;
    }

    public virtual void Init() { }

    public abstract bool CanTransition();
    public abstract void BeforeTransition();
    public abstract void AfterTransition();
}

public class Transition : BaseTransition
{
    public Action<Transition> beforeTransition;
    public Action<Transition> afterTransition;
    public Func<Transition, bool> condition;

    public Transition(
        string from, 
        string to, 
        Action<Transition> beforeTransition = null, 
        Action<Transition> afterTransition = null, 
        Func<Transition, bool> condition = null, 
        bool forceTransition = false
        ) : base(from, to, forceTransition) 
    {
        this.beforeTransition = beforeTransition;
        this.afterTransition = afterTransition;
        this.condition = condition;
    }

    public override bool CanTransition()
    {
        return condition == null || condition(this);
    }

    public override void BeforeTransition() => beforeTransition?.Invoke(this);

    public override void AfterTransition() => afterTransition?.Invoke(this);
}

public class ReverseTransition : BaseTransition
{
    public BaseTransition forwardTransition;

    public ReverseTransition(
        BaseTransition forwardTransition,
        bool forceTransition = false
        ) : base(forwardTransition.to, forwardTransition.from, forceTransition)
    {
        this.forwardTransition = forwardTransition;
    }

    public override bool CanTransition() => !forwardTransition.CanTransition();

    public override void BeforeTransition() => forwardTransition.AfterTransition();

    public override void AfterTransition() => forwardTransition.BeforeTransition();
}
