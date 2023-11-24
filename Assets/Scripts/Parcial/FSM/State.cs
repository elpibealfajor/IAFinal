using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> : IState<T>
{
    Dictionary<T, IState<T>> transition = new Dictionary<T, IState<T>>();
    public IState<T> GetTransition(T input)
    {
        if (transition.ContainsKey(input))
        {
            return transition[input];
        }
        //return default(IState<T>);
        return null;
    }
    public void AddTransition(T input, IState<T> state)
    {
        transition[input] = state;
    }
    public void RemoveTransition(IState<T> state)
    {
        foreach (var item in transition)
        {
            if (item.Value == state)
            {
                transition.Remove(item.Key);
                break;
            }
        }
    }

    public void RemoveTransition(T input)
    {
        if (transition.ContainsKey(input))
        {
            transition.Remove(input);
        }
    }
    public virtual void Awake()
    {
        
    }
    public virtual void Execute()
    {

    }
    public virtual void Sleep()
    {

    }

}
