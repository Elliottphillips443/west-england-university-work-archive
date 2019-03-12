using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingTower : TowerBase {

    protected override void Awake()
    {
        base.Awake();
        //Setup tower type
        m_towerType = TowerTypes.RECYCLING;

        //set resource type
        m_resourceType = "Recyclable";

        //set resource type for sphere collider
        m_radiusCollider.GetComponent<TowerRadius>().SetResourceTag(m_resourceType);

        //get base tower properties
        m_myProperties = PropertiesManager.Instance.GetProperties(m_currentLevel, m_towerType);

        //get the highest level possible for this upgrade line
        m_maxLevel = PropertiesManager.Instance.getMaxLevel(m_towerType);

        //set sprite based on tower properties
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(m_myProperties.spriteName);

        //setup collider
        m_radiusCollider.radius = m_myProperties.range;
        m_radiusCollider.GetComponent<TowerRadius>().SetSpeed(m_myProperties.rateOfFire);

        //setup tower range renderer
        UpdateRadiusRenderer();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
