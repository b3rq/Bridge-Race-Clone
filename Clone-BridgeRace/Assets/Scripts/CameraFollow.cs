using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _cameraTarget;
    [SerializeField] private float _posSpeed;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private Quaternion _rotOffset;

    private void LateUpdate()
    {

        if (FinishSystem.Instance.IsFinish)
        {
            Vector3 finishPos = new Vector3(2.1f, 5.3f, 26.62f);
            Quaternion finishRotation = Quaternion.Euler(10.8f, -152.31f, 0);
            transform.position = Vector3.Lerp(transform.position, finishPos, _posSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, finishRotation, _posSpeed * Time.deltaTime);

        }

        if (UI.Instance.IsStart & !FinishSystem.Instance.IsFinish)
        {
            if (_cameraTarget.transform.position.y > -1)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _rotOffset, _posSpeed * Time.deltaTime);

                transform.position = Vector3.Lerp(transform.position, _cameraTarget.transform.position + _offset, _posSpeed * Time.deltaTime);
            }
            else
            {
                UI.Instance.DieScreenActive();
                //UI.Instance.IsDie = true;
                //UI.Instance.DieScreen.SetActive(true);
            }
        }
    }
}