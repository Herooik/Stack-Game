using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockSpawner[] blockSpawners;
    [SerializeField] private Transform blocksContainer;
    [SerializeField] private float containerMoveSpeed = 2f;

    private BlockSpawner _currentSpawner;
    private int _spawnerIndex;

    private bool _shouldContainerMove;
    private Vector3 _containerTargetPos;

    private bool _isGameStarted;
    
    private void OnEnable()
    {
        BlockController.IsBlockOutside = false;
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

                _shouldContainerMove = true;
                _containerTargetPos = new Vector3(blocksContainer.position.x, blocksContainer.position.y - 0.2f);
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
        
        if (_shouldContainerMove)
        {
            MovingBlocksDown();
        }
    }

    private void SpawnBlock()
    {
        _spawnerIndex = _spawnerIndex == 0 ? 1 : 0;
        _currentSpawner = blockSpawners[_spawnerIndex];
        _currentSpawner.SpawningBlock();
    }
    
    private void MovingBlocksDown()
    {
        blocksContainer.position =
            Vector3.Lerp(blocksContainer.position, _containerTargetPos, containerMoveSpeed * Time.deltaTime);

        if (blocksContainer.position.y <= _containerTargetPos.y)
        {
            _shouldContainerMove = false;
        }
    }
}
