using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
public class Mouse_Input : MonoBehaviour
{

    //public GameObject unit;
    

    public GameObject selectedPlayerUnit;
    public List<GameObject> selectedUnits = new List<GameObject>();

    public GameObject selectedEnemyUnit;

    // the decision tree that qill check if the order is good
    public GameObject decisionTree;

    private RaycastHit hit;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl)) // on mouse left click + left control hold 
        {
            Debug.Log("mouse button and ctrl pressed");


            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) // sent a ray cast
            {
                if (hit.transform.tag == "PlayerUnit") // if the object the ray casts hits is taged with player
                {
                    selectedPlayerUnit = hit.transform.gameObject; // add unit to the selected unit
                    selectedPlayerUnit.gameObject.transform.Find("Marker").gameObject.SetActive(true); // enabled marker

                    selectedUnits.Add(selectedPlayerUnit); // add selected unit to the list of selected units
                    Debug.Log("Number of units in the list " + selectedUnits.Count);
                }
            }
            
        }

        


        if (Input.GetMouseButtonDown(0 ) && !Input.GetKey(KeyCode.LeftControl)) // on mouse left click
        {
            Debug.Log("mouse clicked");

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) // sent a ray cast
            {
                if (hit.transform.tag == "PlayerUnit") // if the object the ray casts hits is taged with player
                {
                    for (int i = 0; i < selectedUnits.Count; i++) // loop thro all of the selected units
                    {
                        selectedUnits[i].gameObject.transform.Find("Marker").gameObject.SetActive(false);
                    }
                    selectedUnits.Clear();

                    selectedPlayerUnit = hit.transform.gameObject; // add unit to the selected unit
                    selectedPlayerUnit.gameObject.transform.Find("Marker").gameObject.SetActive(true); // enabled marker

                    Debug.Log(selectedPlayerUnit + " selected");

                    selectedUnits.Add(selectedPlayerUnit); // add selected unit to the list of selected units

                    selectedPlayerUnit = null; // remove selected unit so another can be added

                }
                if (hit.transform.tag == "Floor") // if the object the ray cast hits is tagged with floor
                {
                    if (selectedUnits.Count > 0)
                    {
                        for (int i = 0; i < selectedUnits.Count; i++) // loop thro all of the selected units
                        {
                            selectedUnits[i].gameObject.transform.Find("Marker").gameObject.SetActive(false); // disable selected unit marker
                        }

                        selectedUnits.Clear(); // clear list
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {

            if (selectedUnits != null)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        selectedEnemyUnit = hit.transform.gameObject;

                        for (int i = 0; i < selectedUnits.Count; i++)
                        {
                            if (decisionTree.GetComponent<Decision_Tree>().ProcessAttackOrder(selectedUnits[i].GetComponent<Unit_Movement>().attack, 
                                selectedEnemyUnit.GetComponent<Enemy>().amour))
                            {

                                selectedUnits[i].GetComponent<Unit_Movement>().SetUnitGoal(hit.point);

                                Debug.Log(selectedUnits[i].name + " is attacking " + selectedEnemyUnit.name);
                            }
                        }


                    }
                }
                if (hit.transform.tag == "Floor")
                {
                    Debug.Log("Hit Floor");

                    for (int i = 0; i < selectedUnits.Count; i++)
                    {

                        if (decisionTree.GetComponent<Decision_Tree>().ProcessMoveOrder(selectedUnits[i].GetComponent<Unit_Movement>().fatigue,
                            selectedUnits[i].GetComponent<Unit_Movement>().disapline))
                        {
                            {
                                selectedUnits[i].GetComponent<Unit_Movement>().SetUnitGoal(hit.point);

                                Debug.Log(i + " Moving to " + hit.point);

                            }
                        }

                    }
                }
            }
        }


    }
}


    */