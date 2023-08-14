using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public GameObject turret;
    public TurretBlueprint turretBlueprint;
    public int turretLevelIndex = 0;

    [SerializeField] private Color coverColor;
    [SerializeField] private Color notEnoughCoverColor;
    [SerializeField] private Vector3 offset;

    private Renderer rend;
    private Color defaltColor;
    private BuildManager buildManager;
    private bool isSelected;

    private void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        defaltColor = rend.material.color;
        EventBus.UpdateMoneyEvent.AddListener(OnUpdateMoney);
    }

    private void OnUpdateMoney()
    {
        if (turret != null)
            return;
        if (!isSelected)
            return;
        if (!buildManager.CanBuild)
            return;
        if (buildManager.HasMoneyToBuild)
        {
            rend.material.color = coverColor;
        }
        else
        {
            rend.material.color = notEnoughCoverColor;
        }
    }
    private void OnMouseEnter()
    {
        if (turret != null)
        {
            rend.material.color = coverColor;
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;
        isSelected = true;
        if (buildManager.HasMoneyToBuild)
        {
            rend.material.color = coverColor;
        }
        else
        {
            rend.material.color = notEnoughCoverColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = defaltColor;
        isSelected = false;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild)
            return;
        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {
        turretBlueprint = blueprint;
        if (PlayerStats.money < turretBlueprint.costs[0])
        {
            return;
        }

        PlayerStats.money -= turretBlueprint.costs[0];
        EventBus.UpdateMoneyEvent.Invoke();
        turret = Instantiate(turretBlueprint.turretPrefabs[0], GetBuildPosition(), Quaternion.identity);
        GameObject buildEffect = Instantiate(buildManager.buildParticles, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 2f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBlueprint.costs[turretLevelIndex + 1])
        {
            return;
        }
        Quaternion rotation = turret.transform.rotation;
        Destroy(turret);
        turretLevelIndex++;
        PlayerStats.money -= turretBlueprint.costs[turretLevelIndex];
        EventBus.UpdateMoneyEvent.Invoke();
        buildManager.DeselectNode();
        turret = Instantiate(turretBlueprint.turretPrefabs[turretLevelIndex], GetBuildPosition(), rotation);
        GameObject buildEffect = Instantiate(buildManager.buildParticles, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 2f);
    }

    public void SellTurret()
    {
        PlayerStats.money += turretBlueprint.GetSellAmount(turretLevelIndex);
        turretLevelIndex = 0;
        EventBus.UpdateMoneyEvent.Invoke();
        buildManager.DeselectNode();
        Destroy(turret);
        GameObject buildEffect = Instantiate(buildManager.sellParticles, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 2f);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + offset;
    }
}
