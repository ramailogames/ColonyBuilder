using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public CanvasGroup fadeCanvas;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        fadeCanvas.DOFade(0, 3f);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
