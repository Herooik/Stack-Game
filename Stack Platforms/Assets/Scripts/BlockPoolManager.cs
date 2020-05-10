using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPoolManager : MonoBehaviour
{
    public static BlockPoolManager Instance { get; private set; }
    
    [SerializeField] private GameObject blockToSpawn;
    [SerializeField] private GameObject blockContainer;
    [SerializeField] private int amountToPool;
    [SerializeField] private bool shouldExpand = true;
    
    private List<GameObject> _pooledBlocks;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _pooledBlocks = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            var obj = Instantiate(blockToSpawn, blockContainer.transform);
            obj.gameObject.SetActive(false);
            _pooledBlocks.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledBlocks.Count; i++)
        {
            if (!_pooledBlocks[i].activeInHierarchy)
            {
                return _pooledBlocks[i];
            }
        }

        if (shouldExpand)
        {
            var obj = Instantiate(blockToSpawn, blockContainer.transform);
            obj.gameObject.SetActive(false);
            _pooledBlocks.Add(obj);
            return obj;
        }

        return null;
    }
}
