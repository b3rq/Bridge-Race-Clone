using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece : MonoBehaviour
{
    GameObject _block;
    private GameObject _blockPrefab;

    public void CreateBlock()
    {
        _block = Instantiate(_blockPrefab, transform);
        CreateRandomColor(_block);
        _block.transform.localPosition = Vector3.zero;

        BlockData.Instance.AddBlock(_block);
    }

    public void CreateRandomColor(GameObject GetBlock)
    {
        GetBlock.GetComponent<Renderer>().material.color = RandomColorFactory.Instance.colors[Random.Range(0, RandomColorFactory.Instance.colors.Length)];
        //BlockController.Instance.ColorIsItTopLevel(GetBlock);
    }

    public void TakeMyBlock()
    {
        BlockData.Instance.AllBlocks.Remove(_block);
        StartCoroutine(CO_WaitForNewBlock());
    }

    public void Init()
    {
        CreateBlock();
    }

    public void SetPrefabs(GameObject pref)
    {
        _blockPrefab = pref;
    }

    private IEnumerator CO_WaitForNewBlock()
    {
        yield return new WaitForSeconds(2);

        CreateBlock();
    }
}