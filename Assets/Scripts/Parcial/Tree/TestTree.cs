using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    public int life;
    public int bullets;
    public bool enemyViewed;

    ITreeNode rootNode;
    public void InitializeTree()
    {
        var dead = new TreeAction(Dead);
        var reload = new TreeAction(Reload);
        var shoot = new TreeAction(Shoot);
        var patrol = new TreeAction(Patrol);

        var hasAmmo = new TreeQuestion(HasBullet, shoot, reload);
        var HasLoadedGun = new TreeQuestion(HasBullet, patrol, reload);
        var enemyView = new TreeQuestion(IsEnemyViewed,hasAmmo,HasLoadedGun);

        var hasLife = new TreeQuestion(HasLife,enemyView,dead);

        rootNode = hasLife;
    }
    private void Awake()
    {
        InitializeTree();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            rootNode.Execute();
        }
    }
    public bool HasLife()
    {
        if (life > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HasBullet()
    {
        if (bullets > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsEnemyViewed()
    {
        return enemyViewed;
    }

    public void Dead()
    {
        print("Dead");
    }
    public void Reload()
    {
        print("Reload");
    }
    public void Shoot()
    {
        print("Shoot");
    }
    public void Patrol()
    {
        print("Patrol");
    }
}
