using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveDifficulty : MonoBehaviour
{
    //general stuff
    
    public bool difficulty_Scaledown = true; // applies to money, when over they undo difficulty gained
    public bool using_NPCS = false;
    public bool if_spawn_per_tile = true;
    public float difficulty = 0.0f;
    private float trash_spawn_rate = 0.0f;
    private float NPC_spawn_rate = 0.0f;
    private int city_level = 1;
    private float general_timer = 0.0f;
    private float tick_timer = 0.0f;
    public float spawn_mod = 1.0f;
    public float trash_density_cap_perTile = 100;
    private float Growth_timer = 0.0f;

    //time stuff
    public float time_difficulty_increment = 0.01f;
    //public float time_difficulty_interval = 1.0f;
    private int time_difficulty_exp_period = 120;
    public bool time_exponential_increase = true;

    //satisfaction stuff
    public float satisfaction = 0.5f;
    public float litter_density = 0.0f;
    private float satisfaction_weighting = 1.0f;
    

    //money stuff
    public static int money_current;    //to do stuff with this amount use AdaptiveDifficulty.money_current
    public int[] money_at_time = new int[6]{ 0, 0, 0, 0, 0, 0 };
    public int money_delta = 0;
    public int money_delta_threshold = 100; //Amount of surplus money earned to trigger more NPCs to spawn
    private float money_weighting = 1.0f;
    private float money_difficulty_threshold = 50;
    private float money_difficulty_gained = 0;
    
    //NPC ratio stuff
    private int[] numbers_of_bins;
    private float[] NPC_ratios; // A SMALL weighting on the number of NPCs of each type spawned. Only SUBTLEY affects actual spawn rates.
    
    //rush stuff
    private bool in_rush_hour = false;
    private float rush_hour_timer = 0.0f;
    private float rush_hour_multiplier = 5.0f;
    private float rush_hour_time_trigger = 30.0f;
    private float rush_hour_time_duration = 5.0f;

	// Use this for initialization
	void Start ()
    {
        //need to set start amount here
        money_current = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        general_timer += Time.deltaTime;
        tick_timer += Time.deltaTime;
        
        //Satisfaction = 1-(litterdensity) (absolute)
        city_level = GetComponent<CityGenerator>().m_stage;

        UpdateSatisfaction();
        //SatisfactionProportionalToLitter();

        if (tick_timer > 1.0f)
        {
            tick_timer = 0.0f;

            //NPC spawn rate += x * money delta
           // MoneyAffectsSpawRate();

            //Encourage variety (spawn NPCs that counter the player's speciality)
           // AdjustNPCRatios();

            //NPC spawn rate -= (x-satisfaction) (only if satisfaction is v.low)
           // SatisfactionAffectsSpawnRate();

            // ajusting the difficulty
            DifficultyByTime();
            DifficultyBySatisfaction();
            //DifficultyByMoney();
            //DifficultyByRush();

            ApplyDifficulty();

            


        }
        if (CheckForCityGrowth())
        {
            GetComponent<CityGenerator>().NewStage();
            GetComponent<TrashSpawner>().Quickspawn();
        }
        //RushHour();
	}

    void MoneyAffectsSpawRate()
    {
        for(int i = 0; i < (money_at_time.Length - 1); i++)
        {
            money_at_time[i] = money_at_time[i + 1];
        }
        money_at_time[money_at_time.Length - 1] = money_current;
        money_delta = money_current - money_at_time[0];
        if (money_delta >= money_delta_threshold)
        {
            NPC_spawn_rate += money_weighting;
        }
    }

    void AdjustNPCRatios()
    {
        if(numbers_of_bins.Length == NPC_ratios.Length)
        {
            int total_number_of_bins = 0;
            foreach(int number in numbers_of_bins)
            {
                total_number_of_bins += number;
            }

            for (int i = 0; i < numbers_of_bins.Length; i++)
            {
                NPC_ratios[i] = 1 - ((float)numbers_of_bins[i]/(float)total_number_of_bins);
            }
        }
        else
        {
            //No, this is wrong
        }
    }

    void SatisfactionProportionalToLitter()
    {
        satisfaction = 1.0f - litter_density;
    }

    void SatisfactionAffectsSpawnRate()
    {
        if(satisfaction < 0.2f)
        {
            NPC_spawn_rate -= satisfaction_weighting*(0.3f - satisfaction);
        }
    }

    void RushHour()
    {
        rush_hour_timer += Time.deltaTime;

        if (rush_hour_timer > rush_hour_time_trigger && !in_rush_hour)
        {
            rush_hour_timer = 0.0f;
            in_rush_hour = true;

            //Rush hour
            NPC_spawn_rate *= rush_hour_multiplier;
        }
        else if (rush_hour_timer > rush_hour_time_duration && in_rush_hour)
        {
            rush_hour_timer = 0.0f;
            in_rush_hour = false;

            //Exit rush hour
            NPC_spawn_rate /= rush_hour_multiplier;
        }
    }

    void DifficultyByTime()
    {
        if (time_exponential_increase)
        {
            //increase difficulty by increment each second, increasing each period of seconds
            difficulty += time_difficulty_increment * (int)(general_timer / time_difficulty_exp_period);
        }
        else
        {
            difficulty += time_difficulty_increment;
        }
        
    }
    void  DifficultyBySatisfaction()
    {
        difficulty += (satisfaction - 0.5f)*satisfaction_weighting;
    }
    void DifficultyByMoney()
    {
        if (money_current > (money_delta_threshold*city_level))
        {
            if (money_difficulty_gained<money_difficulty_threshold)
            {
                difficulty += money_weighting;
                money_difficulty_gained += money_weighting;
            }
        }else if (difficulty_Scaledown)
        {
            if (money_difficulty_gained > 0)
            {
                difficulty -= money_weighting;
                money_difficulty_gained -= money_weighting;
            }
        }
    }
    void DifficultyByRush()
    {
        //copied curtis rush code & altered for difficulty
        rush_hour_timer += Time.deltaTime;

        if (rush_hour_timer > rush_hour_time_trigger && !in_rush_hour)
        {
            rush_hour_timer = 0.0f;
            in_rush_hour = true;

            //Rush hour
            difficulty *= rush_hour_multiplier;
        }
        else if (rush_hour_timer > rush_hour_time_duration && in_rush_hour)
        {
            rush_hour_timer = 0.0f;
            in_rush_hour = false;

            //Exit rush hour
            difficulty /= rush_hour_multiplier;
        }
    }
    void ApplyDifficulty()
    {
        if(using_NPCS)
        {
            NPC_spawn_rate = difficulty * spawn_mod * NumberTiles(city_level);
            AdjustNPCRatios();
        }
        else
        {
            trash_spawn_rate = difficulty * spawn_mod * NumberTiles(city_level);
            GetComponent<TrashSpawner>().m_spawn_amount = (int)trash_spawn_rate +1;
        }
    }

    int NumberTiles(int i)
    {
        if(if_spawn_per_tile)
        {
            return (int)Mathf.Pow((1 + (2 * (i - 1))), 2);
        }else
        {
            return 1;
        }
        
    }
    void UpdateSatisfaction()
    {
        litter_density = (GetComponent<TrashSpawner>().m_number_of_trash / NumberTiles(city_level)) / trash_density_cap_perTile;
        if(litter_density > 1.0f)
        {
            litter_density = 1;
        }
        float satisfaction_target = 1 - litter_density;

        if(satisfaction <= (satisfaction_target - 0.01f))
        {
            satisfaction += 0.01f;
        }
        else if (satisfaction >= (satisfaction_target + 0.01f))
        {
            satisfaction -= 0.01f;
        }
    }

    bool CheckForCityGrowth()
    {
        if (general_timer > 20.0f)
        {
            if (satisfaction > 0.60f)
            {
                Growth_timer += Time.deltaTime;
            }
            else
            {
                Growth_timer = 0.0f;
            }
            if (Growth_timer > 10.0f)
            {
                Growth_timer = -20.0f;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
