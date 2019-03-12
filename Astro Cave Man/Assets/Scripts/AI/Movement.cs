using UnityEngine;
using System.Collections;
using UnityEditor;

public class Movement : MonoBehaviour
{
    //public Transform targetA;
    public GameObject target;
    UnityEngine.AI.NavMeshAgent nav;
    RaycastHit hitInfo = new RaycastHit();
    bool CanSee = false;
    public float fov = 45f;                 // Field of View (in degrees) - Changes angle at which enemies can detect the player
    public float sightDist = 10.0f;         // Sigh Distance - Lower this if you want your enemies to have smaller vision range
    public float heightMultiplier = 0.5f;   // Height Multiplier - Height of the rays (e.g. could be set so that rays come out of the eyes)

    private AI ai;
    private Building building;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("NPC");
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ai = GetComponent<AI>();
    }


    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject closest in targets)
        {
            if (Vector3.Distance(closest.transform.position, gameObject.transform.position) < Vector3.Distance(target.transform.position, gameObject.transform.position))
            {
                target = closest;
            }
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    RaycastHit hit;

        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        //    {
        //        nav.destination = hit.point;
        //    }
        //}

        Vector3 direction = target.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green); // front facing ray

        var rotationRight = Quaternion.AngleAxis(fov, transform.up) * transform.forward;    // Right border of the cone of vision
        var rotationLeft = Quaternion.AngleAxis(-fov, transform.up) * transform.forward;    // Left border of the cone of vision

        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, rotationRight * sightDist, Color.cyan); // right
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, rotationLeft * sightDist, Color.cyan); // left


        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hitInfo, sightDist) ||
            Physics.Raycast(transform.position + Vector3.up * heightMultiplier, rotationRight, out hitInfo, sightDist) ||
            Physics.Raycast(transform.position + Vector3.up * heightMultiplier, rotationLeft, out hitInfo, sightDist))
        {
            if (hitInfo.collider.gameObject.tag == "NPC" && hitInfo.collider.GetComponent<AI>().allegiance != ai.allegiance)
            {
                CanSee = true;
            }
            else if (hitInfo.collider.gameObject.tag != "NPC" || hitInfo.collider.GetComponent<AI>().allegiance == ai.allegiance)
            {
                CanSee = false;
            }
        }
        if (CanSee == true)
        {
            //nav.destination = target.transform.position;  // chase the target
        }
        else if (CanSee == false)
        {
            // check for targets last know position then stop chasing if you still cannot see him
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Building")
        {
            Building temp_building = other.gameObject.GetComponent<Building>();
            // teleport AI to a designated slot on the farm
            if (temp_building.allegiance == ai.allegiance)
            {
                switch (temp_building.type)
                {
                    case Building.BUILDINGTYPE.FARM:
                        ai.job = JOB.FARMER;
                        Debug.Log("Farming...");
                        break;

                    case Building.BUILDINGTYPE.BARRACKS:
                        ai.job = JOB.SOLDIER;
                        Debug.Log("Training...");
                        break;

                    case Building.BUILDINGTYPE.FORGE:
                        ai.job = JOB.MINER;
                        Debug.Log("Mining...");
                        break;

                    case Building.BUILDINGTYPE.MINE:
                        ai.job = JOB.LUMBERJACK;
                        Debug.Log("Chopping...");
                        break;

                    case Building.BUILDINGTYPE.LUMBERMILL:
                        ai.job = JOB.BLACKSMITH;
                        Debug.Log("Smashing...");
                        break;

                    default:
                        Debug.Log("Job not assigned");
                        break;
                }
            }

            if (other.gameObject.GetComponent<Building>().allegiance != ai.allegiance)
            {
                Debug.Log("Get out of my village!");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Building")
        {
            if (other.gameObject.GetComponent<Building>().allegiance != ai.allegiance)
            {
                ai.job = JOB.SOLDIER;
                Debug.Log("No longer working.");
            }
        }
    }

    public void GiveTarget(Vector3 target)
    {
        nav.destination = target;
        //if (hitInfo.collider.gameObject.tag == "NPC")
        //{
         //   target = GameObject.FindGameObjectWithTag("NPC").transform;
        //}
    }
}
