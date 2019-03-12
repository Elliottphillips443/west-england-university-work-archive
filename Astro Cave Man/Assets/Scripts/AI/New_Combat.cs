using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Combat : MonoBehaviour
{
    public int attack_damage = 0;
    public int base_damage = 10;
    public float timer = 0f;
    public float time_between_attacks = 1f;
    
    public int damage_offset = 3;

    public GameObject target;
    public bool enemy_ai_in_range = false;
    public bool enemy_building_in_range = false;


    private AI ai;
    public AI ai_target;
    public Building building_target;

	void Start ()
    {
        ai = this.GetComponent<AI>();
	}
	
	void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= time_between_attacks)
            Attack();
	}



    private void Attack()
    {
        if (ai.current_health > 0)
        {
            if (enemy_ai_in_range)
            {
                attack_damage = Random.Range(base_damage - damage_offset, base_damage + damage_offset);
                ai_target.takeDamage(attack_damage);
                timer = 0f;
            }
            else if (enemy_building_in_range)
            {
                attack_damage = Random.Range(base_damage - damage_offset, base_damage + damage_offset);
                building_target.takeDamage(attack_damage);
                timer = 0f;
            }
        }
        else
        {
            ai.village.removeVillager(ai);
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {
            AI potential_ai_target;
            if(potential_ai_target = other.gameObject.GetComponent<AI>())
            {
                if (potential_ai_target.allegiance != ai.allegiance)
                {
                    enemy_ai_in_range = true;
                    ai_target = potential_ai_target;
                }
            }
        }
        else if(other.gameObject.tag == "Building" || other.gameObject.tag == "Wall")
        {
            Building potential_building_target;
            if (potential_building_target = other.gameObject.GetComponent<Building>())
            {
                if (potential_building_target.allegiance != ai.allegiance)
                {
                    enemy_building_in_range = true;
                    building_target = potential_building_target;
                }
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            AI potential_ai_target;
            if (potential_ai_target = other.gameObject.GetComponent<AI>())
            {
                if (potential_ai_target == ai_target)
                {
                    enemy_ai_in_range = false;
                    ai_target = null;
                }
            }
        }
        else if (other.gameObject.tag == "Building" || other.gameObject.tag == "Wall")
        {
            Building potential_building_target;
            if (potential_building_target = other.gameObject.GetComponent<Building>())
            {
                if (potential_building_target == building_target)
                {
                    enemy_building_in_range = false;
                    building_target = null;
                }
            }
        }
    }
}
