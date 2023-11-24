using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Dog : MonoBehaviour, IBoid
{
    public float speed;
    public float rotSpeed;
    public float radius;
    Rigidbody _rb;
    Isteering obstacleAvoidance;

    public float multiplierToAvoid;
    public LayerMask mask;
    public float angle;
    public float radiusToObsAvoidance;
    public int maxObstacles;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        obstacleAvoidance = new ObstacleAvoidance(transform, mask, maxObstacles, angle, radiusToObsAvoidance);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //SceneManager.LoadScene("SampleScene");
            //Debug.Log("colisione con el player");
        }
    }
    public Vector3 Position => transform.position;

    public Vector3 Front => transform.forward;

    public float Radius => radius;
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
    public void Move(Vector3 dir)
    {
        Vector3 dirAvoidance = obstacleAvoidance.GetDir();
        dir *= speed;
        dir.y = _rb.velocity.y;
        dir += dirAvoidance;
        //dir = (dir + dirAvoidance * multiplierToAvoid).normalized;
        _rb.velocity = dir;
    }
    public void LookDir(Vector3 dir)
    {
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Position, radius);
    }
}
