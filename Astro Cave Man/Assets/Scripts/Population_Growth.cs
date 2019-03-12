using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population_Growth : MonoBehaviour
{
    public Village village;
    public STAR_SIGN allegiance;
    public JOB job;
    public GameObject ai_prefab;
    public float rate_of_growth = 0f;
    public float ticks_spawn_point = 10f;
    private float ticks_elapsed = 0f;
    public int village_population_limit = 10;
    public Transform spawn_point;


    private void Start()
    {
        if(!village)
        {
            if(!(village = this.GetComponent<Village>()))
            {
                village = this.gameObject.AddComponent(typeof(Village)) as Village;
            }
        }

        this.allegiance = village.allegiance;
    }



    private void findAllegiance()
    {
        this.allegiance = village.allegiance;
    }



    public void Update()
    {
        if (allegiance == STAR_SIGN.NULL)
            findAllegiance();

        ticks_elapsed += Time.deltaTime * (village.food * village.population * rate_of_growth);
        Debug.Log("s fro");
        if(ticks_elapsed >= ticks_spawn_point)
        {
            Debug.Log("GT");
            ticks_elapsed = 0f;
            spawnChild();
        }
    }



    private void spawnChild()
    {
        if (village.population >= village_population_limit)
            return;
        
        //Vector3 spawn_position = village.randomLocationWithinVillage();
        Vector3 spawn_position = spawn_point.position;
        Vector3 direction_facing = new Vector3(0f, Random.Range(0f, 360f), 0f);

        GameObject child = Instantiate(ai_prefab, spawn_position, Quaternion.Euler(direction_facing));
        AI child_ai = child.GetComponent<AI>();

        child_ai.village = village;
        child_ai.allegiance = allegiance;
        child_ai.job = JOB.NONE;
    }



   
}
