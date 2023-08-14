using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TurretBlueprint[] turretBlueprints;

    [SerializeField] private Text[] turretTexts;

    [SerializeField] private Color haventEnoughMoneyColor;

    private BuildManager buildManager;
    private Color defaultColor;

    private void Start()
    {
        buildManager = BuildManager.instance;
        for (int i = 0; i < turretTexts.Length; i++)
        {
            turretTexts[i].text = "$" + turretBlueprints[i].costs[0];
        }
        
        defaultColor = turretTexts[0].color;
        EventBus.UpdateMoneyEvent.AddListener(OnUpdateMoney);
        OnUpdateMoney();
    }
    private void OnUpdateMoney()
    {
        for (int i = 0; i < turretBlueprints.Length; i++)
        {
            if (PlayerStats.money >= turretBlueprints[i].costs[0])
            {
                turretTexts[i].color = defaultColor;
            }
            else
            {
                turretTexts[i].color = haventEnoughMoneyColor;
            }
        }
    }

    public void SelectStandartTurret()
    {
        buildManager.SelectTurretToBuild(turretBlueprints[0]);
    }
    public void SelectMissleLauncher()
    {
        buildManager.SelectTurretToBuild(turretBlueprints[1]);
    }
    public void SelectLaserBeamer()
    {
        buildManager.SelectTurretToBuild(turretBlueprints[2]);
    }
}
