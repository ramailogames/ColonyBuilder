using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float GlobalDamageAdd = 0;

    [Header("Resource")]
    public int woodCount;
    public int rockCount;
    public int goldCount;
    public TextMeshProUGUI woodCountTxt;
    public TextMeshProUGUI rockCountTxt;
    public TextMeshProUGUI goldCountTxt;

    public Transform barrack;
    public Transform resourcePoint;
    public Transform campBase;

   

    [Header("UI")]
    public GameObject fightModeBtn;
    public GameObject normalModeBtn;
    public GameObject tutorialPanel;
    public GameObject messagePanel;
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI messageText;
    public GameObject gameoverView;

   
 

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckGameover", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchButtons()
    {
        if (PawnManager.instance.isFightingMode)
        {
            normalModeBtn.SetActive(true);
            fightModeBtn.SetActive(false);
            return;
        }

        normalModeBtn.SetActive(false);
        fightModeBtn.SetActive(true);
    }

    public void AddWood(int count)
    {
        woodCount += count;
        woodCountTxt.text = woodCount.ToString();
    }

    public void RemoveWood(int count)
    {
        woodCount -= count;
        woodCountTxt.text = woodCount.ToString();
    }

    public void AddRock(int count)
    {
        rockCount += count;
        rockCountTxt.text = rockCount.ToString();
    }

    public void RemoveRock(int count)
    {
        rockCount -= count;
        rockCountTxt.text = rockCount.ToString();
    }

    public void AddGold(int count)
    {
        goldCount += count;
        goldCountTxt.text = goldCount.ToString();
    }
    public void RemoveGold(int count)
    {
        goldCount -= count;
        goldCountTxt.text = goldCount.ToString();
    }

    public void WriteTutorialText(string text)
    {
        tutorialText.text = text;
    }

    public void SetTutorialPanel(bool state)
    {
        switch (state)
        {
            case true:
                tutorialPanel.SetActive(true);
                break;

            case false:
                tutorialPanel.SetActive(false);
                break;
        }
    }


    public void WriteMessageText(string text)
    {
        messageText.text = text;
    }

    public void SetMessagePanel(bool state)
    {
        switch (state)
        {
            case true:
                messagePanel.SetActive(true);
                break;

            case false:
                messagePanel.SetActive(false);
                break;
        }
    }

    public void CheckGameover()
    {
        PawnBase[] countPawn = FindObjectsOfType<PawnBase>();
        List<PawnBase> countVillager = new List<PawnBase>();

        for (int i = 0; i < countPawn.Length; i++)
        {
            if(countPawn[i].GetComponent<PawnBase>().myPawnType == PawnBase.MyPawnType.Player)
            {
                countVillager.Add(countPawn[i]);
            }
        }

        Debug.Log(countVillager.Count);

        if(countVillager.Count <= 0)
        {
            Debug.Log("Gameover");
            gameoverView.SetActive(true);
        }
    }


   
}
