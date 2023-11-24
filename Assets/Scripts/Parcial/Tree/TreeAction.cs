using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAction : ITreeNode
{
    Action action;
    public TreeAction(Action action)
    {
        this.action = action;
    }
    public void Execute()
    {
        if (action != null)
        {
            action();
        }
    }
}
