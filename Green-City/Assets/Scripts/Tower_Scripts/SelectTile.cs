using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    public float m_mouse_cast_max_distance = 500f;
    public LayerMask m_layer;
    public GameObject m_selected_tile;
    public GameObject m_tower;
    public TowerTypes m_towerType = TowerTypes.NONE;
    public GameObject m_cityController;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit_info;
                bool hit = Physics.Raycast(ray, out hit_info, m_mouse_cast_max_distance, m_layer);

            if (hit)
            {
                m_selected_tile = hit_info.collider.gameObject;
                PlacableTile tile = m_selected_tile.GetComponent<PlacableTile>();
                if (!tile.GetTower() && m_tower)
                {
                    //make sure that the player can afford to place the tower
                    if (AdaptiveDifficulty.money_current - PropertiesManager.Instance.GetProperties(1,m_towerType).cost >= 0)
                    {
                        GameObject tower = Instantiate(m_tower, m_selected_tile.transform.position, m_tower.transform.rotation);
                        tile.SetTower(tower);
                        tile.m_occupied = true;
                        AdaptiveDifficulty.money_current -= tower.GetComponent<TowerBase>().GetCost();
                    }
                    else
                    {
                        Debug.Log("not enough money!");
                    }
                }
                else if(m_towerType == TowerTypes.SELL && tile.GetTower())
                {
                    tile.SellTower();
                }
            }
        }
    }
}