using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[][] points;

    private Transform child;

    private void Awake()
    {
        points = new Transform[transform.childCount][];
        for (int i = 0; i < points.Length; i++)
        {
            child = transform.GetChild(i);
            points[i] = new Transform[child.childCount];
            for (int j = 0; j < points[i].Length; j++)
            {
                points[i][j] = child.GetChild(j);
            }
        }
    }
}
