using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> 
{
    public IState<T> GetTransition(T input);
    public void AddTransition(T input, IState<T> state);
    public void RemoveTransition(IState<T> state);
    public void RemoveTransition(T input);
    void Awake();
    void Execute();
    void Sleep();
}
