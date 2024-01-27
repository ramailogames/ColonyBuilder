using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public float delay;
    private void OnEnable()
    {
        Invoke("DisableSelf", delay);
    }

    void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
