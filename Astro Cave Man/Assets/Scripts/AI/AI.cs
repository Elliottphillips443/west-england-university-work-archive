using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public STAR_SIGN allegiance = STAR_SIGN.NULL;
    public Village village;

    public JOB job = JOB.NONE;
    private Dictionary<JOB, int> skill_levels;

    private NavMeshAgent navigation_agent;
    public bool target_set = false;
    public bool target_reached = true;

    private bool searching = false;
    private float search_time = 0f;
    public float search_time_min = 3f;
    public float search_time_max = 8f;
    private float search_time_elapsed;

    public int item_scavenge_chance_min = 0;
    public int item_scavenge_chance_max = 10;
    public int item_scavange_modulo_check = 3;
    public bool temp_at_work = false;

    public float timeToLevelUp = 600.0f;
    private float timer = 0.0f;

    public int starting_health = 100;
    public int current_health;


    private void Start()
    {
        current_health = starting_health;

        if(!(navigation_agent = this.gameObject.GetComponent<NavMeshAgent>()))
            navigation_agent = this.gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;

        //this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

        skill_levels = new Dictionary<JOB, int>()
        {
            { JOB.BLACKSMITH,   1 },
            { JOB.BUILDER,      1 },
            { JOB.FARMER,       1 },
            { JOB.LUMBERJACK,   1 },
            { JOB.MINER,        1 },
            { JOB.SOLDIER,      1 }
        };

        if(!village)
        {
            Village[] villages = FindObjectsOfType<Village>();

            foreach(Village vil in villages)
            {
                if(vil.allegiance == this.allegiance)
                {
                    village = vil;
                    break;
                }
            }
        }
    }



    private void Update()
    {
        if (job != JOB.NONE)
        {
            timer += Time.deltaTime;                    //timer which will count up to "timeToLevelUp"
            if (job == JOB.FARMER)                      //if villager is a farmer
            {
                if (timer >= timeToLevelUp)             //if timer is bigger or equal to the time it takes to level up
                {
                    adjustSkillLevel(JOB.FARMER, 1);    //level up the farmer
                    timer = 0.0f;                       // reset the timer
                }                                       //will add a randomisation here later on (from building scrip) to give him a chance to level up few seconds faster
            }

            if (job == JOB.MINER)
            {
                if (timer >= timeToLevelUp)
                {
                    adjustSkillLevel(JOB.MINER, 1);
                    timer = 0.0f;
                }
            }

            if (job == JOB.LUMBERJACK)
            {
                if (timer >= timeToLevelUp)
                {
                    adjustSkillLevel(JOB.LUMBERJACK, 1);
                    timer = 0.0f;
                }
            }

            if (job == JOB.BLACKSMITH)
            {
                if (timer >= timeToLevelUp)
                {
                    adjustSkillLevel(JOB.BLACKSMITH, 1);
                    timer = 0.0f;
                }
            }
        }

        //pollVillageChange();
    }



    public void pollVillageChange()
    {
        if(this.allegiance != village.allegiance)
        {
            print("AI Allegiance: " + this.allegiance);
            print("Village Allegiance: " + village.allegiance);

            village.villagers.Remove(this);
            Village[] villages = FindObjectsOfType<Village>();

            foreach(Village vil in villages)
            {
                if(vil.allegiance == this.allegiance)
                {
                    vil.addVillager(this);
                    this.village = vil;
                }
            }
        }
    }



    public int getSkillLevel(JOB job_type)
    {
        return skill_levels[job_type];
    }



    public void adjustSkillLevel(JOB job_type, int change)
    {
        skill_levels[job_type] += change;
        Debug.Log("skill level / type " + skill_levels[job_type]);
    }



    public void setTarget(Transform target)
    {
        setTarget(target.position);
    }



    public void setTarget(Vector3 target)
    {
        navigation_agent.SetDestination(target);
    }



    public void takeDamage(int damage)
    {
        current_health -= damage;
        if(current_health <= 0)
        {
            village.removeVillager(this);
            Destroy(gameObject);
        }
    }
}
