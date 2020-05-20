using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public bool IsGameEnd { get; set; }

    [SerializeField] private Canvas startGameCanvas;
    [SerializeField] private Canvas endGameCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }  
    }

    public void HideStartCanvas()
    {
        startGameCanvas.gameObject.SetActive(false);
    }

    public void ShowEndGameCanvas()
    {
        IsGameEnd = true;
        endGameCanvas.gameObject.SetActive(true);
    }
}
