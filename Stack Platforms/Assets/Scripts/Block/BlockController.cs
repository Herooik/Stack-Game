using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BlockController : MonoBehaviour
{
    public static BlockController CurrentBlock { get; private set; }
    public static Transform LastBlock { get; private set; }
    public static MoveDirection MoveDirection { get; set; }
    public static bool IsStartBlock { get; set; }
    public static bool IsBlockOutside { get; set; }

    [SerializeField] private PlaySoundSystem playSoundSystem;
    [SerializeField] private DropBlockController dropBlockController;
    [SerializeField] private MoveBlock moveBlock;
    [SerializeField] private Transform startBlock;
    [SerializeField] private float marginError = 0.5f;

    private void OnEnable()
    {
        if (LastBlock == null)
        { 
            LastBlock = startBlock;
            IsStartBlock = true;
        }

        transform.localScale = new Vector3(LastBlock.localScale.x, transform.localScale.y, LastBlock.localScale.z);
    }

    private void Start()
    {
        CurrentBlock = this;
    }

    public void StopMoving()
    {
        moveBlock.StopMoving = true;
        
        var distance = GetDistanceBetweenBlocks();
        
        if (Mathf.Abs(distance) <= marginError)
        {
            ConnectBlocks();
            return;
        }

        if (CheckDistanceDifference(distance)) return;
        
        playSoundSystem.PlayPutSound();
        ScoreManager.Instance.AddScore();
        
        var direction = distance > 0 ? 1f : -1f;

        if (MoveDirection == MoveDirection.Z)
        {
            SplitBlockOnZ(distance, direction);
        }
        else
        {
            SplitBlockOnX(distance, direction);
        }
        
        LastBlock = transform;
    }

    private float GetDistanceBetweenBlocks()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastBlock.position.z;
        }
        
        return transform.position.x - LastBlock.position.x;
    }
    
    private void ConnectBlocks()
    {
        playSoundSystem.PlayPerfectMatchSound();
        
        ScoreManager.Instance.AddScore();
        
        transform.position = new Vector3(LastBlock.position.x, transform.position.y, LastBlock.position.z);
        transform.localScale = new Vector3(LastBlock.localScale.x, transform.localScale.y, LastBlock.localScale.z);

        LastBlock = transform;
    }
    
    private static bool CheckDistanceDifference(float distance)
    {
        var maxDistanceDifference = MoveDirection == MoveDirection.Z
            ? LastBlock.localScale.z
            : LastBlock.localScale.x;

        if (!(Mathf.Abs(distance) > maxDistanceDifference)) return false;
        
        CurrentBlock.gameObject.AddComponent<Rigidbody>();
        IsBlockOutside = true;
        LastBlock = null;
        CurrentBlock = null;

        UIManager.Instance.ShowEndGameCanvas();
        return true;
    }

    private void SplitBlockOnX(float hangover, float direction)
    {
        var newXPosition = LastBlock.position.x + (hangover / 2);
        var newXSize = LastBlock.localScale.x - Mathf.Abs(hangover);
        
        var fallingBlockSize = transform.localScale.x - newXSize;

        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        var blockEdge = transform.position.x + (newXSize / 2f * direction);
        
        var fallingBlockXPosition = blockEdge + (fallingBlockSize / 2f * direction);
        
        dropBlockController.SpawnDropBlock(fallingBlockXPosition, fallingBlockSize);
    }
    
    private void SplitBlockOnZ(float hangover, float direction)
    {
        var newZPosition = LastBlock.position.z + (hangover / 2);
        var newZSize = LastBlock.localScale.z - Mathf.Abs(hangover);

        var fallingBlockZSize = transform.localScale.z - newZSize;
        
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        var blockEdge = transform.position.z + (newZSize / 2f * direction);
        
        var fallingBlockZPosition = blockEdge + (fallingBlockZSize / 2f * direction);
        
        dropBlockController.SpawnDropBlock(fallingBlockZPosition, fallingBlockZSize);
    }
}
