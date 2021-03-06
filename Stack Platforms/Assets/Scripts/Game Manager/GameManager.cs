﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockSpawner[] blockSpawners;
    [SerializeField] private float containerMoveSpeed = 2f;
    [SerializeField] private Transform cameraTransform;

    private BlockSpawner _currentSpawner;
    private int _spawnerIndex;

    private bool _shouldCameraMove;
    private Vector3 _cameraTargetPos;

    private bool _isGameStarted;

    private void OnEnable()
    {
        BlockController.IsBlockOutside = false;
    }

    private void Start()
    {
        StartCoroutine(WaitForStartUI());
        enabled = false;
    }

    private IEnumerator WaitForStartUI()
    {
        yield return new WaitForSeconds(0.2f);
        enabled = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !UIManager.Instance.IsGameEnd)
        {
            if (!_isGameStarted)
            {
                _isGameStarted = true;
                UIManager.Instance.HideStartCanvas();
            }

            if (BlockController.CurrentBlock != null)
            {
                BlockController.CurrentBlock.StopMoving();

                _shouldCameraMove = true;
                _cameraTargetPos = new Vector3(cameraTransform.position.x, cameraTransform.position.y + 0.2f, cameraTransform.position.z);
            }

            if (!BlockController.IsBlockOutside)
            {
                SpawnBlock();
            }
        }
        else if (Input.GetButtonDown("Fire1") && UIManager.Instance.IsGameEnd)
        {
            SceneManager.LoadScene(0);
        }
        
        if (_shouldCameraMove)
        {
            MoveCameraUp();
        }
        
    }

    private void SpawnBlock()
    {
        _spawnerIndex = _spawnerIndex == 0 ? 1 : 0;
        _currentSpawner = blockSpawners[_spawnerIndex];
        _currentSpawner.SpawningBlock();
    }
    
    private void MoveCameraUp()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, _cameraTargetPos, containerMoveSpeed * Time.deltaTime);

        if (cameraTransform.position.y >= _cameraTargetPos.y)
        {
            _shouldCameraMove = false;
        }
    }
}
