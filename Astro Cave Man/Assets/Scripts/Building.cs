using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum BUILDINGTYPE
    {
        FARM,
        BARRACKS,
        SHRINE,
        FORGE,
        MINE,
        LUMBERMILL,
        WALL
    }

    public STAR_SIGN allegiance = STAR_SIGN.NULL;
    public Village village;

    private float level_up_meter = 0.0f;
    public int level = 1;
    public int health = 100;
    private int max_health = 100;
    public List<AI> units;
    public BUILDINGTYPE type;
    private float resource_timer = 0.0f;
    public int resource = 0;
    JOB job_type = JOB.NONE;

    // Use this for initialization
    void Start ()
    {
        findJobType();
        //Depending on building type, change stats like maximum health
        //Add others if you think of any other differences they should have
        switch (type)
        {
            case BUILDINGTYPE.WALL:
                max_health = 250;
                break;
            case BUILDINGTYPE.SHRINE:
                max_health = 500;
                break;
        }
        health = max_health;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!village)
            return;

        pollUpdatingUnitList();

        if (health <= 0)
        {
            health = 0;
        }
        resource_timer += Time.deltaTime;
        if(resource_timer > 1)
        {
            resource_timer = 0;

            //Generate resources proportional to number of assigned units and level
            switch (type)
            {
                //Most buildings generate a generic "resource" which is only assigned a type when harvested by the player
                case BUILDINGTYPE.FARM:                                         //if building is a farm
                    foreach (AI unit in units)                                  //depending how many villagers are assigned to the farm
                    {
                        if (unit.job == JOB.FARMER)                             //and assuming those villagers are farmers
                        {
                            if (health != max_health)
                            {
                                health ++;
                                Debug.Log("Repairing");
                            }
                            else if (health == max_health)
                            {
                                Debug.Log("Current health: " + health + " out of " + max_health);
                                resource += unit.getSkillLevel(JOB.FARMER) * level; //add X amount of resources multiplied by buildings level eg:
                            }                                                       //if 1 farmer is assigned, and hes level 3, and buildings level is 2 then he generates 6 food per tick
                        }                                                       
                    }
                    break;
                case BUILDINGTYPE.FORGE:
                    foreach (AI unit in units)
                    {
                        if (unit.job == JOB.BLACKSMITH)
                        {
                            resource += unit.getSkillLevel(JOB.BLACKSMITH) * level;
                        }
                    }
                    break;
                case BUILDINGTYPE.LUMBERMILL:
                    foreach (AI unit in units)
                    {
                        if (unit.job == JOB.LUMBERJACK)
                        {
                            resource += unit.getSkillLevel(JOB.LUMBERJACK) * level;
                        }
                    }
                    break;
                case BUILDINGTYPE.MINE:
                    foreach (AI unit in units)
                    {
                        if (unit.job == JOB.MINER)
                        {
                            resource += unit.getSkillLevel(JOB.MINER) * level;
                        }
                    }
                    break;
                case BUILDINGTYPE.SHRINE:
                    resource += units.Count * level;
                    break;
            }
        }

        //int total_builder_skill_level = 0;
        //int number_of_builders = 0;
        //foreach(AI unit in units)
        //{
        //    if(unit.job == JOB.BUILDER)
        //    {
        //        total_builder_skill_level += unit.getSkillLevel(JOB.BUILDER);
        //        number_of_builders++;
        //    }
        //}
        //if(total_builder_skill_level == 0 && number_of_builders != 0)
        //{
        //    total_builder_skill_level++; //Make sure at least some building is being done even if all builders are level 0 (though they shouldn't be, since AI starts at level 1)
        //}
        

        //level_up_meter += Time.deltaTime * total_builder_skill_level;
        //if(level_up_meter > (10 * level))
        //{
        //    level++;
        //    max_health += 50; //Buildings get more health as they level up, making them harder to break
        //    health += 50;
        //    level_up_meter = 0;
        //    //Builders level up when the building does
        //    foreach (AI unit in units)
        //    {
        //        if (unit.job == JOB.BUILDER)
        //        {
        //            unit.adjustSkillLevel(JOB.BUILDER, 1);
        //        }
        //    }
        //}
	}

    private void findJobType()
    {
        switch (type)
        {
            case Building.BUILDINGTYPE.FARM:
                job_type = JOB.FARMER;
                break;

            case Building.BUILDINGTYPE.BARRACKS:
                job_type = JOB.SOLDIER;
                break;

            case Building.BUILDINGTYPE.FORGE:
                job_type = JOB.BLACKSMITH;
                break;

            case Building.BUILDINGTYPE.MINE:
                job_type = JOB.MINER;
                break;

            case Building.BUILDINGTYPE.LUMBERMILL:
                job_type = JOB.LUMBERJACK;
                break;
        }
    }

    private void pollUpdatingUnitList()
    {
        foreach (AI ai in village.villagers)
        {
            if (units.Contains(ai))
            {
                if (ai.job != job_type)
                {
                    units.Remove(ai);
                }
            }
            else
            {
                if (ai.job == job_type)
                {
                    units.Add(ai);
                }
            }
        }
    }

    public void takeDamage(int damage)
    {
        print("DAMAGE: " + damage);
        health -= damage;
        if(health <= 0)
        {
            if(level>1)
            {
                level--;
                max_health -= 50; //Remove bonus health when broken
                health = max_health;
            }
        }
    }

    public int Harvest()
    {
        int temp_resources = resource;
        resource = 0;
        return temp_resources;
    }

    public RESOURCE GetResourceType()
    {
        switch (type)
        {
            case BUILDINGTYPE.FARM:
                return RESOURCE.FOOD;
            case BUILDINGTYPE.LUMBERMILL:
                return RESOURCE.WOOD;
            case BUILDINGTYPE.MINE:
                return RESOURCE.STONE;
            case BUILDINGTYPE.SHRINE:
                return RESOURCE.FAITH;
            default:
                Debug.Log("trying to harvest a non-resource building");
                return RESOURCE.NULL;
        }
    }
}