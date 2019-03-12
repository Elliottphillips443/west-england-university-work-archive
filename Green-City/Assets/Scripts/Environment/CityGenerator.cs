using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    //Set these in the inspector
    private TrashSpawner m_trash_spawner;
    public List<GameObject> m_city_blocks = new List<GameObject>();
    public List<GameObject> m_placed_city_blocks = new List<GameObject>();
    public int m_stage = 1;
    public int m_menu_stage = 5;
    public bool m_on_menu = true;
    private int m_number_of_city_blocks = 0;
    private int m_last_city_block_id = 0;


    private void Start ()
    {
        m_trash_spawner = this.GetComponent<TrashSpawner>();
        //Sets up initial variables and spawns initial city block (doesn't work if the m_city_blocks have not been set in the inspector!)
        m_number_of_city_blocks = m_city_blocks.Count;

        int random_city_block_id = Random.Range(0, m_number_of_city_blocks);
        m_last_city_block_id = random_city_block_id;

        GameObject city_block = Instantiate(m_city_blocks[random_city_block_id], Vector3.zero, Quaternion.identity, this.gameObject.transform);
        m_trash_spawner.UpdateAreaList(city_block);
        m_placed_city_blocks.Add(city_block);
        
        if(m_on_menu)
            LoadMenuStage(m_menu_stage);
    }
	


	private void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            NextStage(m_stage);
            m_stage++;
        }
	}



    public void NewStage()
    {
        NextStage(m_stage);
        m_stage++;
    }



    private void LoadMenuStage(int end_stage)
    {
        for(int i = m_stage; i <= end_stage; i++)
        {
            NextStage(i);
        }

        m_stage = m_menu_stage;
    }


    private void NextStage(int stage)
    {
        int number_new_blocks = stage * 8;
        int number_new_blocks_wide = (stage * 2) + 1;
        int current_row = 0;
        int current_column = 0;

        float change_in_position = 10f;

        Vector3 start_position = new Vector3(stage * 10f, 0f, stage * 10f);
        Vector3 end_position = new Vector3(-stage * 10f, 0f, -stage * 10f);
        Vector3 current_position = start_position;

        float pos_x = 0f;
        float pos_y = 0f;
        float pos_z = 0f;

        for (int i = 0; i < number_new_blocks; i++)
        {
            bool skip_middle = false;

            //Once end of row has been reached
            if (current_position.x <= end_position.x)
            {
                current_column = 0;
                current_row++;
                current_position.x = start_position.x;
            }


            //If this is not the top or bottom row don't fill between blocks
            if(current_row != 0 && current_row != number_new_blocks_wide - 1)
                skip_middle = true;

            //Setting the new positions based on math magic!
            if(skip_middle)
            {
                if(current_column == 0)
                {
                    pos_x = start_position.x;
                    pos_y = start_position.y;
                    pos_z = start_position.z - (current_row * change_in_position);
                }
                else
                {
                    pos_x = end_position.x;
                    pos_y = start_position.y;
                    pos_z = start_position.z - (current_row * change_in_position);
                }
            }
            else
            {
                pos_x = start_position.x - (current_column * change_in_position);
                pos_y = start_position.y;
                pos_z = start_position.z - (current_row * change_in_position);
            }

            current_column++;
            current_position = new Vector3(pos_x, pos_y, pos_z);

            //Picks a random city block and instantiates it
            int random_city_block_id = Random.Range(0, m_number_of_city_blocks);

            while (random_city_block_id == m_last_city_block_id)
                random_city_block_id = Random.Range(0, m_number_of_city_blocks);

            GameObject city_block = Instantiate(m_city_blocks[random_city_block_id], current_position, Quaternion.identity, this.gameObject.transform);
            m_trash_spawner.UpdateAreaList(city_block);
            m_placed_city_blocks.Add(city_block);

            m_last_city_block_id = random_city_block_id;
        }
    }
}
