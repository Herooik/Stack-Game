using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public bool StopMoving { get; set; }
    [SerializeField] private float borderZ = 2f;
    [SerializeField] private float borderX = 2f;
    [SerializeField] private float moveSpeed = 2f;

    private bool _changeDirection;

    void Update()
    {
        if (!StopMoving)
        {
            if (BlockController.MoveDirection == MoveDirection.Z)
            {
                Moving(Vector3.forward, transform.position.z, borderZ);
            }
            else
            {
                Moving(Vector3.right, transform.position.x, borderX);
            }
        }
    }

    private void Moving(Vector3 direction, float blockAxis, float borderAxis)
    {
        if (_changeDirection)
        {
            transform.Translate(-direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
            
        if (blockAxis >= borderZ)
        {
            _changeDirection = true;
        }
        else if (blockAxis <= -borderZ)
        {
            _changeDirection = false;
        }
    }
}
