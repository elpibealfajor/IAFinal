using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    //Line Of Sight
    public float range;
    public float angle = 120f;
    public LayerMask layerMask;

    //chase
    public float speed;
    public float chaseSpeed;

    //patrolling
    public Transform[] wPoints;
    public int current = 0;
    public int patrollsCompleted =0;
    public int patrollsToComplete = 8;
    public float amountOfIdleTime = 4f;

    //RandomPatrolling
    public bool isRandomPatrollingOn = false;
    public Transform currentWaypointTransform;
    public Dictionary<Transform, int> dic;
    public Roulette roulette;

    //Vision of the player
    public Transform target;

    //decoy
    public Transform decoy;
    public bool isDistracted;

    //Shoot
    public Transform targetToShoot;
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float shootInterval = 1f;
    public float shootTimer = 0f;

    //flee
    public Transform remoteBall;
    public EntityModel enemyEntity;
    public Rigidbody rb;
    Isteering steering;
    public float RemoteBallDetectionRange = 10f;

    //obstacle avoidance
    Isteering obsAvoidance;
    public LayerMask mask;
    public int maxObs;
    public float angleToAvoid;
    public float radius;
    public float multiplier;
    private void Awake()
    {
        enemyEntity = GetComponent<EntityModel>();
        rb = GetComponent<Rigidbody>();
        roulette = new Roulette();
        dic = new Dictionary<Transform, int>();
        foreach (Transform wpointTransform in wPoints)
        {
            dic.Add(wpointTransform, 25);
        }
        //obstacle avoidance
        obsAvoidance = new ObstacleAvoidance(transform, mask, maxObs, angleToAvoid, radius);
    }
    void Start()
    {
        currentWaypointTransform = roulette.Run<Transform>(dic);
    }
    public void Shoot()
    {
        if (targetToShoot != null)
        {
            var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position + transform.forward, Quaternion.identity) as GameObject;
            Vector3 dir = (targetToShoot.position - projectileSpawnPoint.position);
            projectile.GetComponent<Rigidbody>().velocity = dir * projectileSpeed;
            Destroy(projectile, 1f);
        }
        else
        {
            return;
        }
    }
    public void Move(Transform target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position
             , speed * Time.deltaTime);
        transform.LookAt(target);
    }
    //public void Move(Transform target, float speed)
    //{
    //    Vector3 dirAvoidance = obsAvoidance.GetDir();
    //    Vector3 dir = ((target.position - transform.position).normalized + dirAvoidance * multiplier).normalized;
    //    enemyEntity.Move(dir);

    //    transform.LookAt(target);
    //    Debug.Log("me muevo");
    //}
    #region
    //public void Chase(Vector3 playerPosition, Transform player)
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, playerPosition
    //        , chaseSpeed * Time.deltaTime);
    //    transform.LookAt(player);
    //}
    //public void Patrol(Vector3 currentWaypointPosition)
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, currentWaypointPosition, speed * Time.deltaTime);
    //}
    //public void RandomPatrol(Transform waypointTransform)
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, waypointTransform.position, speed * Time.deltaTime);
    //}
    //public void LookDirPatrol(Transform currentWaypointTransform)
    //{
    //    transform.LookAt(currentWaypointTransform);
    //}
    #endregion
    public bool PatrollCompleted()
    {
        if (patrollsCompleted == patrollsToComplete)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator resetPatrollsCompleted()
    {
        yield return new WaitForSeconds(amountOfIdleTime);
        patrollsCompleted = 0;
    }

    public bool IsInRange(Transform target)
    {
        // lo mismo que hacer b-a
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > range)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool IsInAngle(Transform target)
    {
        Vector3 foward = transform.forward;
        Vector3 dirToTarget = (target.position - transform.position);
        float angleToTarget = Vector3.Angle(foward, dirToTarget);
        if (angle / 2 > angleToTarget)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool IsInVision(Transform target)
    {
        Vector3 diff = (target.position - transform.position);
        Vector3 dirToTarget = diff.normalized;
        float distanceToTarget = diff.magnitude;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, dirToTarget, out hit, distanceToTarget, layerMask))
        {
            return false;
        }
        return true;
    }
    public bool GetIfTargetIsViewed()
    {
        if (IsInRange(target) && IsInAngle(target) && IsInVision(target))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsInAttackRange()
    {
        if (Vector3.Distance(transform.position, targetToShoot.position) <= range / 2)
        {
            //Debug.Log("esta a rango de tiro");
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetIfDecoyIsViewed()
    {
        if (decoy != null && IsInRange(decoy) && IsInAngle(decoy) && IsInVision(decoy))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetIfRemoteBallIsInAngle()
    {
        if (remoteBall != null)
        {
            float distance = Vector3.Distance(remoteBall.position, transform.position);
            return distance <= RemoteBallDetectionRange;
        }
        else
        {
            return false;
        }
    }
    public void Flee()
    {
        Vector3 dir = steering.GetDir();
        enemyEntity.Move(dir);
        enemyEntity.LookDir(dir);
    }
    public void InitializeSteering()
    {
        var flee = new Flee(transform, remoteBall.transform);
        steering = flee;
    }
    public void SetEyesVisuals()
    {
        //efectos visuales al estar en vision
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0,-angle / 2, 0) * transform.forward * range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RemoteBallDetectionRange);
    }
}
