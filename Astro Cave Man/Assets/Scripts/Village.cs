using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    public Vector3 village_position_bottom_left;
    public Vector3 village_position_top_right;
    public int food = 0;
    public int stone = 0;
    public int wood = 0;
    public int faith = 0;
    public int population = 0;

    public int kill_count = 0;
    public int prisoner_kill_count = 0;
    public int prisoner_converted_count = 0;

    public List<AI> villagers = new List<AI>();
    private AI selected_villager;
    public List<Building> buildings = new List<Building>(); //List of all buildings, add them manually for debugging
    private float timer = 0.0f;
    public STAR_SIGN allegiance = STAR_SIGN.NULL;

    private bool JustDelayedStart = true;


    private void Start()
    {
    }

    void Update()
    {
        population = villagers.Count;

        timer += Time.deltaTime;
        if(timer > 5) //Every X seconds
        {
            timer = 0;
            foreach(Building b in buildings) //Auto-harvest all buildings in this village
            {
                addResource(b.GetResourceType(), b.Harvest());
            }
        }
        if (JustDelayedStart)
        {
            allegiance = GetComponentInChildren<PlayerControl>().allegiance;
            JustDelayedStart = false;

            Building[] buildings = GetComponentsInChildren<Building>();

            foreach(Building b in buildings)
            {
                b.allegiance = allegiance;
                b.village = this;
            }

            AI[] units = this.transform.GetComponentsInChildren<AI>();

            foreach (AI ai in units)
            {
                ai.allegiance = this.allegiance;
                villagers.Add(ai);
            }

            population = villagers.Count;
        }

        AI[] possible_units = FindObjectsOfType<AI>();

        foreach (AI unit in possible_units)
        {
            if (villagers.Contains(unit))
                continue;

            if (unit.allegiance == this.allegiance)
            {
                villagers.Add(unit);
            }
        }

        population = villagers.Count;
    } 

    public Vector3 randomLocationWithinVillage()
    {
        float x = Random.Range(village_position_bottom_left.x, village_position_top_right.x);
        float y = Random.Range(village_position_bottom_left.y, village_position_top_right.y);
        float z = Random.Range(village_position_bottom_left.z, village_position_top_right.z);

        return new Vector3(x, y, z);
    }



    public void addVillager(AI new_villager)
    {
        if(!villagers.Contains(new_villager))
            villagers.Add(new_villager);
    }



    public void removeVillager(AI leaving_villager)
    {
        if (villagers.Contains(leaving_villager))
            villagers.Remove(leaving_villager);
    }



    public List<AI> getVillagerList()
    {
        return villagers;
    }

    public void addResource(RESOURCE type, int amount)
    {
        switch (type)
        {
            case RESOURCE.FAITH:
                faith += amount;
                Debug.Log(amount + " faith acquired");
                break;
            case RESOURCE.FOOD:
                food += amount;
                Debug.Log(amount + " food acquired");
                break;
            case RESOURCE.STONE:
                stone += amount;
                Debug.Log(amount + " stone acquired");
                break;
            case RESOURCE.WOOD:
                wood += amount;
                Debug.Log(amount + " wood acquired");
                break;
            default:
                Debug.Log("Invalid resource type added to village");
                break;
        }
    }
}
