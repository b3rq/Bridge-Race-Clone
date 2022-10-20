using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{

    public List<GameObject> collactableBlocks;
    private AI ai;

    #region Materials
    public Material Mycolor;
    public Material BotMat;
    #endregion

    #region Collect System
    public List<GameObject> CollectedBlocks;
    [SerializeField] private GameObject _blockTransporter;
    [SerializeField] private GameObject _stairs;
    [SerializeField] private float _lerpSpeed;
    private Quaternion _setRot;
    #endregion

    private void Start()
    {
        ai = GetComponent<AI>();
        RandomColorFactory.Instance.ChangeColor(Mycolor);
        GetComponent<MeshRenderer>().material.color = Mycolor.color;

        BlockData.Instance.OnBlockAdded += SetCollactabes;
    }

    private void OnDisable()
    {
        BlockData.Instance.OnBlockAdded -= SetCollactabes;
    }

    void Update()
    {
        CollectBlocks();
    }

    public void SetCollactabes()
    {
        collactableBlocks.Clear();

        foreach (var block in BlockData.Instance.AllBlocks)
        {
            if (block.GetComponent<MeshRenderer>().material.color == BotMat.color)
            {
                collactableBlocks.Add(block);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Renderer>().material.color == Mycolor.color)
        {
            other.transform.parent.GetComponent<GridPiece>().TakeMyBlock();
            CollectedBlocks.Add(other.gameObject);
            other.GetComponent<BoxCollider>().enabled = false;
            _setRot = Quaternion.Euler(0, 90, 0);
            other.transform.localRotation = _setRot;
            other.transform.SetParent(transform);
        }

        if (other.gameObject.CompareTag("stairblock"))
        {
            GameObject stairBlock = other.gameObject;
            ChangeBlockColor(stairBlock);

            if (CollectedBlocks.Count > 0)
            {
                _stairs.SetActive(true);
                var deletedBlock = CollectedBlocks[^1];
                CollectedBlocks.Remove(deletedBlock);
                deletedBlock.transform.SetParent(null);
                Destroy(deletedBlock);

                if (ai & !FinishSystem.Instance.IsFinish) ai.AgainCollect();
            }
        }
    }

    void ChangeBlockColor(GameObject GetBlock)
    {
        GetBlock.GetComponent<BoxCollider>().enabled = false;
        if (CollectedBlocks.Count > 0)
        {
            Color GetColor = CollectedBlocks[0].GetComponent<Renderer>().material.color;
            GetBlock.GetComponent<Renderer>().material.color = GetColor;
            GetBlock.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void CollectBlocks()
    {
        foreach (var block in CollectedBlocks)
        {
            if (CollectedBlocks.IndexOf(block) == 0)
            {
                block.transform.position = Vector3.Lerp(block.transform.position, _blockTransporter.transform.position, _lerpSpeed * Time.deltaTime);
                block.transform.localRotation = _setRot;
            }
            else
            {
                if (block.transform.position.x != CollectedBlocks[CollectedBlocks.IndexOf(block) - 1].transform.position.x)
                {
                    Vector3 otherBlocks = CollectedBlocks[CollectedBlocks.IndexOf(block) - 1].transform.position + Vector3.up * 0.19f;
                    block.transform.localRotation = _setRot;

                    block.transform.position = Vector3.Lerp(block.transform.position, otherBlocks, _lerpSpeed * Time.deltaTime);
                }
            }
        }
    }
}