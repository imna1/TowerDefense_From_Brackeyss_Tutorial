using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public string name;

    public GameObject[] turretPrefabs;
    public int[] costs;

    public int GetSellAmount(int turretLevelIndex)
    {
        float result = 0;
        for (int i = 0; i <= turretLevelIndex; i++)
        {
            result += costs[i] / 2;
        }
        return (int)result;
    }
}
