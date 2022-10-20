using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishSystem : MonoBehaviour
{
    public bool IsFinish = false;
    private GameObject _firstPlayer;
    [SerializeField] List<GameObject> Characters;

    public static FinishSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (IsFinish)
        {
            //Invoke(nameof(LosingPlayerSetPosition), 1);
            //Invoke(nameof(SetWinnerPlayerPosition), 1);
            LosingPlayerSetPosition();
            SetWinnerPlayerPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ai"))
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            _firstPlayer = other.gameObject;
            Characters.Remove(_firstPlayer);
            IsFinish = true;

            AI asd = _firstPlayer.GetComponent<AI>();
            asd._animator.SetBool("isRun", false);
            UI.Instance.DieScreenActive();
            return;
        }
        if (other.CompareTag("player"))
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            _firstPlayer = other.gameObject;
            Characters.Remove(_firstPlayer);
            IsFinish = true;
            CharacterRotate.Instance._animator.SetBool("isRun", false);
            UI.Instance.FinishScreen.SetActive(true);

            UI.Instance.StartCoroutine("CO_PlayParticle");
        }
    }

    private void SetWinnerPlayerPosition()
    {
        if (_firstPlayer.CompareTag("player"))
        {
            _firstPlayer.transform.parent.SetPositionAndRotation(new Vector3(-0.245f, 3.879f, 22.392f), Quaternion.identity);
            return;
        }
        _firstPlayer.transform.SetPositionAndRotation(new Vector3(-0.245f, 3.879f, 22.392f), Quaternion.identity);

    }

    private void LosingPlayerSetPosition()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            if (i == 1)
            {
                Characters[i].transform.parent.SetPositionAndRotation(new Vector3(1.5f, 3, 18), Quaternion.identity);
                Characters[i].transform.SetPositionAndRotation(new Vector3(1.5f, 3, 18), Quaternion.identity);
            }
            else
            {
                Characters[i].transform.parent.SetPositionAndRotation(new Vector3(1.5f, 3, 18), Quaternion.identity);
                Characters[i].transform.SetPositionAndRotation(new Vector3(-1.5f, 2, 18), Quaternion.identity);
            }
        }
    }
}