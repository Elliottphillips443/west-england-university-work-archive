using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TileMap : MonoBehaviour
{


    public GameObject prefab;
    public List<int> importantTilesListIndex;

    public int xSize;
    public int ySize;
    int numberOfTowns;

    public GameObject pathFind;

    public List<GameObject> InstanceList;

    // Use this for initialization
    void Start()
    {
        ySize = 20;
        xSize = 20;
        numberOfTowns = 10;
        GenerateNewTileMap(false);

        for (int i = 0; i < InstanceList.Count; i++)
        {
            InstanceList[i].GetComponent<Tile_Changer>().ScanLocalTiles();
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (xSize < 0)
            xSize = 0;

        if (ySize < 0)
            ySize = 0;


        if (xSize >= 50)
            xSize = 50;

        if (ySize >= 50)
            ySize = 50;


        RefreshChildren();

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveTileMap();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateNewTileMap(false);
        }
    }

    public void GenerateNewTileMap(bool LoadFromFile)
    {
        RemoveTileMap();

        for (int x = 0; x < xSize; x++)
        {

            for (int y = 0; y < ySize; y++)
            {
                GameObject currentTile = Instantiate(prefab, new Vector2(x * 2.0F, y * 2.0F), Quaternion.identity);
                if (LoadFromFile == false)
                {
                    currentTile.GetComponent<Tile_Changer>().RandomTileMaker();
                    //Debug.Log("gen ordered");
                }
                InstanceList.Add(currentTile);

            }

        }
        GenerateOtherTiles("town", (xSize + ySize) / 10);
        GenerateOtherTiles("tree", (xSize + ySize) / 10);
        GenerateOtherTiles("mine", (xSize + ySize) / 10);
        GenerateOtherTiles("farm", (xSize + ySize) / 10);

        for (int i = 0; i < importantTilesListIndex.Count; i++)
        {
            int tempInt = Random.Range(0, importantTilesListIndex.Count);

            for (int j = 0; j < importantTilesListIndex.Count; j++)
            {
                int tempInt2 = Random.Range(0, importantTilesListIndex.Count);

                pathFind.GetComponent<PathFinding>().PathFindingCustom(
                    importantTilesListIndex[tempInt], importantTilesListIndex[tempInt2]);
                importantTilesListIndex.RemoveAt(tempInt2);
                importantTilesListIndex.RemoveAt(tempInt);
            }
        }
        for (int i = 0; i < pathFind.GetComponent<PathFinding>().moveList.Count; i++)
        {
            if( InstanceList[pathFind.GetComponent<PathFinding>().moveList[i]].GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                InstanceList[pathFind.GetComponent<PathFinding>().moveList[i]].GetComponent<Tile_Changer>().SetTypeTileInt(1);
            }
        }
    }

    public void GenerateOtherTiles(string tileType,int numberOfThem)
    {
        int maxCount = 1000;
        int tempIndex = 1;
       
        if (tileType == "town")
        {
            tempIndex = 3;
        }

        if (tileType == "tree")
        {
            tempIndex = 4;
        }

        if (tileType == "mine")
        {
            tempIndex = 6;
        }

        if (tileType == "farm")
        {
            tempIndex = 7;
        }


        for (int j = 0; j < numberOfThem;)
        {

            int tempInt = Random.Range(0, InstanceList.Count);

            if (InstanceList[tempInt].GetComponent<Tile_Changer>().GetTypeTileInt() == 1 
                  && InstanceList[tempInt].GetComponent<Tile_Changer>().IsThereASeaTileAround() == false 
                     && InstanceList[tempInt].GetComponent<Tile_Changer>().IsThereFreeSpaceAround() == true
                        && InstanceList[tempInt].GetComponent<Tile_Changer>().IsntEdge() == false)
            {
                //Debug.Log("town");
                //Debug.Log(InstanceList[tempInt].GetComponent<Tile_Changer>().GetTypeTileInt());
                InstanceList[tempInt].GetComponent<Tile_Changer>().SetTypeTileInt(tempIndex);
                importantTilesListIndex.Add(tempInt);
               // Debug.Log(j);
                j++;
            }
            maxCount--;

            if(maxCount< 0)
            {
                break;
            }
        }
    }

    public void RefreshChildren()
    {
        for (int i = 0; i < InstanceList.Count; i++)
        {
            
            if (InstanceList[i].GetComponent<Tile_Changer>().GetMapRefresh() == true)
            {
              //  Debug.Log(" RefreshChildren()");
                for (int J = 0; J < InstanceList.Count; J++)
                {
                    InstanceList[J].GetComponent<Tile_Changer>().MapLogic();
                    InstanceList[J].GetComponent<Tile_Changer>().MapRefresh(false);
                }
            }
        }
    }

   public void RemoveTileMap()
    {
        int count = InstanceList.Count;

        for (int i = 0; i < count; i++)
        {

            Destroy(InstanceList[0]);
            InstanceList.RemoveAt(0);
        }
    }

   public void AddYSize()
    {
        ySize++;
    }

    public void TakeYSize()
    {
        ySize--;
    }

    public void AddXSize()
    {
        xSize++;
    }

    public void TakeXSize()
    {
        xSize--;
    }

    public int GetXsize()
    {
        return xSize;
    }

    public int GetYsize()
    {
        return ySize;
    }

    public void SetYSize(int number)
    {
        ySize = number;
    }

    public void SetXSize(int number)
    {
        xSize = number;
    }
}
