using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 10f;

    private float xInput;
    private float zInput;

    //decoy
    public GameObject decoy;
    public float decoyCooldownTime;
    private float decoyCooldown = 0f;

    //RemoteControlBall
    public GameObject remoteBall;
    public float remoteBallCooldownTime;
    private float remoteBallCooldown = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInputs();
        DecoySpawn();
        RemoteControlSpawn();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void ProcessInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
    }
    private void Move()
    {
        rb.AddForce(new Vector3(xInput, 0f, zInput) * moveSpeed);        
    }
    private void DecoySpawn()
    {
        decoyCooldown = decoyCooldown - Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R) && decoyCooldown <= 0)
        {
            Instantiate(decoy,transform.position,transform.rotation);
            decoyCooldown = decoyCooldownTime;
        }
        else
        {
            return;
        }
    }
    private void RemoteControlSpawn()
    {
        remoteBallCooldown = remoteBallCooldown - Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F) && remoteBallCooldown <= 0)
        {
            Instantiate(remoteBall, transform.position, transform.rotation);
            remoteBallCooldown = remoteBallCooldownTime;
        }
        else
        {
            return;
        }
    }
}
