using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistractedState<T> : EnemyStateBase<T> 
{
    T input;

    public EnemyDistractedState(T input)
    {
        this.input = input;
    }
    public override void Awake()
    {
        base.Awake();
    }
    public override void Execute()
    {
        base.Execute();

    }
    public override void Sleep()
    {
        base.Sleep();
    }
}
