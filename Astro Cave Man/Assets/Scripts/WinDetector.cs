using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WinDetector : MonoBehaviour
{
    private Village[] villages;
    private PlayerControl[] players;
    private List<PlayerControl> ranks = new List<PlayerControl>();
    public Sprite winnerImage;
    public EndScreen end_screen;
    public EndPanel end_panel;

    public bool game_won = false;
    public int population_to_win = 25;
    public int players_alive = 4;
    public int global_pop = 0;
    public Text timerText;
    public float timer;
    
    private void Start ()
    {
        villages = FindObjectsOfType<Village>();
        players = FindObjectsOfType<PlayerControl>();
        game_won = false;
	}



    private void Update()
    {
        if (!game_won)
        {
            timer = timerText.GetComponent<TimeText>().timer;
            if(timer <= 0)
            {
                TimeoutWinner();
                game_won = true;
                return;
            }
            if (pollPopulationVictory())
                return;
            else if (pollLastManStandingVictory())
                return;
        }
        else
        {
            PlayerControl victorious_player = players[0];
            foreach (PlayerControl player in players)
            {
                if(player.has_won)
                {
                    victorious_player = player;
                    break;
                }
            }

            end_screen.game_over = true;
            end_panel.winnerNumber = victorious_player.playerNumber;
            end_panel.winPic = winnerImage;
            end_panel.enabled = true;
        }
	}

    private void TimeoutWinner()
    {
        int winningVillage = 0;
        for(int i = 0; i<villages.Length - 1;i++)
        {
            if(villages[i].population > villages[i+1].population)
            {
                winningVillage = i;
            }
        }
        foreach (PlayerControl player in players)
        {
            if (player.allegiance == villages[winningVillage].allegiance)
            {
                player.has_won = true;
                game_won = true;
                winnerImage = player.GetComponentInChildren<SpriteRenderer>().sprite;
            }
        }
    }

    private bool pollPopulationVictory()
    {
        global_pop = 0;
        
        foreach (Village village in villages)
        {
            global_pop += village.population;

            /*
            if (village.population >= population_to_win)
            {
                foreach (PlayerControl player in players)
                {
                    if (player.allegiance == village.allegiance)
                    {
                        player.has_won = true;
                        game_won = true;
                        return true;
                    }
                }
            }
            */
        }
        foreach(Village village in villages)
        {
            Debug.Log((float)(village.population / (global_pop + 1)));
            if (village.population / (global_pop * 0.5f) >= 0.5f)
            {
                foreach (PlayerControl player in players)
                {
                    if (player.allegiance == village.allegiance)
                    {
                        player.has_won = true;
                        game_won = true;
                        winnerImage = player.GetComponentInChildren<SpriteRenderer>().sprite;
                        return true;
                    }
                }
            }
        }
        foreach(Village village in villages)
        {
            Debug.Log((float)(village.population / (global_pop + 1)));
            if (village.population / (global_pop * 0.5f) >= 0.5f)
            {
                foreach (PlayerControl player in players)
                {
                    if (player.allegiance == village.allegiance)
                    {
                        player.has_won = true;
                        game_won = true;
                        winnerImage = player.GetComponentInChildren<SpriteRenderer>().sprite;
                        return true;
                    }
                }
            }
        }

        return false;
    }



    private bool pollLastManStandingVictory()
    {
        foreach(PlayerControl player in players)
        {
            if (ranks.Contains(player))
                continue;

            if (player.perma_dead)
            {
                players_alive--;
                ranks.Add(player);

                if(players_alive >= 3)
                {
                    foreach(PlayerControl p in players)
                    {
                        if(!p.perma_dead)
                        {
                            p.has_won = true;
                            ranks.Add(p);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}
