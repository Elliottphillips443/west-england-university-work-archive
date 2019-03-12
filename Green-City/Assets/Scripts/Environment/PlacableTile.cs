using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableTile : MonoBehaviour
{
    public bool m_occupied = false;
    private GameObject m_tower = null;

    public GameObject GetTower()
    {
        return m_tower;
    }

    public void SetTower(GameObject tower)
    {
        m_tower = tower;
    }

    public void SellTower()
    {
        AdaptiveDifficulty.money_current += Mathf.FloorToInt(m_tower.GetComponent<TowerBase>().GetCost() / 2);
        Destroy(m_tower);
        m_tower = null;        
    }
}

//   GameObject m_myTower;
//// Use this for initialization
//void Start () {

//}

//   public void SpawnTower(string towerType)
//   {
//       if (!m_myTower)
//       {
//           switch (towerType)
//           {
//               case "Bin":
//                   m_myTower = Instantiate(Resources.Load("Towers&Trash/BinTower"), this.transform) as GameObject;
//                   break;
//               default:
//                   Debug.Log("tower type doesn't exist");
//                   break;
//           }
//       }
//   }

//// Update is called once per frame
//void Update () {

//}