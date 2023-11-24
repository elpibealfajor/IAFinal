using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeQuestion : ITreeNode
{
    Func<bool> question;
    ITreeNode tNode;
    ITreeNode fNode;
    public TreeQuestion(Func<bool> question, ITreeNode TrueNode, ITreeNode FalseNode)
    {
        this.question = question;
        tNode = TrueNode;
        fNode = FalseNode;
    }
    public void Execute()
    {
        if (question())
        {
            tNode.Execute();
        }
        else
        {
            fNode.Execute();
        }
    }
}