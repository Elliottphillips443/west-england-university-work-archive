using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour {
    //I'm sorry about it looking bad but it's the best way to lay it out :/
    //[SerializeField]
    //protected float m_range;  //how far is its effect reaching
    //[SerializeField]
    //protected float m_rateOfFire; //for bins read as rate of suction
    //[SerializeField]
    //protected int m_cost;   //how much the player must pay to place
    [SerializeField]
    protected int m_resourceCount = 0;  //total lifetime resources collected/distributed
    [SerializeField]
    protected int m_upgradeCount = 0;   //resources currently obtained towards the next upgrade
    //[SerializeField]
    //protected int m_forNextUpgrade;    //resource requirement to upgrade
    [SerializeField]
    protected int m_currentLevel = 1;   //current level of the tower
    [SerializeField]
    protected int m_maxLevel = 2;   //highest level the tower can reach
    //[SerializeField]
    //protected Sprite m_sprite;  //sprite of the tower, changes at certain levels
    [SerializeField]
    protected SphereCollider m_radiusCollider;
    [SerializeField]
    protected LineRenderer m_radiusRenderer;  //used to display the tower's effect radius
    [SerializeField]
    protected int m_segments = 50; //number of line segments in the line renderer
    [SerializeField]
    protected GameObject m_myTile;
    [SerializeField]
    protected TowerTypes m_towerType;
    [SerializeField]
    protected string m_resourceType;

    [SerializeField]
    protected TowerProperties m_myProperties; 
    //stores all of the tower's current properties:
    //range
    //rate of fire (pull speed)
    //cost
    //resources needed for next upgrade
    //file path to the sprite to use

    public int GetResourceCount()
    {
        return m_resourceCount;
    }

    public int GetCost()
    {
        //return m_myProperties.cost;
        return m_myProperties.cost;
    }

	// Use this for initialization
	protected virtual void Awake () {
        m_radiusCollider = GetComponentInChildren<SphereCollider>();
        if(gameObject.transform.parent)
        {
            m_myTile = gameObject.transform.parent.gameObject;
        }

        //one time setup for tower radius display
        m_radiusRenderer = gameObject.AddComponent<LineRenderer>();
        m_radiusRenderer.positionCount = m_segments + 1;
        m_radiusRenderer.useWorldSpace = false;
        m_radiusRenderer.material = Resources.Load("Materials/TowerRadius") as Material;
        m_radiusRenderer.receiveShadows = false;
        m_radiusRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        m_radiusRenderer.startWidth = 0.1f;
        m_radiusRenderer.endWidth = 0.1f;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

    //called when the tower upgrades
    protected void SetProperties()
    {
        m_myProperties = PropertiesManager.Instance.GetProperties(m_currentLevel, m_towerType);
    }

    //called once to setup the radius and once more each time the tower radius changes (through upgrades)
    //this function draws a circle around the tower
    protected void UpdateRadiusRenderer()
    {
        float x, y;
        float angle = 0f;

        for (int i = 0; i < (m_segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * m_myProperties.range;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * m_myProperties.range;

            m_radiusRenderer.SetPosition(i, new Vector3(x, y, 0));
            angle += (360f / (m_segments));
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == m_resourceType)
        {
            GameObject.Destroy(other.gameObject);
            m_resourceCount++;
            m_upgradeCount++;
            if(m_upgradeCount >= m_myProperties.forNextUpgrade && m_currentLevel < m_maxLevel)
            {
                Upgrade();
            }
            AdaptiveDifficulty.money_current += 2;
        }
    }

    protected void Upgrade()
    {
        m_currentLevel++;
        m_myProperties = PropertiesManager.Instance.GetProperties(m_currentLevel, m_towerType);
        m_upgradeCount = 0;
        m_radiusCollider.radius = m_myProperties.range;
        m_radiusCollider.GetComponent<TowerRadius>().SetSpeed(m_myProperties.rateOfFire);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(m_myProperties.spriteName);
        UpdateRadiusRenderer();
    }
}
