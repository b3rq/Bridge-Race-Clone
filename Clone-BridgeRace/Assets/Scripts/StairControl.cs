using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairControl : MonoBehaviour
{
    [SerializeField] private LayerMask mask;

    private CharacterRotate _characterRotate;
    private RaycastHit hit;

    private Collector _collector;

    private void Awake()
    {
        _collector = GetComponentInParent<Collector>();
    }

    private void Start()
    {
        _characterRotate = CharacterRotate.Instance;
    }


    void Update()
    {
        if (!hit.collider) CharacterRotate.Instance.IsCanMoveZ = true;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity, mask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);

            MeshRenderer mr = hit.collider.GetComponent<MeshRenderer>();


            if (_collector.CollectedBlocks.Count == 0) _characterRotate.IsCanMoveZ = mr.enabled;
        }
    }
}
