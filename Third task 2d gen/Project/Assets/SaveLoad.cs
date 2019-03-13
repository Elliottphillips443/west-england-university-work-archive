using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour {

    public GameObject tileMap;

    string path;
    string jsonString;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public  void SaveFunction()
    {
        path = Application.streamingAssetsPath + "/TileMap.json";
        jsonString = File.ReadAllText(path);
        TileMapJson tempMap = JsonUtility.FromJson<TileMapJson>(jsonString);
        tempMap.TileX = tileMap.GetComponent<TileMap>().GetXsize();
        tempMap.TileY = tileMap.GetComponent<TileMap>().GetYsize();

        int number = tileMap.GetComponent<TileMap>().GetXsize() *
             tileMap.GetComponent<TileMap>().GetYsize();
        tempMap.TileTypes = new int[number];

        for (int i = 0; i < number; i++)
        {
            tempMap.TileTypes[i] = tileMap.GetComponent<TileMap>().InstanceList[i].
                    GetComponent<Tile_Changer>().GetTypeTileInt();
        }

        string newTempMap = JsonUtility.ToJson(tempMap);
        File.WriteAllText(path, newTempMap);

        Debug.Log(newTempMap);
        
    }
    

    public void LoadFunction()
    {
        path = Application.streamingAssetsPath + "/TileMap.json";
        jsonString = File.ReadAllText(path);
        TileMapJson tempMap = JsonUtility.FromJson<TileMapJson>(jsonString);
        tileMap.GetComponent<TileMap>().SetXSize(tempMap.TileX);
        tileMap.GetComponent<TileMap>().SetYSize(tempMap.TileY);

        tileMap.GetComponent<TileMap>().GenerateNewTileMap(true);

        int number = tileMap.GetComponent<TileMap>().GetXsize() * tileMap.GetComponent<TileMap>().GetYsize();

        for (int i = 0; i < number; i++)
        {
            tileMap.GetComponent<TileMap>().InstanceList[i].GetComponent<Tile_Changer>().SetTypeTileInt(tempMap.TileTypes[i]) ;
        }


        Debug.Log(tempMap.TileX);
        Debug.Log(tempMap.TileY);

        //string newTempMap = JsonUtility.ToJson(tempMap);

        Debug.Log("loadFunction called");
    }

}

[System.Serializable]
public class TileMapJson
{
    public int TileX;
    public int TileY;
    public int[] TileTypes;
}
