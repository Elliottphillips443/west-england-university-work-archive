using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour {

    private float speed = 5.0f;

    public GameObject prefabTransporter;

    string mouseType;
    GameObject tileSelected;

    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

        Click();

    }

    void Click()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {


            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    tileSelected = hit.transform.gameObject;
                    if (mouseType == "TileChanger")
                    {
                        tileSelected.GetComponent<Tile_Changer>().SetTypeTileInt(tileChanger());

                    }
                    else if (mouseType == "Road")
                    {
                        if(tileSelected.GetComponent<Tile_Changer>().GetTypeTileInt() == 1)
                        {
                            tileSelected.GetComponent<Tile_Changer>().SetTypeTileInt(5);
                        }
                    }
                    else if (mouseType == "Transporter")
                    { 
                        if (tileSelected.GetComponent<Tile_Changer>().GetTypeTileInt() == 5)
                        {
                            Instantiate(prefabTransporter, new Vector3(tileSelected.transform.position.z,
                                tileSelected.transform.position.x, tileSelected.transform.position.y + 1f),
                                    Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    int tileChanger()
    {
        if (tileSelected.GetComponent<Tile_Changer>().GetTypeTileInt() == 0)
        {
            return 1;
        }
        else

        {
            return 0;
        }
        

    }

    public void SetMouseSpawnRoad()
    {
        mouseType = "Road";
    }

    public void SetMouseSpawnTransporter()
    {
        mouseType = "Transporter";
    }

    public void SetMouseTileChanger()
    {
        mouseType = "TileChanger";
    }

}
