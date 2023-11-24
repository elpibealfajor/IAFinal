using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDecoyState<T> : EnemyStateBase<T>
{
    T input;

    public EnemyAttackDecoyState(T input)
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
        model.shootTimer += Time.deltaTime;
        if (model.shootTimer >= model.shootInterval)
        {
            model.targetToShoot = model.decoy;
            model.Shoot();
            model.shootTimer = 0f;
        }
        model.targetToShoot = model.target;
    }
    public override void Sleep()
    {
        base.Sleep();
    }
}
