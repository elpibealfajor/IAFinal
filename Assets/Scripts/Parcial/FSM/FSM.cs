using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    IState<T> current;

    public FSM(IState<T> initState)
    {
        SetInit(initState);
    }
    public FSM()
    {

    }
    public void SetInit(IState<T> initState)
    {
        current = initState;
        current.Awake();
    }
    public void OnUpdate()
    {
        if (current != null)
        {
            current.Execute();
        }
    }
    public void Transitions(T input)
    {
        IState<T> newState = current.GetTransition(input);
        if (newState == null) 
        {
            return;
        }
        current.Sleep();
        current = newState;
        current.Awake();
    }
}
