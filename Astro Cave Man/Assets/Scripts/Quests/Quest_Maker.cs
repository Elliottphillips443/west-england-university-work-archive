using System.Collections.Generic;
using System.Text;
using System;

using UnityEngine;

public class Quest_Maker : MonoBehaviour
{
    public enum QUEST_TYPE
    {
        NULL,
        COMBAT,
        CAPTURE_KILL,
        CAPTURE_CONVERT,
        POPULATION,
        HARVEST_FOOD,
        HARVEST_STONE
    }

    [Serializable]
    public struct PREFIX
    {
        public string description;
        public QUEST_TYPE type;
        public RESOURCE resource;
        public int win_reward;
        public int failure_loss;
        public int value_change_required;
        public int day_to_complete;
    }

    [Serializable]
    public struct QUEST
    {
        public string description;
        public QUEST_TYPE type;
        public RESOURCE resource;
        public int win_reward;
        public int failure_loss;
        public int value_change_required;
        public int day_to_complete;
        public bool quest_started;
    }

    public TextAsset prefix_file;
    public TextAsset suffix_file;
    //Format should be as follows: Quest Description :Quest Type:Resource Effected:Change in Resource if Completed:Change in Resource if Failed:Days to Complete
    public List<PREFIX> prefix = new List<PREFIX>();
    public List<string> suffix = new List<string>();
    public QUEST quest;
    public Village village;
    public Time_Progression time_progression;
    public bool quest_timer_started = false;
    public int quest_start_day = 0;
    public int quest_start_resource = 0;


    private void Start()
    {
        village = this.GetComponent<Village>();
        prefixInit();
        suffixInit();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            generateQuest();

        if (quest.type != QUEST_TYPE.NULL)
        {
            questTask();
        }
    }



    private void questTask()
    {
        if(!quest_timer_started)
        {
            quest_timer_started = true;
            quest_start_day = time_progression.day_count;
        }
        //Not working for some reason (need to check if the user has ran out of days
        if(time_progression.day_count - quest_start_day > quest.day_to_complete && quest_timer_started)
        {
            print(time_progression.day_count - quest_start_day + " : " + quest.day_to_complete);
            //Failure Conditions
            village.addResource(quest.resource, quest.failure_loss);
            quest_timer_started = false;
            questReset();
            
            print("WELL FUCK");
        }

        questing();
    }



    private void questing()
    {
        if(!quest.quest_started)
        {
            quest.quest_started = true;

            switch (quest.type)
            {
                case QUEST_TYPE.COMBAT:
                    quest_start_resource = village.kill_count;
                    break;

                case QUEST_TYPE.CAPTURE_KILL:
                    quest_start_resource = village.prisoner_kill_count;
                    break;

                case QUEST_TYPE.CAPTURE_CONVERT:
                    quest_start_resource = village.prisoner_converted_count;
                    break;

                case QUEST_TYPE.POPULATION:
                    quest_start_resource = village.population;
                    break;

                case QUEST_TYPE.HARVEST_FOOD:
                    quest_start_resource = village.food;
                    break;

                case QUEST_TYPE.HARVEST_STONE:
                    quest_start_resource = village.stone;
                    break;

                default:
                    break;
            }
        }

        int resource_change = 0;
        switch (quest.type)
        {
            case QUEST_TYPE.COMBAT:
                resource_change = village.kill_count - quest_start_resource;
                break;

            case QUEST_TYPE.CAPTURE_KILL:
                resource_change = village.prisoner_kill_count - quest_start_resource;
                break;

            case QUEST_TYPE.CAPTURE_CONVERT:
                resource_change = village.prisoner_converted_count - quest_start_resource;
                break;

            case QUEST_TYPE.POPULATION:
                resource_change = village.population - quest_start_resource;
                break;

            case QUEST_TYPE.HARVEST_FOOD:
                resource_change = village.food - quest_start_resource;
                break;

            case QUEST_TYPE.HARVEST_STONE:
                resource_change = village.stone - quest_start_resource;
                break;

            default:
                break;
        }

        if(resource_change >= quest.value_change_required)
        {
            village.addResource(quest.resource, quest.win_reward);
            questReset();
        }
    }
    


    private void questReset()
    {
        quest.description = "";
        quest.type = QUEST_TYPE.NULL;
        quest.resource = RESOURCE.NULL;
        quest.win_reward = 0;
        quest.failure_loss = 0;
        quest.day_to_complete = 0;
        quest.quest_started = false;
    }



    private void generateQuest()
    {
        int prefix_index = UnityEngine.Random.Range(0, prefix.Count);
        int suffix_index = UnityEngine.Random.Range(0, suffix.Count);

        quest.type = prefix[prefix_index].type;
        quest.description = prefix[prefix_index].description + suffix[suffix_index];
        quest.resource = prefix[prefix_index].resource;
        quest.win_reward = prefix[prefix_index].win_reward;
        quest.failure_loss = prefix[prefix_index].failure_loss;
        quest.value_change_required = prefix[prefix_index].value_change_required;
        quest.day_to_complete = prefix[prefix_index].day_to_complete;
    }



    private void prefixInit()
    {
        List<string> pre_lines = getLines(prefix_file);
        List<string> split_lines = new List<string>();

       foreach (string line in pre_lines)
        {
            List<string> split_line = split(line, ':');
            int i = 1;
            PREFIX temp = new PREFIX();

            foreach (string new_line in split_line)
            {
                switch (i)
                {
                    case 1:
                        temp.description = new_line;
                        break;

                    case 2:
                        temp.type = (QUEST_TYPE)Enum.Parse(typeof(QUEST_TYPE), new_line.ToUpper());
                        break;

                    case 3:
                        temp.resource = (RESOURCE)Enum.Parse(typeof(RESOURCE), new_line.ToUpper());
                        break;

                    case 4:
                        temp.win_reward = int.Parse(new_line);
                        break;

                    case 5:
                        temp.failure_loss = int.Parse(new_line);
                        break;

                    case 6:
                        temp.value_change_required = int.Parse(new_line);
                        break;

                    case 7:
                        temp.day_to_complete = int.Parse(new_line);
                        break;

                    default:
                        break;
                }
                i++;
            }

            prefix.Add(temp);
        }
    }



    private void suffixInit()
    {
        List<string> suf_lines = getLines(suffix_file);
        List<string> split_lines = new List<string>();

        foreach (string line in suf_lines)
        {
            List<string> split_line = split(line, ':');
            bool toggle = false;

            foreach (string new_line in split_line)
            {
                if (!toggle)
                {
                    toggle = true;
                }
                else
                {
                    suffix.Add(new_line);
                    toggle = false;
                }
            }
        }
    }



    private List<string> split(string line, char split_letter)
    {
        StringBuilder new_line = new StringBuilder();
        List<string> new_lines = new List<string>();

        foreach(char letter in line)
        {
            if(letter == split_letter)
            {
                new_lines.Add(new_line.ToString());
                new_line = new StringBuilder();
                continue;
            }

            new_line.Append(letter);
        }

        if(new_line.Length > 0)
            new_lines.Add(new_line.ToString());

        return new_lines;
    }



    private List<string> getLines(TextAsset txt)
    {
        string text = txt.text;
        StringBuilder new_line = new StringBuilder();
        List<string> line_breaks = new List<string>();

        foreach(char letter in text)
        {
            if(letter == '\n' || letter == '\r')
            {
                continue;
            }
            if(letter == ';')
            {
                line_breaks.Add(new_line.ToString());
                new_line = new StringBuilder();
                continue;
            }

            new_line.Append(letter);
        }

        return line_breaks;
    }
}



