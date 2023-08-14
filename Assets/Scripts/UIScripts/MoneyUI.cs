using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Text moneyText;

    private void Update()
    {
        moneyText.text = PlayerStats.money.ToString();
    }
}
