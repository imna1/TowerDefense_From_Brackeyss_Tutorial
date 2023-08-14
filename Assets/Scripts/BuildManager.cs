using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject buildParticles;
    public GameObject sellParticles;

    [SerializeField] private NodeUI nodeUI;
    [SerializeField] private LayerMask layerMask;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoneyToBuild { get { return PlayerStats.money >= turretToBuild.costs[0]; } }

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    private void Awake()
    {
        instance = this;
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    
}
