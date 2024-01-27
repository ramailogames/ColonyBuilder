using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldLoot : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.goldCount += 1;
        GameManager.instance.goldCountTxt.text = GameManager.instance.goldCount.ToString();
        StartCoroutine(MoveToCampBase());
    }

    IEnumerator MoveToCampBase()
    {
        yield return new WaitForEndOfFrame();

        transform.DOMove(GameManager.instance.resourcePoint.position, 2f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
