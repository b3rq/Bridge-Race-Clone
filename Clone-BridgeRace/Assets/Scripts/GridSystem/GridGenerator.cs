using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Transform _TopLeftCorner;
    [SerializeField] private Transform _BottomRightCorner;

    [SerializeField] private float _zDistance;
    [SerializeField] private float _xDistance;


    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        CreatePieces();
    }

    private void CreatePieces()
    {
        int xAmount = (int)((_BottomRightCorner.position.x - _TopLeftCorner.position.x) / _xDistance);
        int zAmount = (int)((_TopLeftCorner.position.z - _BottomRightCorner.position.z) / _zDistance);

        xAmount++;
        zAmount++;

        //Debug.Log(xAmount);
        //Debug.Log(zAmount);

        for (int i = 0; i < xAmount; i++)
        {
            for (int j = 0; j < zAmount; j++)
            {
                //Debug.Log(i+ " " + " " +j);

                GameObject grid = new();
                grid.AddComponent<GridPiece>();
                grid.GetComponent<GridPiece>().SetPrefabs(_prefab);
                grid.GetComponent<GridPiece>().Init();
                grid.transform.position = new Vector3(i * _xDistance, 0.1f, j * -_zDistance) + _TopLeftCorner.position;
            }
        }
    }
}