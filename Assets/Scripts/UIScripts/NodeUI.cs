using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text sellAmountText;
    [SerializeField] private Text upgradeCostText;
    [SerializeField] private Button upgradeButton;
    private Node target;

    public void SetTarget(Node node)
    {
        target = node;
        transform.position = target.GetBuildPosition();
        sellAmountText.text = "<b>SELL</B>\n$" + target.turretBlueprint.GetSellAmount(target.turretLevelIndex);
        if (target.turretLevelIndex >= target.turretBlueprint.turretPrefabs.Length - 1)
        {
            upgradeButton.interactable = false;
            upgradeCostText.text = "<b>MAX\nLEVEL</B>";
        }
        else
        {
            upgradeButton.interactable = true;
            upgradeCostText.text = "<b>UPGRADE</B>\n$" + target.turretBlueprint.costs[target.turretLevelIndex + 1];
        }
        canvas.SetActive(true);
    }
    
    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
    }

    public void Sell()
    {
        target.SellTurret();
    }
}
