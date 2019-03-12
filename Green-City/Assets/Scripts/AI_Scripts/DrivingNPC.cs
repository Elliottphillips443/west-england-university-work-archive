using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class DrivingNPC : MonoBehaviour
{
    public int m_waypoints_reached_to_despawn = 5;
    public NavMeshAgent m_navmesh_agent;

    private RoadWaypoint m_previous_waypoint;
    private RoadWaypoint m_current_waypoint;
    private GameObject m_other_car;
    private bool m_initialized = false;
    private int m_waypoints_reached = 0;
    private bool m_waiting = false;
    private float m_wait_time = 0.5f;
    private float m_wait_time_elapsed = 0f;


    public void Initialize(RoadWaypoint initial_waypoint)
    {
        this.transform.position = initial_waypoint.transform.position;
        m_current_waypoint = initial_waypoint;

        m_initialized = true;
    }



    private void Start()
    {
        m_navmesh_agent = this.GetComponent<NavMeshAgent>();

        if (!m_navmesh_agent)
            print(this.name + " does not have a navmesh agent attached!");
    }



    void Update()
    {
        if (m_waiting)
        {
            m_wait_time_elapsed += Time.deltaTime;

            if(m_wait_time <= m_wait_time_elapsed)
            {
                m_wait_time_elapsed = 0.0f;
                m_waiting = false;
                m_navmesh_agent.enabled = true;
                UpdateWaypoint();
            }
        }


        if (!m_initialized || !m_current_waypoint.m_can_get_next_waypoint)
            return;

        float distance_from_waypoint = Vector3.Distance(this.transform.position, m_current_waypoint.transform.position);
        float distance_required = 0.25f;

        if (distance_from_waypoint <= distance_required)
        {
            m_waypoints_reached++;

            if (m_waypoints_reached >= m_waypoints_reached_to_despawn)
                Destroy(this.gameObject);

            RoadWaypoint new_waypoint = m_current_waypoint.GetNextWaypoint(m_previous_waypoint);
            m_previous_waypoint = m_current_waypoint;
            m_current_waypoint = new_waypoint;

            UpdateWaypoint();
        }
    }



    private void UpdateWaypoint()
    {
        if (!m_navmesh_agent.SetDestination(m_current_waypoint.transform.position))
            Destroy(this.gameObject);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Car")
            return;

        DrivingNPC npc = other.GetComponent<DrivingNPC>();

        if (npc.m_current_waypoint != m_current_waypoint)
            return;

        float other_distance = Vector3.Distance(other.gameObject.transform.position, m_current_waypoint.transform.position);
        float this_distance = Vector3.Distance(this.gameObject.transform.position, m_current_waypoint.transform.position);

        if (other_distance < this_distance)
        {
            m_other_car = other.gameObject;
            m_navmesh_agent.enabled = false;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_other_car)
            m_waiting = true;
    }
}
