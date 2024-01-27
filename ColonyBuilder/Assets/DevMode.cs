using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.goldCount += 1;
        GameManager.instance.goldCountTxt.text = GameManager.instance.goldCount.ToString();

        GameManager.instance.woodCount += 10;
        GameManager.instance.woodCountTxt.text = GameManager.instance.woodCount.ToString(); 

        GameManager.instance.rockCount += 5;
        GameManager.instance.rockCountTxt.text = GameManager.instance.rockCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
