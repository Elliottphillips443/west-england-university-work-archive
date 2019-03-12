using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    public static TowerTypes m_currentTowerType = TowerTypes.NONE;
    private static SelectTile m_selectTile;

	// Use this for initialization
	void Start ()
    {
        m_selectTile = this.GetComponent<SelectTile>();	
	}
	
    public static void SetBins()
    {
        m_currentTowerType = TowerTypes.BIN;
        m_selectTile.m_tower = Resources.Load("Towers&Trash/BinTower") as GameObject;
        m_selectTile.m_towerType = m_currentTowerType;
    }

    public static void SetRecycling()
    {
        m_currentTowerType = TowerTypes.RECYCLING;
        m_selectTile.m_tower = Resources.Load("Towers&Trash/RecyclingTower") as GameObject;
        m_selectTile.m_towerType = m_currentTowerType;
    }

    public static void SetSell()
    {
        m_currentTowerType = TowerTypes.SELL;
        m_selectTile.m_tower = null;
        m_selectTile.m_towerType = m_currentTowerType;
    }
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetBins();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetRecycling();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSell();
        }
        //right click to cancel tower placement
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            m_currentTowerType = TowerTypes.NONE;
            m_selectTile.m_tower = null;
            m_selectTile.m_towerType = m_currentTowerType;
            EventSystem.current.SetSelectedGameObject(null);
        }
	}
}