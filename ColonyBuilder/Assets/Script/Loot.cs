using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Loot : MonoBehaviour
{
    private void Start()
    {
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
