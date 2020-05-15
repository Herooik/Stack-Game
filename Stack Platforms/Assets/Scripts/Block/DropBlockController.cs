using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBlockController : MonoBehaviour
{
    [SerializeField] private Renderer dropBlockRenderer;
    
    private void Start()
    {
        dropBlockRenderer.material.color = BlockColorChange.BlockColor;
    }

    public void SpawnDropBlock(float fallingBlockZPosition, float fallingBlockSize)
    {
        var currentBlock = BlockController.CurrentBlock.transform;
        
        var dropBlock = Instantiate(gameObject);
        
        Destroy(dropBlock, 1f);

        if (BlockController.MoveDirection == MoveDirection.Z)
        {
            dropBlock.transform.localScale = new Vector3(currentBlock.localScale.x, currentBlock.localScale.y, fallingBlockSize);
            dropBlock.transform.position = new Vector3(currentBlock.position.x, currentBlock.position.y, fallingBlockZPosition);
        }
        else
        {
            dropBlock.transform.localScale = new Vector3(fallingBlockSize, currentBlock.localScale.y, currentBlock.localScale.z);
            dropBlock.transform.position = new Vector3(fallingBlockZPosition, currentBlock.position.y, currentBlock.position.z);
        }
    }
}
