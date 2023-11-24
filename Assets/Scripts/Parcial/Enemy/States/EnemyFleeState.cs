using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFleeState<T> : EnemyStateBase<T>
{
    T input;

    public EnemyFleeState(T input)
    {
        this.input = input;
    }
    public override void Awake()
    {
        base.Awake();
        model.rb.isKinematic = false;
        model.InitializeSteering();
    }
    public override void Execute()
    {
        base.Execute();
        model.Flee();
    }
    public override void Sleep()
    {
        base.Sleep();
        model.rb.isKinematic = true;
    }
}
