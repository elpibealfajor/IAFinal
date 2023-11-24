using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControlBall : MonoBehaviour
{
    public Transform target;
    Isteering steering;

    Isteering obsAvoidance;
    public LayerMask mask;
    public int maxObs;
    public float angle;
    public float radius;
    public float multiplier;

    EntityModel remoteBall;
    public float lifeTime;
    public float range;


    private void Awake()
    {
        remoteBall = GetComponent<EntityModel>();
        CheckCollision();
        Destroy(gameObject, lifeTime); 
        InitializeSteering();
    }
    private void Start()
    {

    }
    private void CheckCollision()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, range);
        foreach (Collider col in collider)
        {
            if (col.gameObject.tag == "Enemy")
            {
                var enemyModel = col.GetComponent<EnemyModel>();
                target = enemyModel.transform;               
                enemyModel.remoteBall = this.transform;
            }
        }
    }
    void InitializeSteering()
    {
        var seek = new Seek(transform, target.transform);
        obsAvoidance = new ObstacleAvoidance(transform, mask, maxObs, angle, radius);
        steering = seek;
    }
    private void Update()
    {
        Vector3 dirAvoidance = obsAvoidance.GetDir();
        Vector3 dir = (steering.GetDir() + dirAvoidance * multiplier).normalized;
        remoteBall.Move(dir);
        //Vector3 dir = steering.GetDir();
        //remoteBall.LookDir(dir);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
