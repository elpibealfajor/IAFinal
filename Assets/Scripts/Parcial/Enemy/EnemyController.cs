using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    FSM<EnemyStates> fsm;
    ITreeNode root;
    EnemyModel model;

    Dictionary<EnemyStates, int> dic;
    Roulette roulette;
    private void Awake()
    {
        model = GetComponent<EnemyModel>();
        InitializedFSM();
        InitializedTree();

        roulette = new Roulette();
        dic = new Dictionary<EnemyStates, int>();
        dic.Add(EnemyStates.EnemyAtackDecoy, 50);
        dic.Add(EnemyStates.Distracted, 50);
    }
    public void InitializedFSM()
    {
        var list = new List<EnemyStateBase<EnemyStates>>();
        fsm = new FSM<EnemyStates>();

        var chase = new EnemyChaseState<EnemyStates>(EnemyStates.Patroling);
        var patrol = new EnemyPatrolState<EnemyStates>(EnemyStates.chase);
        var distracted = new EnemyDistractedState<EnemyStates>(EnemyStates.Distracted);
        var shoot = new EnemyAttackState<EnemyStates>(EnemyStates.Shoot);
        var atackDecoy = new EnemyAttackDecoyState<EnemyStates>(EnemyStates.EnemyAtackDecoy);
        var flee = new EnemyFleeState<EnemyStates>(EnemyStates.EnemyFlee);
        var idle = new EnemyIdleState<EnemyStates>(EnemyStates.idle);

        list.Add(chase);
        list.Add(patrol);
        list.Add(distracted);
        list.Add(shoot);
        list.Add(atackDecoy);
        list.Add(flee);
        list.Add(idle);

        chase.AddTransition(EnemyStates.Patroling, patrol);
        chase.AddTransition(EnemyStates.Distracted, distracted);
        chase.AddTransition(EnemyStates.Shoot, shoot);
        chase.AddTransition(EnemyStates.EnemyFlee, flee);
        patrol.AddTransition(EnemyStates.chase, chase);
        patrol.AddTransition(EnemyStates.Distracted, distracted);
        patrol.AddTransition(EnemyStates.EnemyAtackDecoy, atackDecoy);
        patrol.AddTransition(EnemyStates.EnemyFlee, flee);
        patrol.AddTransition(EnemyStates.idle, idle);
        distracted.AddTransition(EnemyStates.Patroling, patrol); //como distracted no tiene transicion a attack decoy, arregle que se sobreescriban
        distracted.AddTransition(EnemyStates.chase, chase);  //como distracted no tiene transicion a flee, se puede combear decoy + remote
        shoot.AddTransition(EnemyStates.Patroling, patrol);
        shoot.AddTransition(EnemyStates.chase, chase);
        shoot.AddTransition(EnemyStates.Distracted, distracted);
        shoot.AddTransition(EnemyStates.EnemyFlee, flee);
        atackDecoy.AddTransition(EnemyStates.Patroling, patrol);
        atackDecoy.AddTransition(EnemyStates.chase, chase);
        atackDecoy.AddTransition(EnemyStates.Shoot, shoot);
        atackDecoy.AddTransition(EnemyStates.EnemyFlee, flee);
        flee.AddTransition(EnemyStates.Patroling, patrol);
        flee.AddTransition(EnemyStates.chase, chase);
        flee.AddTransition(EnemyStates.Distracted, distracted);
        flee.AddTransition(EnemyStates.EnemyAtackDecoy, atackDecoy);
        flee.AddTransition(EnemyStates.Shoot, shoot);
        idle.AddTransition(EnemyStates.chase, chase);
        idle.AddTransition(EnemyStates.Shoot, shoot);
        idle.AddTransition(EnemyStates.Patroling, patrol);
        idle.AddTransition(EnemyStates.EnemyFlee, flee);
        idle.AddTransition(EnemyStates.EnemyAtackDecoy, atackDecoy);
        idle.AddTransition(EnemyStates.Distracted, distracted);

        for (int i = 0; i < list.Count; i++)
        {
            list[i].InitializedState(model, fsm);
        }

        fsm.SetInit(patrol);
    }
    public void InitializedTree()
    {
        //dic = new Dictionary<TreeAction, int>();
        //roulette = new Roulette();

        //actions
        var patrol = new TreeAction(ActionPatrol);
        var chase = new TreeAction(ActionChase);
        var distracted = new TreeAction(ActionDistracted);
        var shoot = new TreeAction(ActionShoot);
        var flee = new TreeAction(ActionFlee);
        var idle = new TreeAction(ActionIdle);
        //var shootDecoy = new TreeAction(ActionAttackDecoy);

        //roulete wheel dic
        //dic.Add(distracted, 50);
        //dic.Add(shootDecoy, 50);

        //questions
        //// la ptm el roulete no puede ir aca entra solo una vez
        //var isTheDecoy = new TreeQuestion(IsDecoy, roulette.Run<TreeAction>(dic), patrol); 
        var isInIdle = new TreeQuestion(IsInIdle, idle, patrol);
        var isTheDecoy = new TreeQuestion(IsDecoy, distracted, isInIdle);
        var isThePlayerInRange = new TreeQuestion(IsInShootRange, shoot, chase);
        var isThePlayer = new TreeQuestion(IsThePlayer, isThePlayerInRange, isTheDecoy);
        var isRemoteBallInAngle = new TreeQuestion(IsRemoteBallInAngle, flee, isThePlayer);

        root = isRemoteBallInAngle;
    }
    bool IsThePlayer()
    {
        return model.GetIfTargetIsViewed();
    }
    bool IsDecoy()
    {
        return model.GetIfDecoyIsViewed();
    }
    bool IsInShootRange()
    {
        return model.IsInAttackRange();
    }
    bool IsRemoteBallInAngle()
    {
        return model.GetIfRemoteBallIsInAngle();
    }
    bool IsInIdle()
    {
        return model.PatrollCompleted();
    }

    void ActionPatrol()
    {
        fsm.Transitions(EnemyStates.Patroling);
    }
    void ActionChase()
    {
        fsm.Transitions(EnemyStates.chase);
    }
    void ActionDistracted()
    {
        //fsm.Transitions(EnemyStates.Distracted);
        fsm.Transitions(roulette.Run<EnemyStates>(dic));
    }
    void ActionShoot()
    {
        fsm.Transitions(EnemyStates.Shoot);
    }
    void ActionFlee()
    {
        fsm.Transitions(EnemyStates.EnemyFlee);
    }
    void ActionIdle()
    {
        fsm.Transitions(EnemyStates.idle);

    }
    //void ActionAttackDecoy()
    //{
    //    fsm.Transitions(EnemyStates.EnemyAtackDecoy);
    //}
    void Update()
    {
        fsm.OnUpdate();
        root.Execute();
    }
}
