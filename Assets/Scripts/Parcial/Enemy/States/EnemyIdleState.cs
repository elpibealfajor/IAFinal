using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState<T> : EnemyStateBase<T>
{
    T input;

    public EnemyIdleState(T input)
    {
        this.input = input;
    }
    public override void Awake()
    {
        base.Awake();
        model.StartCoroutine("resetPatrollsCompleted"); // no puedo hacerlo aca por no tener monobehaviour      
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
