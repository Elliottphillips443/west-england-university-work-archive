using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> m_adjacent_waypoints = new List<Waypoint>();


    
	public Waypoint GetNextWaypoint(Waypoint previous_waypoint)
    {
        int waypoint_index = Random.Range(0, m_adjacent_waypoints.Count);

        if (previous_waypoint)
        {
            while (m_adjacent_waypoints[waypoint_index] == previous_waypoint)
                waypoint_index = Random.Range(0, m_adjacent_waypoints.Count);
        }

        return m_adjacent_waypoints[waypoint_index];
    }
}
