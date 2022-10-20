using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [SerializeField] private GameObject _topGridGenerator;

    private void OnTriggerEnter(Collider other)
    {
        _topGridGenerator.SetActive(true);
    }
}
