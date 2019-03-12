using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    private enum TrashType
    {
        NONE,
        RECYCLING,
        GENERAL
    }


    private List<Transform> m_spawn_areas = new List<Transform>();
    public List<Sprite> rubbish_sprites;
    public List<Sprite> recyclable_sprites;
    public float m_spawn_interval = 5f;
    public float m_spawn_time_elapsed = 0f;
    public int m_spawn_amount = 1;
    public GameObject m_trash_prefab;
	public GameObject m_trash_parent;
	public int m_number_of_trash = 0;
    public int m_chance_for_recycling = 5; // out of 10, less than


	void Update ()
    {
        m_spawn_time_elapsed += Time.deltaTime;

        if(m_spawn_time_elapsed >= m_spawn_interval && m_spawn_areas.Count > 0)
        {
            m_spawn_time_elapsed = 0f;
            for(int i = 0; i < m_spawn_amount; i++)
            {
                int rand_trash_int = Random.Range(0, 10) + 1;
                TrashType trash_type = TrashType.GENERAL;

                if (rand_trash_int <= m_chance_for_recycling)
                    trash_type = TrashType.RECYCLING;

                SpawnTrash(trash_type);
            }
        }

		m_number_of_trash = m_trash_parent.transform.childCount;
	}

    public void Quickspawn()
    {
        if (m_spawn_areas.Count > 0)
        {
            m_spawn_time_elapsed = 0f;
            for (int i = 0; i < m_spawn_amount; i++)
            {
                int rand_trash_int = Random.Range(0, 10) + 1;
                TrashType trash_type = TrashType.GENERAL;

                if (rand_trash_int <= m_chance_for_recycling)
                    trash_type = TrashType.RECYCLING;

                SpawnTrash(trash_type);
            }
        }

        m_number_of_trash = m_trash_parent.transform.childCount;
    }


    public void UpdateAreaList(GameObject new_city_block)
    {
        GameObject area_parent = null;
        
        foreach(Transform child in new_city_block.transform)
        {
            if(child.tag == "TrashSpawnArea")
            {
                area_parent = child.gameObject;
                break;
            }
        }

        foreach(Transform area in area_parent.transform)
        {
            m_spawn_areas.Add(area);
        }
    }



    private void SpawnTrash(TrashType trash_type)
	{
		int random_area_index = Random.Range (0, m_spawn_areas.Count);
		Transform selected_area = m_spawn_areas [random_area_index];

		float width = selected_area.localScale.x;
		float depth = selected_area.localScale.z;

		float position_x = selected_area.position.x;
		float position_y = selected_area.position.y;
		float position_z = selected_area.position.z;

		Vector3 min = new Vector3 (position_x - (width / 2), position_y, position_z - (depth / 2));
		Vector3 max = new Vector3 (position_x + (width / 2), position_y, position_z + (depth / 2));

		float random_position_x = Random.Range (min.x, max.x);
		float random_position_z = Random.Range (min.z, max.z);

		Vector3 spawn_position = new Vector3 (random_position_x, 0.1f, random_position_z);

		GameObject trash = Instantiate (m_trash_prefab, spawn_position, m_trash_prefab.transform.rotation, m_trash_parent.transform);

        if (trash_type == TrashType.GENERAL)
        {
            trash.tag = "Rubbish";
            trash.GetComponent<SpriteRenderer>().sprite = rubbish_sprites[Random.Range(0, rubbish_sprites.Count)];
        }
        else
        {
            trash.tag = "Recyclable";
            trash.GetComponent<SpriteRenderer>().sprite = recyclable_sprites[Random.Range(0, recyclable_sprites.Count)];
        }
    }
}
