using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public static BlockController Instance;


    public AI AI_1;
    public AI AI_2;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AI_1 = RandomColorFactory.Instance.AI_1.GetComponent<AI>();
        AI_2 = RandomColorFactory.Instance.AI_2.GetComponent<AI>();
    }

    public void ColorIsItTopLevel(GameObject block)
    {
        if (CharacterRotate.Instance.KeepGoing && block.GetComponent<Renderer>().material.color == CharacterRotate.Instance.DoorTriggerCheck.GetComponent<MeshRenderer>().material.color)
        {
            block.SetActive(false);
        }
        if (RandomColorFactory.Instance.AI_1.GetComponent<AI>().KeepGoing == true && block.GetComponent<Renderer>().material.color == AI_1.DoorTriggerCheck.GetComponent<MeshRenderer>().material.color)
        {
            block.SetActive(false);
        }
        if (RandomColorFactory.Instance.AI_2.GetComponent<AI>().KeepGoing && block.GetComponent<Renderer>().material.color == AI_2.DoorTriggerCheck.GetComponent<MeshRenderer>().material.color)
        {
            block.SetActive(false);
        }
    }
}