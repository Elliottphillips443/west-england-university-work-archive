using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    private CityGenerator m_city_generator;
    private List<GameObject> m_placed_city_blocks = new List<GameObject>();
    private List<Waypoint> m_waypoints = new List<Waypoint>();
    private List<RoadWaypoint> m_road_waypoints = new List<RoadWaypoint>();

    [SerializeField]
    private float m_npc_walking_spawn_time = 1.0f;
    private float m_npc_walking_spawn_time_elapsed = 0.0f;

    [SerializeField]
    private float m_npc_car_spawn_time = 5.0f;
    private float m_npc_car_spawn_time_elapsed = 0.0f;

    public GameObject m_walking_npc_parent;
    public GameObject m_car_npc_parent;
    public List<GameObject> m_walking_npcs = new List<GameObject>();
    public List<GameObject> m_car_npcs = new List<GameObject>();



    private void Start ()
    {
        m_city_generator = this.GetComponent<CityGenerator>();

        if (!m_city_generator)
        {
            print(this.name + " does not have a CityGenerator attached!");
            return;
        }

        UpdateWaypointList();
        UpdateRoadWaypointList();
	}



    private void Update ()
    {
        m_npc_walking_spawn_time_elapsed += Time.deltaTime;
        m_npc_car_spawn_time_elapsed += Time.deltaTime;

        if(m_npc_walking_spawn_time <= m_npc_walking_spawn_time_elapsed)
        {
            m_npc_walking_spawn_time_elapsed = 0.0f;
            UpdateWaypointList();
            SpawnNPCWalking();
        }    

        if(m_npc_car_spawn_time <= m_npc_car_spawn_time_elapsed)
        {
            m_npc_car_spawn_time_elapsed = 0.0f;
            UpdateRoadWaypointList();
            SpawnNPCCar();
        }
	}



    private void UpdateWaypointList()
    {
        foreach (GameObject placed_city_block in m_city_generator.m_placed_city_blocks)
        {
            foreach (Transform child in placed_city_block.transform)
            {
                if (child.tag == "Waypoints")
                {
                    foreach(Transform waypoint_child in child)
                    {
                        m_waypoints.Add(waypoint_child.gameObject.GetComponent<Waypoint>());
                    }

                    break;
                }
            }
        }
    }



    private void UpdateRoadWaypointList()
    {
        foreach (GameObject placed_city_block in m_city_generator.m_placed_city_blocks)
        {
            foreach (Transform child in placed_city_block.transform)
            {
                if (child.tag == "CarWaypoints")
                {
                    foreach (Transform waypoint_child in child)
                    {
                        if (waypoint_child.tag == "CarWaypoint")
                        {
                            m_road_waypoints.Add(waypoint_child.gameObject.GetComponent<RoadWaypoint>());
                        }
                    }

                    break;
                }
            }
        }
    }



    private void SpawnNPCWalking()
    {
        int npc_index = Random.Range(0, m_walking_npcs.Count);
        GameObject npc = Instantiate(m_walking_npcs[npc_index], m_walking_npcs[npc_index].transform.position, m_walking_npcs[npc_index].transform.rotation, m_walking_npc_parent.transform);

        WalkingNPC npc_script = npc.GetComponent<WalkingNPC>();
        if(!npc_script)
        {
            print(npc.name + " doesn't have WalkingNPC script attached!");
            Destroy(npc);
            return;
        }

        int waypoint_index = Random.Range(0, m_waypoints.Count);
        npc_script.Initialize(m_waypoints[waypoint_index]);
    }



    private void SpawnNPCCar()
    {
        int npc_index = Random.Range(0, m_car_npcs.Count);
        GameObject npc = Instantiate(m_car_npcs[npc_index], m_car_npcs[npc_index].transform.position, m_car_npcs[npc_index].transform.rotation, m_car_npc_parent.transform);

        DrivingNPC npc_script = npc.GetComponent<DrivingNPC>();
        if(!npc_script)
        {
            print(npc.name + " doesn't have DrivingNPC script attached!");
            Destroy(npc);
            return;
        }

        int waypoint_index = Random.Range(0, m_road_waypoints.Count);
        npc_script.Initialize(m_road_waypoints[waypoint_index]);
    }
}
