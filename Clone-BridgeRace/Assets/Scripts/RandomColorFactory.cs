using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorFactory : MonoBehaviour
{
    [SerializeField] public Color[] colors = { Color.blue, Color.red, Color.green };
    [HideInInspector] public List<Color> Colors;

    private int _randomNumber;
    public static RandomColorFactory Instance;

    public GameObject AI_1;
    public GameObject AI_2;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < colors.Length; i++)
        {
            Colors.Add(colors[i]);
        }
    }

    public void ChangeColor(Material getMaterial)
    {
        _randomNumber = Random.Range(0, Colors.Count);
        getMaterial.color = Colors[_randomNumber];
        Colors.RemoveAt(_randomNumber);
    }

    public void SetMe()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            Colors.Add(colors[i]);
        }
    }
}