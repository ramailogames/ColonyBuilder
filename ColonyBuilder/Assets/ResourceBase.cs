using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResourceBase : MonoBehaviour
{
    public GameObject axeUi;
    bool isAlreadyMined = false;

    [Header("Check Pawn")]
    public Transform checkPawnPos;
    public ResourceData myResourceData;
    private PawnBase workingPawn;

    public enum ResourceType
    {
        Tree,
        Rock
    }

    public ResourceType myResourceType;

    private void Start()
    {
        InvokeRepeating("CheckPawn", 1f, 1f);
    }

    public void MineResource()
    {
        StartCoroutine(EnumMineResource());
    }

    IEnumerator EnumMineResource()
    {

        yield return new WaitForEndOfFrame();

        if (isAlreadyMined)
        {
            yield break;
        }

        isAlreadyMined = true;

        transform.DOScale(new Vector2(0.8f, 0.8f), .5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.25f);
        transform.DOScale(new Vector2(1, 1), .5f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(0.25f);
        transform.DOScale(new Vector2(0.8f, 0.8f), .5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.25f);
        transform.DOScale(new Vector2(1, 1), .5f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(0.25f);
        transform.DOScale(new Vector2(0.8f, 0.8f), .5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.25f);
        transform.DOScale(new Vector2(1, 1), .5f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(0.25f);
        transform.DOScale(new Vector2(0.8f, 0.8f), .5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.25f);
        transform.DOScale(new Vector2(1, 1), .5f).SetEase(Ease.OutBack);

        Loot();
        workingPawn.MakePawnIdle();
        Destroy(gameObject);

    }

    void Loot()
    {
        switch (myResourceType)
        {
            case ResourceType.Tree:
                GameManager.instance.woodCount += 5;
                break;

            case ResourceType.Rock:
                GameManager.instance.rockCount += 2;
                break;
        }
    }

    private void OnMouseDown()
    {
        if (isAlreadyMined)
        {
            return;
        }

        if (PawnManager.instance.idlePawnList.Count <= 0)
        {
            return;
        }

        axeUi.SetActive(true);
        axeUi.transform.DOMove(new Vector2(axeUi.transform.position.x, axeUi.transform.position.y + 0.4f), 1f).SetEase(Ease.OutBack);
        axeUi.transform.DOScale(new Vector2(1f, 1f), 1f).SetEase(Ease.OutBack);

        PawnManager.instance.SelectPawn();
        PawnManager.instance.selectedPawn.SetTarget(transform);
        workingPawn = PawnManager.instance.selectedPawn;
    }

    void CheckPawn()
    {
        Collider2D pawn = Physics2D.OverlapCircle(checkPawnPos.position, myResourceData.checkRadius, myResourceData.whatIsPlayer);

        if (pawn != null)
        {
            if (!pawn.GetComponent<PawnBase>().isIdle && pawn.GetComponent<PawnBase>().isTaskRunning)
            {
                MineResource();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPawnPos.position, myResourceData.checkRadius);
    }
}
