using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject shopView;
    public GameObject barrack;
    public GameObject barrackPosition;
    public GameObject campFire;

    public TextMeshProUGUI campFirePurchaseText;
    public TextMeshProUGUI barrackPurchaseText;
    //public TextMeshProUGUI barrackPurchaseText;

    public Button campFirePurchaseBtn;
    public Button barrackPurchaseBtn;

    public GameObject attackBtn;

    public GameObject pawn;

    public Button[] purchaseBtns;
    public BuyButton campfireBuyButton;

    private void Awake()
    {
        instance = this;
    }

    public void SetShowView(bool state)
    {
        switch (state)
        {
            case true:
                shopView.SetActive(true);
                break;

            case false:
                shopView.SetActive(false);
                break;
        }
    }
    public void BuyBarrack()
    {
        if (GameManager.instance.woodCount < 10 || GameManager.instance.rockCount < 5)
        {
            Debug.Log("Not enough Resources");
            return;
        }
        GameManager.instance.RemoveWood(10);
        GameManager.instance.RemoveRock(5);

        barrack.SetActive(true);
        GameManager.instance.barrack = barrackPosition.transform;
        barrackPurchaseText.text = "Purchased";
        barrackPurchaseBtn.interactable = false;
        attackBtn.SetActive(true);


        if(Game.hasPurchasedBarrack == true)
        {
            return;
        }
        Game.hasPurchasedBarrack = true;
        //set purchase button active after first purchase
        for (int i = 0; i < purchaseBtns.Length; i++)
        {
            purchaseBtns[i].interactable = true;
        }
        GameManager.instance.SetTutorialPanel(true);
        GameManager.instance.WriteTutorialText("You can change whole colony Pawn's state with Attack/Normal Button, - Fight Btn Forces to find enemy and attack, - Regroup Btn Forces to regroup ");
        Invoke("MineResourcesTutorial", 15f);
        FindObjectOfType<TimeManager>().SpawnWave();
      
    }

    public void BuyCampFire()
    {
        if (GameManager.instance.woodCount < 10 || GameManager.instance.rockCount < 10)
        {
            Debug.Log("Not enough Resources");
            return;
        }

        GameManager.instance.RemoveWood(10);
        GameManager.instance.RemoveRock(10);

        campFire.SetActive(true);
        GameManager.instance.campBase = campFire.transform;
        campFirePurchaseText.text = "Purchased";
        campFirePurchaseBtn.interactable = false;
        campfireBuyButton.isAlreadyPurchased = true;
        Game.hasPurchasedCampFire = true;
    }

    public void BuyUnit()
    {
        if(GameManager.instance.goldCount < 2)
        {
            Debug.Log("Not enough Resources");
            return;
        }

        GameManager.instance.RemoveGold(2);

        Instantiate(pawn, transform.position, Quaternion.identity);
    }

    public void BuyGlobalDamageAdd()
    {
        if(GameManager.instance.goldCount < 1 || GameManager.instance.woodCount < 10 || GameManager.instance.rockCount < 10)
        {
            Debug.Log("Not enough Resources");
            return;
        }
        GameManager.instance.RemoveGold(1);
        GameManager.instance.RemoveRock(10);
        GameManager.instance.RemoveWood(10);
        GameManager.instance.GlobalDamageAdd += 5.0f;

    }

    void MineResourcesTutorial()
    {
        GameManager.instance.SetTutorialPanel(true);
        GameManager.instance.WriteTutorialText("You can cut or mine resources by clicking upon wood/Stone. Note: You can only queue task per number of your colony pawn. Only Normal state pawn will do Task");

    }
}
