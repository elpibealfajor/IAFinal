using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    public Transform target;
    public float multiplier;
    public float time;
    public LayerMask mask;
    public float angle;
    public float radius;
    public int maxObstacles;
    EntityModel dogEntity;
    Isteering steering;
    Isteering obstacleAvoidance;

    //public float speed;
    //public float radiusToFlocking;
    //Rigidbody rb;
    //public Vector3 Position => transform.position;

    //public Vector3 Front => transform.forward;

    //public float Radius => radiusToFlocking;

    void InitializeSteering()
    {
        var seek = new Seek(transform, target);
        obstacleAvoidance = new ObstacleAvoidance(transform, mask, maxObstacles, angle, radius);
        steering = seek;
    }
    private void Awake()
    {
        dogEntity = GetComponent<EntityModel>();
        InitializeSteering();
        //rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //SceneManager.LoadScene("SampleScene");
            Debug.Log("colisione con el player");
        }
    }
    private void Update()
    {
        Vector3 dirAvoidance = obstacleAvoidance.GetDir();
        Vector3 dir = (steering.GetDir() + dirAvoidance * multiplier).normalized;
        dogEntity.Move(dir);
        dogEntity.LookDir(dir);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * radius);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(Position, radiusToFlocking);
    }

    //public void Move(Vector3 dir)
    //{
    //    dir *= speed;
    //    dir.y = rb.velocity.y;
    //    rb.velocity = dir;
    //}

    //public void LoockDir(Vector3 dir)
    //{
    //    dir.y = 0;
    //    transform.forward = dir;
    //}
}
