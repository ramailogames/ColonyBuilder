using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public void SelectPawn(int number)
    {
        PawnManager.instance.pawnList[number].SetModeToFight();
        PawnManager.instance.selectedPawn = PawnManager.instance.pawnList[number];
    }
}
