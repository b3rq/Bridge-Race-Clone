using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] public GameObject DoorTriggerCheck;
    private NavMeshAgent _navmeshagent;
    public Animator _animator;

   

    private int _randomBlockNumber;
    private int _randomCollectNumber;
    public bool KeepGoing = false;

    #region Instance
    private Collector _collector;
    #endregion

    private void Awake()
    {
        _collector = GetComponent<Collector>();
    }

    private void Start()
    {
        _navmeshagent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == DoorTriggerCheck)
        {
            DoorTriggerCheck.SetActive(false);
            TopLayerOn();
        }

        if (other.gameObject.CompareTag("block"))
        {
            if (other.GetComponent<MeshRenderer>().material.color == _collector.BotMat.color)
            {
                _collector.collactableBlocks.Remove(other.gameObject);

                if (_collector.CollectedBlocks.Count == 10 & KeepGoing)
                {
                    GoToStairs();
                }
                else
                {
                    GoToOtherBlocks();
                }
            }
        }
    }

    public void GoToFirstBlock()
    {
        _randomCollectNumber = Random.Range(1, _collector.collactableBlocks.Count);

        _randomBlockNumber = Random.Range(0, _collector.collactableBlocks.Count);
        _navmeshagent.SetDestination(_collector.collactableBlocks[_randomBlockNumber].transform.position);
        _animator.SetBool("isRun", true);

    }

    private void GoToOtherBlocks()
    {
        if (FinishSystem.Instance.IsFinish) return;
        {
            _randomBlockNumber = Random.Range(0, _collector.collactableBlocks.Count - 1);
            if (_collector.CollectedBlocks.Count < _randomCollectNumber)
            {
                if (KeepGoing & _collector.collactableBlocks[_randomBlockNumber].transform.position.y! < 1)
                {
                    _collector.collactableBlocks.RemoveAt(_randomBlockNumber);

                    GoToOtherBlocks();
                }
                _navmeshagent.SetDestination(_collector.collactableBlocks[_randomBlockNumber].transform.position);
                return;
            }
            GoToStairs(); _randomBlockNumber = Random.Range(0, _collector.collactableBlocks.Count - 1);
            if (_collector.CollectedBlocks.Count < _randomCollectNumber)
            {
                if (KeepGoing & _collector.collactableBlocks[_randomBlockNumber].transform.position.y! < 1)
                {
                    GoToOtherBlocks();
                }
                _navmeshagent.SetDestination(_collector.collactableBlocks[_randomBlockNumber].transform.position);
                return;
            }
            GoToStairs();
        }
    }

    private void GoToStairs()
    {
        if (!FinishSystem.Instance.IsFinish)
        {
            _navmeshagent.SetDestination(_target.transform.position);
        }
    }

    public void AgainCollect()
    {
        if (_collector.CollectedBlocks.Count == 0)
        {

            _randomCollectNumber = 15;

            GoToOtherBlocks();
        }
    }

    private void TopLayerOn()
    {
        KeepGoing = true;

        StartCoroutine(CO_TopLayerFirstBlock());

        for (int i = 0; i < BlockData.Instance.AllBlocks.Count; i++)
        {
            if (BlockData.Instance.AllBlocks[i].GetComponent<MeshRenderer>().material.color == GetComponent<MeshRenderer>().material.color)
            {
                if (BlockData.Instance.AllBlocks[i].gameObject.transform.position.y < 1)
                {
                    BlockData.Instance.AllBlocks[i].SetActive(false);
                }
            }
        }
    }

    IEnumerator CO_TopLayerFirstBlock()
    {
        yield return new WaitForSeconds(0.1f);

        GoToOtherBlocks();
    }
}