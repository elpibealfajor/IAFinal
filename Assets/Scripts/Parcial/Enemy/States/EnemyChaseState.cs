using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState<T> : EnemyStateBase<T>
{
    T input;
    public EnemyChaseState(T input)
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

        // asi se haria si no quisiera ponerle obstacle avoidance,
        // el profe dijo que sino va en enemymodel pero en una sola funcion
        //model.transform.position = Vector3.MoveTowards(model.transform.position, model.target.position
        //     , model.chaseSpeed * Time.deltaTime);
        //model.transform.LookAt(model.target);

        model.Move(model.target, model.chaseSpeed);
    }
    public override void Sleep()
    {
        base.Sleep();
    }
}
