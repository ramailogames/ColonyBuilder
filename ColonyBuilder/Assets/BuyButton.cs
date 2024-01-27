using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public float GoldCost, WoodCost, StoneCost;
    Button button;

    public bool isAlreadyPurchased = false;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        InvokeRepeating("CheckGold", 0f, 0.5f);
    }

    void CheckGold()
    {
        if (isAlreadyPurchased)
        {
            return;
        }

        if(GameManager.instance.goldCount >= GoldCost && GameManager.instance.woodCount >= WoodCost && GameManager.instance.rockCount >= StoneCost)
        {
            button.interactable = true;
            return;
        }

        button.interactable = false;
    }
}
