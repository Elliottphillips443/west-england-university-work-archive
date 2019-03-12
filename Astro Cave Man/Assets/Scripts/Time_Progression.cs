using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Progression : MonoBehaviour
{
    public STAR_SIGN current_buff = STAR_SIGN.AQUARIUS;
    public STAR_SIGN next_buff = STAR_SIGN.PISCES;
    private float day_time_elapsed = 0f;
    public float day_length = 3f;
    public int day_count = 0;
    public int month_length = 5;
    public int month_count = 0;

    private void Update()
    {
        day_time_elapsed += Time.deltaTime;

        if (day_time_elapsed >= day_length)
        {
            day_count++;
            day_time_elapsed = 0f;

            if(day_count % month_length == 0)
            {
                month_count++;

                if(current_buff == STAR_SIGN.CAPRICORN)
                    current_buff = STAR_SIGN.AQUARIUS;
                else
                    current_buff++;

                if (next_buff == STAR_SIGN.CAPRICORN)
                    next_buff = STAR_SIGN.AQUARIUS;
                else
                    next_buff++;

            }
        }
    }

}
