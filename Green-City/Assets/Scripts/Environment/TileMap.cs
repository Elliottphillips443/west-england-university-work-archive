using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public GameObject tile_prefab;
    public Transform start_transform;
    public Transform end_transform;
    public float tile_width = 1f;
    public float tile_height = 1f;

    public List<Vector3> tile_positions = new List<Vector3>();


        
    void Start ()
    {
        Vector2 fixed_start_position;
        Vector2 fixed_end_position;

        if(start_transform.position.z > end_transform.position.z)
        {
            fixed_start_position.y = end_transform.position.z;
            fixed_end_position.y = start_transform.position.z;
        }
        else
        {
            fixed_start_position.y = start_transform.position.z;
            fixed_end_position.y = end_transform.position.z;
        }

        if (start_transform.position.x > end_transform.position.x)
        {
            fixed_start_position.x = end_transform.position.x;
            fixed_end_position.x = start_transform.position.x;
        }
        else
        {
            fixed_start_position.x = start_transform.position.x;
            fixed_end_position.x = end_transform.position.x;
        }

        for (float h = fixed_start_position.y; h <= fixed_end_position.y; h += tile_height)
        {
            for (float w = fixed_start_position.x; w <= fixed_end_position.x; w += tile_width)
            {
                tile_positions.Add(new Vector3(w, 0f, h));
            }
        }

        int count = 0;
        foreach(Vector3 pos in tile_positions)
        {
            GameObject spawned = Instantiate(tile_prefab, pos, Quaternion.identity);
            if(count % 5 == 0)
            {
                //spawned.GetComponent<PlacableTile>().SpawnTower("Bin");
            }
            count++;
        }
	}
	
	void Update ()
    {
        
	}
}
