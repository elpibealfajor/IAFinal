using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyController : MonoBehaviour
{
    public float lifeTime;
    public float range;
    private void Awake()
    {
        CheckCollision();
        Destroy(gameObject, lifeTime);
    }
    private void CheckCollision()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, range);
        foreach (Collider col in collider)
        {
            if (col.gameObject.tag == "Enemy")
            {
                var enemyModel = col.GetComponent<EnemyModel>();
                enemyModel.decoy = this.transform;               
            }
            else
            {
                Destroy(this);
            }
        }
    }
    
}
