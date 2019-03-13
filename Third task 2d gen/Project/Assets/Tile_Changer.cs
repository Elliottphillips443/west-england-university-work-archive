using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Changer : MonoBehaviour
{

    public Sprite[] sprite;

    [SerializeField]
    int index;
    [SerializeField]
    string tileType;

    [SerializeField]
    bool seaUp;
    [SerializeField]
    bool seaDown;
    [SerializeField]
    bool seaLeft;
    [SerializeField]
    bool seaRight;

    [SerializeField]
    bool tmpSeaFwd;
    [SerializeField]
    bool tmpSeaBck;
    [SerializeField]
    bool tmpSeaLeft;
    [SerializeField]
    bool tmpSeaRight;

    [SerializeField]
    bool userChangedTile = false;
    [SerializeField]
    bool justStarted = true;
    [SerializeField]
    public bool mapRefresh = false;

    [SerializeField]
    GameObject tileFront;
    [SerializeField]
    GameObject tileBack;
    [SerializeField]
    GameObject tileLeft;
    [SerializeField]
    GameObject tileRight;

    public bool setTown;

    void Start()
    {

        GetComponent<SpriteRenderer>().sprite = sprite[index];

        if (index == 0)
        {
            tileType = "Sea";
        }

        if (index > 0)
        {
            tileType = "Land";
        }

        StartCoroutine(WaitSeconds(0.1f));
        ScanLocalTiles();
        IsThereASeaTileAround();

        //StartCoroutine(WaitSeconds(2));
            
        
    }

    IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        EdgeWorld();
    }

    void Update()
    {
        /*
        if (userChangedTile == true
                || justStarted == true)
        {
            MapLogic();
        }*/ // this breaks alot of things prob have to have  look as to why 
        if (seaUp && seaDown && seaLeft && seaRight) // sea up right left down
        {
            index = 0;
        }

        if(setTown == true)
        {
            index = 3;
        }

        MapLogic();
        

    }


    private void FixedUpdate()
    {


    }

    public string GetTileTypeString()
    {
        return tileType;
    }



    private void CheckTilesAround()
    {

        if (tileFront != null)
        {
            if (tileFront.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                seaUp = true;
            }
            else { seaUp = false; }
        }

        if (tileBack != null)
        {
            if (tileBack.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                seaDown = true;
            }
            else { seaDown = false; }
        }

        if (tileLeft != null)
        {
            if (tileLeft.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                seaLeft = true;
            }
            else { seaLeft = false; }

        }

        if (tileRight != null)
        {
            if (tileRight.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                seaRight = true;
            }
            else { seaRight = false; }

        }
    }

    private void TileTypeCalculation()
    {
        if (index == 1)
        {
            if (seaUp && !seaDown && !seaLeft && !seaRight) // sea up
            {
                GetComponent<SpriteRenderer>().sprite = sprite[2];
            }

            else if (seaUp && !seaDown && !seaLeft && seaRight) // sea up right
            {
                GetComponent<SpriteRenderer>().sprite = sprite[4];
            }

            else if (seaUp && !seaDown && seaLeft && !seaRight) // sea up left
            {
                GetComponent<SpriteRenderer>().sprite = sprite[3];
            }

            else if (!seaUp && seaDown && !seaLeft && !seaRight) // sea bck
            {
                GetComponent<SpriteRenderer>().sprite = sprite[5];
            }

            else if (!seaUp && seaDown && !seaLeft && seaRight) // sea bck right
            {
                GetComponent<SpriteRenderer>().sprite = sprite[7];
            }

            else if (!seaUp && seaDown && seaLeft && !seaRight) // sea bck left
            {
                GetComponent<SpriteRenderer>().sprite = sprite[6];
            }

            else if (!seaUp && !seaDown && seaLeft && !seaRight) // sea left
            {
                GetComponent<SpriteRenderer>().sprite = sprite[9];
            }

            else if (!seaUp && !seaDown && !seaLeft && seaRight) // sea right
            {
                GetComponent<SpriteRenderer>().sprite = sprite[8];
            }

            else if (!seaUp && !seaDown && seaLeft && seaRight) // sea right left
            {
                GetComponent<SpriteRenderer>().sprite = sprite[10];
            }

            else if (seaUp && seaDown && !seaLeft && !seaRight) // sea up down
            {
                GetComponent<SpriteRenderer>().sprite = sprite[11];
            }

            else if (seaUp && seaDown && seaLeft && !seaRight) // sea up down left
            {
                GetComponent<SpriteRenderer>().sprite = sprite[13];
            }

            else if (seaUp && seaDown && !seaLeft && seaRight) // sea up down right
            {
                GetComponent<SpriteRenderer>().sprite = sprite[12];
            }

            else if (seaUp && !seaDown && seaLeft && seaRight) // sea up right left
            {
                GetComponent<SpriteRenderer>().sprite = sprite[14];
            }

            else if (!seaUp && seaDown && seaLeft && seaRight) // sea  down right left
            {
                GetComponent<SpriteRenderer>().sprite = sprite[15];
            }

            else if (seaUp && seaDown && seaLeft && seaRight) // sea up right left down
            {
                index = 0;
            }

            else if (!seaUp && !seaDown && !seaLeft && !seaRight)// land
            {
                GetComponent<SpriteRenderer>().sprite = sprite[1];
                index = 1;

            }
        }

    }

    public void ScanLocalTiles()
    {
        //Debug.Log("ScanLocalTiles function called");
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.up);
        Vector3 bck = transform.TransformDirection(Vector3.down);
        Vector3 left = transform.TransformDirection(Vector3.left);
        Vector3 right = transform.TransformDirection(Vector3.right);



        if (Physics.Raycast(transform.position, fwd, out hit, 2))
        {
            tileFront = hit.transform.gameObject;
        }

        if (Physics.Raycast(transform.position, bck, out hit, 2))
        {
            tileBack = hit.transform.gameObject;
        }

        if (Physics.Raycast(transform.position, left, out hit, 2))
        {
            tileLeft = hit.transform.gameObject;
        }

        if (Physics.Raycast(transform.position, right, out hit, 2))
        {
            tileRight = hit.transform.gameObject;
        }

        CheckTilesAround();
        TileTypeCalculation();

        tmpSeaFwd = seaUp;
        tmpSeaBck = seaDown;
        tmpSeaLeft = seaLeft;
        tmpSeaRight = seaRight;
    }

    public void SetTypeTileInt(int changeTile)
    {

        index = changeTile;
        userChangedTile = true;
        MapRefresh(true);
        IndexSet();
    }

    public int GetTypeTileInt()
    {
        return index;
    }

    public void RandomTileMaker()
    {

        index = Random.Range(0, 2);

        if (Random.Range(0, 2) == 0)
        {

            index = 1;
        }


    }

    void IndexSet()
    {
        if (index == 0)
        {
            tileType = "Sea";
        }

        if (index == 1 || index == 2)
        {
            tileType = "Land";
        }

        if (index == 3)
        {
            tileType = "Town";
        }

        if (index == 4)
        {
            tileType = "Forest";
        }

        if (index == 6)
        {
            tileType = "Mine";
        }

        if (index == 5)
        {
            tileType = "Road";
        }

        if (index == 7)
        {
            tileType = "Farm";
        }
    }

    public bool IsThereASeaTileAround()
    {
        //Debug.Log("IsThereASeaTileAround");
        ScanLocalTiles();
        CheckTilesAround();

        int numberOfSeaTiles = 0;

        if (tileFront != null)
        {
            if (tileFront.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                numberOfSeaTiles++;
            }
        }

        if (tileBack != null)
        {
            if (tileBack.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                numberOfSeaTiles++;

            }
        }

        if (tileLeft != null)
        {
            if (tileLeft.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                numberOfSeaTiles++;

            }

        }

        if (tileRight != null)
        {
            if (tileRight.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                numberOfSeaTiles++;

            }

        }

        if (numberOfSeaTiles > 0)
        {
            return true;
        }
        else
        {
            //Debug.Log("no sea tiles found");
            return false;
        }
    }

    public bool IsntEdge()
    {
        //Debug.Log("IsThereASeaTileAround");
        ScanLocalTiles();
        CheckTilesAround();

        int numberOfEdge = 0;

        if (tileFront != null)
        {
            if (tileFront.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                numberOfEdge++;
            }
        }

        if (tileBack != null)
        {
            if (tileBack.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                numberOfEdge++;

            }
        }

        if (tileLeft != null)
        {
            if (tileLeft.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                numberOfEdge++;

            }

        }

        if (tileRight != null)
        {
            if (tileRight.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
            {
                numberOfEdge++;

            }

        }

        if (numberOfEdge > 0)
        {
            return true;
        }
        else
        {
            //Debug.Log("no sea tiles found");
            return false;
        }
    }
    public bool IsThereFreeSpaceAround()
    {
        //Debug.Log("IsThereFreeSpaceAround");
        ScanLocalTiles();
        CheckTilesAround();

        int numberOfFreeTiles = 0;

        if (tileFront != null)
        {
            if (tileFront.GetComponent<Tile_Changer>().GetTypeTileInt() == 1)
            {
                numberOfFreeTiles++;
            }
        }

         if (tileBack != null)
        {
            if (tileBack.GetComponent<Tile_Changer>().GetTypeTileInt() == 1)
            {
                numberOfFreeTiles++;

            }
        }

         if (tileLeft != null)
        {
            if (tileLeft.GetComponent<Tile_Changer>().GetTypeTileInt() == 1)
            {
                numberOfFreeTiles++;

            }

        }

         if (tileRight != null)
        {
            if (tileRight.GetComponent<Tile_Changer>().GetTypeTileInt() == 1)
            {
                numberOfFreeTiles++;

            }

        }

        Debug.Log(numberOfFreeTiles);
        if(numberOfFreeTiles == 4)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void MapRefresh(bool refresh)
    {
        //Debug.Log(" MapRefresh()");
        mapRefresh = refresh;
        //GetComponentInParent<TileMap>().RefreshChildren();

    }

    public bool GetMapRefresh()
    {
        return mapRefresh ;

    }


    public void MapLogic()
    {
        IndexSet();

        if (tileType == "Sea")
        {
            GetComponent<SpriteRenderer>().sprite = sprite[0];

        }

        if (tileType == "Land")
        {

            CheckTilesAround();
            if (userChangedTile)
            {
                ScanLocalTiles();
                userChangedTile = false;
            }
            //  CheckBeachDirection();

            if (tmpSeaFwd != seaUp || tmpSeaBck != seaDown || tmpSeaLeft != seaLeft
                || tmpSeaRight != seaRight)
            {
                // Debug.Log("1");
                //totalIndex = 1;

                //CheckBeachDirection();
                ScanLocalTiles();
                TileTypeCalculation();
            }

            //CheckBeachDirection();
            //TileTypeCalculation();


            tmpSeaFwd = seaUp;
            tmpSeaBck = seaDown;
            tmpSeaLeft = seaLeft;
            tmpSeaRight = seaRight;

        }


        if (tileType == "Town")
        {
            GetComponent<SpriteRenderer>().sprite = sprite[19];
        }

        if (tileType == "Mine")
        {
            GetComponent<SpriteRenderer>().sprite = sprite[16];
        }

        if (tileType == "Forest")
        {
            GetComponent<SpriteRenderer>().sprite = sprite[21];
        }

        if (tileType == "Farm")
        {
            GetComponent<SpriteRenderer>().sprite = sprite[20];
        }

        if (tileType == "Road")
        {
            GetComponent<SpriteRenderer>().sprite = sprite[17];
        }



        justStarted = false;
        mapRefresh = false;


    }

    void EdgeWorld()
    {
        if (tileBack == null || tileLeft == null || tileRight == null || tileFront == null)
        {
            index = 0;
        }
    }

    public void delete()
    {
        Destroy(this.gameObject);
    }
}
