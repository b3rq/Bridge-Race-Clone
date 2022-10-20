using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private List<Color> _colors;

    private int rndFactory;

    #region BoxCast
    RaycastHit hit;
    RaycastHit hit2;

    public LayerMask mask;
    bool isHit;
    bool isHit2;
    #endregion

    private void Start()
    {
        rndFactory = Random.Range(5, 10);

        _colors.Add(new Color(0.1214845f, 0.3149333f, 0.6603774f));
        _colors.Add(new Color(0.8301887f, 0.1997152f, 0.1997152f));
        _colors.Add(new Color(0.1974457f, 0.8207547f, 0.1974457f));

        StartCoroutine(CreateRandomBlock());
    }

    IEnumerator CreateRandomBlock()
    {
        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < rndFactory; i++)
        {
            Vector3 RandomSpawnPos1 = new Vector3(Random.Range(-4.5f, 4.5f), 2.29f, Random.Range(9, 19));

            Vector3 RandomSpawnPos2 = new Vector3(Random.Range(-4.5f, 4.5f), 2.29f, Random.Range(9, 19));

            Vector3 origin1 = RandomSpawnPos1;
            origin1.y += 5;

            Vector3 origin2 = RandomSpawnPos2;
            origin2.y += 5;

            isHit = Physics.BoxCast(origin1, _blockPrefab.transform.lossyScale / 2, Vector3.down, out hit, transform.rotation, Mathf.Infinity, mask);


            if (!isHit)
            {
                GameObject block = _blockPrefab.gameObject;
                block.GetComponent<Renderer>().sharedMaterial.color = _colors[Random.Range(0, 3)];

                //Instantiate(block, RandomSpawnPos1, Quaternion.identity);

            }
            else
            {
                Debug.Log("koyulmadý!");
            }

            isHit2 = Physics.BoxCast(origin2, _blockPrefab.transform.lossyScale / 2, Vector3.down, out hit2, transform.rotation, Mathf.Infinity, mask);

            //if (!isHit2)
            //{
            //    Instantiate(_enemyBlockPrefab, RandomSpawnPos2, Quaternion.identity);
            //}
            //else
            //{
            //    Debug.Log("koyulmadý!");
            //}
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(CreateRandomBlock());
    }


}
