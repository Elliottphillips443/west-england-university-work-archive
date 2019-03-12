using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class WalkingNPC : MonoBehaviour
{
    public int m_waypoints_reached_to_despawn = 5;

    private Waypoint m_current_waypoint;
    private Waypoint m_previous_waypoint;
    private NavMeshAgent m_navmesh_agent;
    private bool m_initialized = false;
    private int m_waypoints_reached = 0;



    public void Initialize(Waypoint initial_waypoint)
    {
        this.transform.position = initial_waypoint.transform.position;
        m_current_waypoint = initial_waypoint;

        m_initialized = true;
    }



    private void Start()
    {
        m_navmesh_agent = this.GetComponent<NavMeshAgent>();

        if(!m_navmesh_agent)
            print(this.name + " does not have a navmesh agent attached!");
    }



    private void Update ()
    {
        if (!m_initialized)
            return;

        float distance_from_waypoint = Vector3.Distance(this.transform.position, m_current_waypoint.transform.position);
        float distance_required = 0.25f;

		if(distance_from_waypoint <= distance_required)
        {
            m_waypoints_reached++;

            if (m_waypoints_reached >= m_waypoints_reached_to_despawn)
                Destroy(this.gameObject);

            Waypoint new_waypoint = m_current_waypoint.GetNextWaypoint(m_previous_waypoint);
            m_previous_waypoint = m_current_waypoint;
            m_current_waypoint = new_waypoint;

            UpdateWaypoint();
        }
	}



    private void UpdateWaypoint()
    {
        if (m_navmesh_agent.isOnNavMesh)
        {
            if (!m_navmesh_agent.SetDestination(m_current_waypoint.transform.position))
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
