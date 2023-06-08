using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PawnBase : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] public Transform target;
    public GameObject myAttackStateTarget;
    GameObject currentAttackStateTarget;
    public Transform myGfx;
    Rigidbody2D rb;
    Camera mainCamera;

    [Header("Properties")]
    public PawnData myData;
    public bool isIdle = true; // not working or doing any task
    public bool isTaskRunning = false; // not working or doing any task
    private float moveSpeed;


    [Header("Path Finding")]
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;

    [Header("Pawn State")]
    public PawnState myState;
    public enum PawnState
    {
        NormalState,
        FightState
    }


    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }



    private void Start()
    {
        moveSpeed = myData.walkSpeed;
        PawnManager.instance.AddPawnToList(this); // Add to pawn list
        PawnManager.instance.AddPawnToIdleList(this); //Add to pawn idle list

        WalkToRandom(); //Walk to random

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

        switch (myState)
        {
            case PawnState.FightState:
                MoveToMouse();
                break;

            case PawnState.NormalState:
                MoveToTarget();
                break;
        }

        CheckFlip();

    }

     void MoveToMouse()
    {
       
        if(PawnManager.instance.selectedPawn != this)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Destroy(currentAttackStateTarget);

            Debug.Log("Clicked");
            Vector3 mouseClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseClick.z = transform.position.z;
            
            currentAttackStateTarget = Instantiate(myAttackStateTarget, transform.position, Quaternion.identity);
            currentAttackStateTarget.transform.position = mouseClick;

            target = currentAttackStateTarget.transform;
            // You can perform additional logic here based on the mouse click position
        }

        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            Debug.Log("Reached target");
            rb.velocity = Vector2.zero;
            reachedEndOfPath = true;

            /* if (isIdle) // if not working or doing task
             {
                 isTaskRunning = false;
                 WalkToRandom(); //Walk to random
             }
             else
             {
                 isTaskRunning = true;
             }*/
           

            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;

        Vector2 force = direction * myData.runSpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < myData.nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }

    void MoveToTarget()
    {
        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            Debug.Log("Reached target");
            rb.velocity = Vector2.zero;
            reachedEndOfPath = true;

            if (isIdle) // if not working or doing task
            {
                isTaskRunning = false;
                WalkToRandom(); //Walk to random
            }
            else
            {
                isTaskRunning = true;
            }


            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;

        Vector2 force = direction * moveSpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < myData.nextWayPointDistance)
        {
            currentWayPoint++;
        }
    }
  
    private void CheckFlip()
    {
        /*if (isTaskRunning) // do not flip, if doing task, might fix the bug?
        {
            return;
        }*/

        if (rb.velocity.x >= 0.01f)
        {
            myGfx.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if ((rb.velocity.x <= -0.01f))
        {
            myGfx.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void WalkToRandom()
    {
        moveSpeed = myData.walkSpeed; // walk speed
        int randomIndex = Random.Range(0, PawnManager.instance.randomPosInMap.Count);
        Transform randomPos = PawnManager.instance.randomPosInMap[randomIndex];
        target = randomPos;
    }

    public void SetTarget(Transform newTarget)
    {
        moveSpeed = myData.runSpeed; // run speed
        target = newTarget;
    }

    public void MakePawnIdle()
    {
        isIdle = true;
        PawnManager.instance.AddPawnToIdleList(this);
        PawnManager.instance.selectedPawn = null;
    }

}
