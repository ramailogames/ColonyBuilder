using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PawnBase : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public Transform myGfx;
    Rigidbody2D rb;

    [Header("Properties")]
    public PawnData myData;

    [Header("Path Finding")]
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        
        
    }
    private void Start()
    {
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error) 
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;

        Vector2 force = direction * myData.moveSpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < myData.nextWayPointDistance)
        {
            currentWayPoint++;
        }


        if (force.x >= 0.01f)
        {
            myGfx.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if ((force.x <= -0.01f))
        {
            myGfx.localScale = new Vector3(1f, 1f, 1f);
        }

    }

}
