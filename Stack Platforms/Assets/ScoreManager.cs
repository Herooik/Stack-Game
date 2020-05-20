using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Canvas scoreCanvas;
    [SerializeField] private MoveBlock moveBlock;

    private int _score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        moveBlock.moveSpeed = 2.3f;
    }
    
    public void AddScore()
    {
        _score++;
        scoreText.text = _score.ToString();
        scoreCanvas.gameObject.SetActive(true);
        
        if (_score % 10 == 0 && _score != 0)
        {
            moveBlock.moveSpeed += 0.1f;
        }
    }
}
