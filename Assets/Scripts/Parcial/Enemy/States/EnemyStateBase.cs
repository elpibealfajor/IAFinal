using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase<T> : State<T>
{
    protected EnemyModel model;
    protected FSM<T> fsm;

    public void InitializedState(EnemyModel model, FSM<T> fsm)
    {
        this.model = model;
        this.fsm = fsm;
    }
}
