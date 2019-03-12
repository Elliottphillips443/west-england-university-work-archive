using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    public int startingHealth = 100;
    public int currentHealth;
    public int attackDamage = 10;
    public float timeBetweenAttacks = 0.5f;

    public int dmg_min = 10;
    public int dmg_max = 20;


    public GameObject enemy;
    public bool enemy_in_range = false;
    public bool building_in_range = false;
    float timer = 0.0f;

    private AI ai;
    private Building building;

    // Use this for initialization
    void Start ()
    {
        currentHealth = startingHealth;
        ai = GetComponent<AI>();
        enemy = GameObject.FindGameObjectWithTag("NPC");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (other.gameObject.GetComponent<AI>().allegiance != ai.allegiance)
            {
                enemy_in_range = true;
            }
        }
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Building")
        {
            print("Building toggled");
            Building temp_building;
            if(temp_building = other.GetComponent<Building>())
            {
                if (temp_building.allegiance != ai.allegiance)
                {
                    print("Building in range");
                    building = temp_building;
                    building_in_range = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (other.gameObject.GetComponent<AI>().allegiance != ai.allegiance)
            {
                print("WHY THOUGH");
                enemy_in_range = false;
            }
        }
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Building")
        {
            if (other.gameObject.GetComponent<Building>().allegiance != ai.allegiance)
            {
                print("Lost focus of building");
                building_in_range = false;
            }
        }
    }
    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && enemy_in_range && currentHealth > 0)
        {
            Attack();
        }
        
        if (timer >= timeBetweenAttacks && building_in_range && currentHealth > 0)
        {
            print("Attacking_building");
            building.takeDamage(attackDamage);
            timer = 0f;
        }

        //if (timer >= timeBetweenAttacks)
        //    timer = 0f;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
            var villages = GameObject.FindGameObjectsWithTag("Village");
            foreach (GameObject vil in villages)
            {
                if (vil.GetComponent<Village>().allegiance == ai.allegiance)
                {
                    vil.GetComponent<Village>().population -= 1;
                }
            }
        }
    }

    public void Attack()
    {
        timer = 0f;

        if (currentHealth > 0)
        {
            attackDamage = Random.Range(dmg_min, dmg_max);
            TakeDamage(attackDamage);
        }
    }

    void TakeDamage(int attackDamage)
    {
        currentHealth -= attackDamage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
