using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public List<GameObject> enemyWave1;
    public List<GameObject> enemyWave2;
    public List<GameObject> enemyWave3;
    public List<GameObject> enemyWave4;
    public List<GameObject> enemyWave5;


    public float wavaDelay1;
    public float wavaDelay2;
    public float wavaDelay3;
    public float wavaDelay4;
    public float wavaDelay5;

    public Transform[] randomEnemySpawnPoints;

    public GameObject winView;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.WriteTutorialText("Click Trade Button and Place a Barrack!");

      
    }

    public void SpawnWave()
    {
        StartCoroutine(Enum_SpawnWave());
    }

    IEnumerator Enum_SpawnWave()
    {
        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(wavaDelay1);
        // spawn wave 1
        for (int i = 0; i < enemyWave1.Count; i++)
        {
            Instantiate(enemyWave1[i], spawnPoint().position, Quaternion.identity);
        }
        GameManager.instance.SetMessagePanel(true);
        GameManager.instance.messageText.text = "Bandits are raiding!";

        yield return new WaitForSeconds(wavaDelay2);
        // spawn wave 2
        for (int i = 0; i < enemyWave2.Count; i++)
        {
            Instantiate(enemyWave2[i], spawnPoint().position, Quaternion.identity);
        }
        GameManager.instance.SetMessagePanel(true);
        GameManager.instance.messageText.text = "Bandits are raiding!";

        yield return new WaitForSeconds(wavaDelay3);
        // spawn wave 3
        for (int i = 0; i < enemyWave3.Count; i++)
        {
            Instantiate(enemyWave3[i], spawnPoint().position, Quaternion.identity);
        }
        GameManager.instance.SetMessagePanel(true);
        GameManager.instance.messageText.text = "Bandits are raiding!";

        yield return new WaitForSeconds(wavaDelay4);
        // spawn wave 4
        for (int i = 0; i < enemyWave4.Count; i++)
        {
            Instantiate(enemyWave4[i], spawnPoint().position, Quaternion.identity);
        }
        GameManager.instance.SetMessagePanel(true);
        GameManager.instance.messageText.text = "Bandits are raiding!";

        yield return new WaitForSeconds(wavaDelay5);
        // spawn wave 4
        for (int i = 0; i < enemyWave5.Count; i++)
        {
            Instantiate(enemyWave5[i], spawnPoint().position, Quaternion.identity);
        }
        GameManager.instance.SetMessagePanel(true);
        GameManager.instance.messageText.text = "Teodor The Savant and his bandits are raiding!";

        yield return new WaitForSeconds(3f);

        InvokeRepeating("CheckWin", 0f, 1f);
    }

    public Transform spawnPoint()
    {
        int index = Random.Range(0, randomEnemySpawnPoints.Length);
        return randomEnemySpawnPoints[index];
    }


    public void CheckWin()
    {
        PawnBase[] countPawn = FindObjectsOfType<PawnBase>(); //Finds
        List<PawnBase> countVillager = new List<PawnBase>(); //listed

        for (int i = 0; i < countPawn.Length; i++)
        {
            if (countPawn[i].GetComponent<PawnBase>().myPawnType == PawnBase.MyPawnType.Hostile)
            {
                countVillager.Add(countPawn[i]); //add hostile to list
            }
        }

        Debug.Log(countVillager.Count);

        if (countVillager.Count <= 0) // if hostile is zero
        {
            Debug.Log("You won.");
            winView.SetActive(true);
        }
    }
}
