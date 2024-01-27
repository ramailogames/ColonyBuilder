using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PawnBase : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] public Transform target;
    public GameObject myAttackStateTarget;
    public SpriteRenderer sr;
    GameObject currentAttackStateTarget;
    public Transform myGfx;
    Rigidbody2D rb;
    Material defMat;
    Camera mainCamera;

    [Header("Properties")]
    public PawnData myData;
    public bool isIdle = true; // not working or doing any task
    public bool isTaskRunning = false; // not working or doing any task
    private float moveSpeed;
    public GameObject attackIcon;
   
    public enum MyPawnType
    {
        Player,
        Hostile
    }
    public MyPawnType myPawnType;


    [Header("Heath and Attacking")]
    public Transform checkEnemyPoint;
    private float currentHp;
    private bool isAttacking = false;
    

    
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
        defMat = sr.material;
    }



    private void Start()
    {
        if(myPawnType == MyPawnType.Player)
        {
            PawnManager.instance.AddPawnToList(this); // Add to pawn list
            PawnManager.instance.AddPawnToIdleList(this); //Add to pawn idle list
            WalkToRandom(); //Walk to random
            Regen();
        }
        if (myPawnType == MyPawnType.Hostile)
        {
            attackIcon.SetActive(true);
            isTaskRunning = false;
            isIdle = false;
            myState = PawnState.FightState;
            target = GameManager.instance.barrack;
            StartCoroutine(Enum_TryFindEnemyPawn());
        }

        moveSpeed = myData.walkSpeed;
        currentHp = myData.maxHealth;
        InvokeRepeating("UpdatePath", 0f, 0.5f);

     
    }

    void UpdatePath()
    {
        if(target == null && myState == PawnState.FightState)
        {
            /* if(myPawnType == MyPawnType.Hostile)
             {
                 StartCoroutine(Enum_TryFindEnemyPawn());
                 return;
             }
             target = GameManager.instance.barrack;*/

            StartCoroutine(Enum_TryFindEnemyPawn());
        }

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
                  MoveToTargetFightMode();
                  break;

              case PawnState.NormalState:
                  MoveToTarget();
                  break;
          }

 
        CheckFlip();
        CheckEnemy();

    }

     void MoveToMouse()
    {
       
        if(PawnManager.instance.selectedPawn != this)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            if(currentAttackStateTarget != null)
            {
                Destroy(currentAttackStateTarget);
            }

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
    void MoveToTargetFightMode()
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

    public void SetModeToFight()
    {
        attackIcon.SetActive(true);
        isTaskRunning = false;
        isIdle = false;
        myState = PawnState.FightState;
        PawnManager.instance.RemovePawnIdleList(this);
    }

    public void SetModeToNormal()
    {
        attackIcon.SetActive(false);
        isTaskRunning = false;
        isIdle = true;
        myState = PawnState.NormalState;
        PawnManager.instance.AddPawnToIdleList(this);
    }

    public void GoToBarrack()
    {
        if (GameManager.instance.barrack == null) // check if barrack is available
        {
            return;
        }
        SetTarget(GameManager.instance.barrack);
    }

    #region Find and Fight Pawn

    bool haveFoundEnemyPawn = false;
    PawnBase enemyPawn;

    public void TryFindEnemyPawn()
    {
        StartCoroutine(Enum_TryFindEnemyPawn());
    }
    IEnumerator Enum_TryFindEnemyPawn()
    {
        yield return new WaitForEndOfFrame();
        if (haveFoundEnemyPawn)
        {
            yield break;
        }

        PawnBase[] allPawnList = FindObjectsOfType<PawnBase>();
        yield return new WaitForSeconds(0.1f);
        switch (myPawnType)
        {
            case MyPawnType.Player:
                for (int i = 0; i < allPawnList.Length; i++)
                {
                    if(allPawnList[i].myPawnType == MyPawnType.Hostile)
                    {
                        enemyPawn = allPawnList[i];
                        Debug.Log("Found enemy " + allPawnList[i].name);
                        break;
                    }
                   
                }
                break;

            case MyPawnType.Hostile:
                for (int i = 0; i < allPawnList.Length; i++)
                {
                    if (allPawnList[i].myPawnType == MyPawnType.Player)
                    {
                        enemyPawn = allPawnList[i];
                        Debug.Log("Found enemy " + allPawnList[i].name);
                        break;
                    }
                  
                }
                break;
        }

        yield return new WaitForSeconds(1f);

        if(enemyPawn == null)
        {
            yield break;
        }
        SetTarget(enemyPawn.transform);
    }



    #endregion


    #region Health, Take Damage and Give Damage

    IEnumerator Enum_CheckEnemyToDamage()
    {
        yield return new WaitForEndOfFrame();

        if (isAttacking)
        {
            yield break;
        }
        isAttacking = true;

        DamageEnemyNearBy();

        yield return new WaitForSeconds(1f);
        isAttacking = false;


    }

    void CheckEnemy()
    {
        Collider2D enemyInRange = Physics2D.OverlapCircle(checkEnemyPoint.position, myData.checkEnemyRadius, myData.whatIsEnemy);

        if (enemyInRange == null)
        {
            return; // no enemy found
        }

        StartCoroutine(Enum_CheckEnemyToDamage());

    }
    public void DamageEnemyNearBy()
    {
        Collider2D[] enemyInRange = Physics2D.OverlapCircleAll(checkEnemyPoint.position, myData.checkEnemyRadius, myData.whatIsEnemy);

        if(enemyInRange == null)
        {
            return; // no enemy found
        }

        for (int i = 0; i < enemyInRange.Length; i++)
        {
            enemyInRange[i].GetComponent<PawnBase>().TakeDamage(myData.myStrength);
            break;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if(myPawnType == MyPawnType.Player)
        {
            currentHp -= damageAmount;
        }

        if (myPawnType == MyPawnType.Hostile)
        {
            float totalDamageAmount = damageAmount + GameManager.instance.GlobalDamageAdd;
            currentHp -= totalDamageAmount;
        }

       
        sr.material = myData.whiteMat;

        Invoke("ResetMat", .25f);
        Debug.Log(gameObject.name + " taken damage by" + damageAmount);

        if(currentHp <= 0)
        {
            PawnManager.instance.RemovePawnList(this);
            PawnManager.instance.RemovePawnIdleList(this);
            Instantiate(myData.lootDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Heal()
    {
        if (!Game.hasPurchasedCampFire)
        {
            return;
        }

        if (currentHp >= myData.maxHealth)
        {
            return;
        }
        currentHp += 5;

    }
    void ResetMat()
    {
        sr.material = defMat;
    }

    void Regen()
    {
        InvokeRepeating("Heal", 5f, 3f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkEnemyPoint.position, myData.checkEnemyRadius);
    }
    #endregion
}

