using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadWaypoint : MonoBehaviour
{
    public List<RoadWaypoint> m_adjacent_waypoints = new List<RoadWaypoint>();
    public RoadWaypoint m_next_traffic_light;
    public bool m_can_get_next_waypoint = false;
    public float m_duration_enabled = 3.0f;
    public float m_duration_enabled_elapsed = 0f;



    private void Start()
    {
        if (!m_next_traffic_light)
            m_can_get_next_waypoint = true;
    }



    private void Update()
    {
        if (!m_can_get_next_waypoint || !m_next_traffic_light)
            return;

        m_duration_enabled_elapsed += Time.deltaTime;
        
        if(m_duration_enabled <= m_duration_enabled_elapsed)
        {
            m_duration_enabled_elapsed = 0.0f;
            m_can_get_next_waypoint = false;
            m_next_traffic_light.m_can_get_next_waypoint = true;
        }
    }



    public RoadWaypoint GetNextWaypoint(RoadWaypoint previous_waypoint)
    {
        if (!m_can_get_next_waypoint)
            return null;

        int waypoint_index = Random.Range(0, m_adjacent_waypoints.Count);

        if (previous_waypoint)
        {
            while (m_adjacent_waypoints[waypoint_index] == previous_waypoint)
                waypoint_index = Random.Range(0, m_adjacent_waypoints.Count);
        }

        return m_adjacent_waypoints[waypoint_index];
    }
}
