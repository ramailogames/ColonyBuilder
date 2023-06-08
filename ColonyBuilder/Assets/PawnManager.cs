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
}
