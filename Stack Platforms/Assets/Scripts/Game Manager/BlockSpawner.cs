using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private BlockController blockPrefab;
    [SerializeField] private MoveDirection moveDirection;

    public void SpawningBlock()
    {
        var block = BlockPool.Instance.GetPooledObject();
        block.SetActive(true);

        if (BlockController.LastBlock != null && !BlockController.IsStartBlock)
        {
            
            var zPos = moveDirection == MoveDirection.Z
                ? transform.position.z
                : BlockController.LastBlock.transform.position.z;
            
            var xPos = moveDirection == MoveDirection.X
                ? transform.position.x
                : BlockController.LastBlock.transform.position.x; 
            
            block.transform.position = new Vector3(xPos,
                BlockController.LastBlock.transform.position.y + blockPrefab.transform.localScale.y,
                zPos);
        }
        else
        {
            block.transform.position = transform.position;
            BlockController.IsStartBlock = false;
        }
        
        BlockController.MoveDirection = moveDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, blockPrefab.transform.localScale);
    }
}