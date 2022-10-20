using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStairControl : MonoBehaviour
{
    public bool keepGoing = false;

    public void OnTriggerEnter(Collider other)
    {
        keepGoing = true;
    }
}