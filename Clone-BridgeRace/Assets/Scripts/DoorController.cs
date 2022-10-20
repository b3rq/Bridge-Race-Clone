using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent OpenDoorEvent;

    public void Interact()
    {
        OpenDoorEvent?.Invoke();
    }
}