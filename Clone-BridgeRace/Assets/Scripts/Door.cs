using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public enum DoorsOpenType { Pos, Rot }
    public DoorsOpenType doorsOpenType;


    [SerializeField] private Vector3 _DoorTargetPos;
    [SerializeField] private Quaternion _DoorTargetRot;

    [SerializeField] private Quaternion _DoorClosedRot;
    [SerializeField] private Vector3 _DoorOpenPos;
    [SerializeField] private Quaternion _DoorOpenRot;
    private Vector3 _DoorClosedPos;

    [SerializeField] private bool _isDoorOpen = false;
    [SerializeField] private float _lerpspeed;

    void Start()
    {
        _DoorClosedPos = transform.position;
    }

    public void OpenTheDoor()
    {
        _DoorTargetPos = _DoorOpenPos;
        _DoorTargetRot = _DoorOpenRot;

        StartCoroutine(CO_ChangeDoorStateCorotuine());

        _isDoorOpen = true;
    }

    public void CloseTheDoor()
    {

        _DoorTargetPos = _DoorClosedPos;
        _DoorTargetRot = _DoorClosedRot;

        StartCoroutine(CO_ChangeDoorStateCorotuine());

        _isDoorOpen = false;
    }

    public void ChangeDoorState()
    {
        if (_isDoorOpen)
        {
            _DoorTargetPos = _DoorClosedPos;
            _DoorTargetRot = _DoorClosedRot;
        }
        else
        {
            _DoorTargetPos = _DoorOpenPos;
            _DoorTargetRot = _DoorOpenRot;
        }
        StartCoroutine(CO_ChangeDoorStateCorotuine());

        _isDoorOpen = !_isDoorOpen;
    }

    IEnumerator CO_ChangeDoorStateCorotuine()
    {
        if (doorsOpenType == DoorsOpenType.Pos)
        {
            transform.position = Vector3.Lerp(transform.position, _DoorTargetPos, _lerpspeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _DoorTargetPos) > 0.01f)
            {
                yield return new WaitForEndOfFrame();
                StartCoroutine(CO_ChangeDoorStateCorotuine());
            }
            else
            {
                yield break;
            }
        }
        else
        {
            Quaternion SetRot = Quaternion.Lerp(transform.rotation, _DoorTargetRot, _lerpspeed * Time.deltaTime);

            transform.rotation = SetRot;

            if (Quaternion.Angle(transform.rotation, _DoorTargetRot) > 3f)
            {
                yield return new WaitForEndOfFrame();
                StartCoroutine(CO_ChangeDoorStateCorotuine());
            }
            else
            {
                yield break;
            }
        }
        yield return null;
    }
}
