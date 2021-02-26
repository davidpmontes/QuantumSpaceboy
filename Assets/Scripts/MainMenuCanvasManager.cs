﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasManager : MonoBehaviour
{
    public static MainMenuCanvasManager Instance { get; private set; }
    [SerializeField] private Image fadeToBlackPanel;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject startScreen;

    public void Init()
    {
        Instance = this;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        startScreen.SetActive(false);
    }

    public void ShowStartScreen()
    {
        titleScreen.SetActive(false);
        startScreen.SetActive(true);
    }

    public void SetToBlack()
    {
        fadeToBlackPanel.color = new Color(0, 0, 0, 1);
    }

    public void SetToTransparent()
    {
        fadeToBlackPanel.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator FadeToBlackCoroutine(float durationSeconds)
    {
        float startTime = Time.time;
        float endTime = Time.time + durationSeconds;
        
        while(Time.time < endTime)
        {
            var currAlpha = Mathf.Lerp(0, 1, (Time.time - startTime) / durationSeconds);
            fadeToBlackPanel.color = new Color(0, 0, 0, currAlpha);
            yield return null;
        }

        fadeToBlackPanel.color = new Color(0, 0, 0, 1);
    }

    public IEnumerator FadeToTransparentCoroutine(float durationSeconds)
    {
        float startTime = Time.time;
        float endTime = Time.time + durationSeconds;

        while (Time.time < endTime)
        {
            var currAlpha = 1 - Mathf.Lerp(0, 1, (Time.time - startTime) / durationSeconds);
            fadeToBlackPanel.color = new Color(0, 0, 0, currAlpha);
            yield return null;
        }

        fadeToBlackPanel.color = new Color(0, 0, 0, 0);
    }
}
