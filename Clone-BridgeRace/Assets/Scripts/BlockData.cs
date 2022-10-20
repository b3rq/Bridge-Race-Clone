using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockData : MonoBehaviour
{
    public List<GameObject> AllBlocks;
    public static BlockData Instance;
    public event Action OnBlockAdded;

    public GameObject AI_1_laststair;
    public GameObject AI_2_laststair;

    private void Awake()
    {
        Instance = this;
    }

    public void AddBlock(GameObject block)
    {
        AllBlocks.Add(block);

        OnBlockAdded?.Invoke();
    }
}