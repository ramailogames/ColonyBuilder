using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    public static PawnManager instance;

    public List<PawnBase> pawnList = new List<PawnBase>();
    public List<PawnBase> idlePawnList = new List<PawnBase>();

    public List<Transform> randomPosInMap = new List<Transform>();


    public PawnBase selectedPawn;
    [HideInInspector] public bool isFightingMode = false;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
      
    }

    public void AddPawnToList(PawnBase pawn)
    {
        pawnList.Add(pawn);
    }
    public void RemovePawnList(PawnBase pawn)
    {
        pawnList.Remove(pawn);
    }

    public void AddPawnToIdleList(PawnBase pawn)
    {
        idlePawnList.Add(pawn);
    }
    public void RemovePawnIdleList(PawnBase pawn)
    {
        idlePawnList.Remove(pawn);
    }

    public void SelectPawn()
    {
        for (int i = 0; i < idlePawnList.Count; i++)
        {
            selectedPawn = idlePawnList[i];
            selectedPawn.isIdle = false;
            RemovePawnIdleList(selectedPawn);
            break;
        }
    }

    public void FightModeBtn()
    {
        //reset resource
        ResourceBase[] resources = FindObjectsOfType<ResourceBase>();
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].ResetResource();
        }


        //call pawnbase - switch to fightmode
        for (int i = 0; i < pawnList.Count; i++)
        {
            pawnList[i].SetModeToFight();
            pawnList[i].GoToBarrack();
        }
        isFightingMode = true;
        GameManager.instance.SwitchButtons();


    }

    public void NormalModeBtn()
    {
        for (int i = 0; i < pawnList.Count; i++)
        {
            pawnList[i].SetModeToNormal();
           
        }
        isFightingMode = false;
        GameManager.instance.SwitchButtons();

    }

    public void AllPawnTryFindEnemy()
    {
        for (int i = 0; i < pawnList.Count; i++)
        {
            pawnList[i].TryFindEnemyPawn();
            //pawnList[i].GoToBarrack();
        }
    }

    public void AllPawnRegroupAtCamp()
    {
        for (int i = 0; i < pawnList.Count; i++)
        {
            //pawnList[i].TryFindEnemyPawn();
            pawnList[i].GoToBarrack();
        }
    }




}

