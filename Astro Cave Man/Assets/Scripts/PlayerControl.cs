
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public STAR_SIGN allegiance = STAR_SIGN.NULL;
	public List<AI> selectedUnits = new List<AI>();
    public Village myVillage;
    public string temp_ai_tag = "NPC";

    public bool has_won = false;
    public bool is_currently_dead = false;
    public bool perma_dead = false;
    private bool buttonPressedUse = false;
    private bool buttonPressedAttackBasic = false;
    private float attack_time;

    public float max_cast_distance = 100f;
    public float radius = 0.1f;
    public float attack_radius = 5f;
    public float speed = 10f;
    public float rotationSpeed = 10f;
	public int playerNumber = 0;
    public GameObject selectionSphere;
    public GameObject attackSphere;
    //health stuff
    private float healthTick = 0f;
    public float maxHealth = 100f;
    public float health = 100f;
    //public Text healthText;

    public int damage = 10;
    public float attack_speed = 3f;

    void Start ()
    {
        attack_time = attack_speed;
        myVillage = transform.parent.transform.parent.GetComponent<Village>();
        if(PlayerInformation.list_of_players.Count > 0)
        {
            playerNumber = PlayerInformation.list_of_players[PlayerInformation.list_of_players.Count - 1].playerNumber;
            Debug.Log("Player number " + playerNumber + " in control");
            allegiance = PlayerInformation.list_of_players[PlayerInformation.list_of_players.Count - 1].sign;
            PlayerInformation.list_of_players.RemoveAt(PlayerInformation.list_of_players.Count - 1);

            
        }
        switch (allegiance)
        {
            case STAR_SIGN.AQUARIUS:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aquarius");
                break;
            case STAR_SIGN.ARIES:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Aries");
                break;
            case STAR_SIGN.CANCER:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Cancer");
                break;
            case STAR_SIGN.CAPRICORN:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Capricorn");
                break;
            case STAR_SIGN.GEMINI:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Gemini");
                break;
            case STAR_SIGN.LEO:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Leo");
                break;
            case STAR_SIGN.LIBRA:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Libra");
                break;
            case STAR_SIGN.PISCES:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pisces");
                break;
            case STAR_SIGN.SAGITTARIUS:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sagitarius");
                break;
            case STAR_SIGN.SCORPIO:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Scorpio");
                break;
            case STAR_SIGN.TAURUS:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Taurus");
                break;
            case STAR_SIGN.VIRGO:
                GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Virgo");
                break;
        }
    }
	


	void Update ()
    {
        attack_time += Time.deltaTime;
        healthTick += Time.deltaTime;
        Movement();
        UnitSelection();
        //healthText.text = "HP: " + health.ToString() + "/" + maxHealth.ToString();
        //NewUnitSelection();
       // NewUnitRemoval();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0 && !is_currently_dead)
        {
            Die();
        }
    }

    public void Die()
    {
        is_currently_dead = true;
    }

    private void Movement()
    {
        if (playerNumber != 0)
        {
            Vector3 direction = Vector3.zero;
            direction.x = Input.GetAxis("Horizontal Joystick " + playerNumber);
            direction.z = Input.GetAxis("Vertical Joystick " + playerNumber);

            float moveHorizontal = Input.GetAxis("Horizontal Joystick " + playerNumber) * speed * Time.deltaTime;
            float moveVertical = Input.GetAxis("Vertical Joystick " + playerNumber) * speed * Time.deltaTime;

            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            this.transform.position += new Vector3(moveHorizontal, 0.0f, moveVertical);
            selectionSphere.transform.position += new Vector3(moveHorizontal, 0.0f, moveVertical);
            selectionSphere.transform.position = new Vector3(selectionSphere.transform.position.x, this.transform.position.y, selectionSphere.transform.position.z);


            direction = Vector3.zero;
            direction.x = Input.GetAxis("Right Stick H " + playerNumber);
            direction.z = Input.GetAxis("Right Stick V " + playerNumber);

            moveHorizontal = Input.GetAxis("Right Stick H " + playerNumber) * speed * Time.deltaTime;
            moveVertical = Input.GetAxis("Right Stick V " + playerNumber) * speed * Time.deltaTime;

            if(direction != Vector3.zero)
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            selectionSphere.transform.position += new Vector3(2*moveHorizontal, 0.0f, 2*moveVertical);

            if(Vector3.Distance(this.transform.position, selectionSphere.transform.position) > 8.0f)
            {
                Vector3 correction_vect = this.transform.position - selectionSphere.transform.position;
                correction_vect *= Time.deltaTime * 1.5f;
                selectionSphere.transform.position += correction_vect;
            }
        }
    } 



    private void UnitSelection()
    {
        if (playerNumber != 0)
        {
            if (Input.GetButton("Attack " + playerNumber))
            {
                if (attack_time >= attack_speed)
                {
                    attackSphere.GetComponent<Renderer>().enabled = true;
                    attack_time = 0;
                    //Attack others
                    Collider[] colliders;
                    colliders = Physics.OverlapSphere(selectionSphere.transform.position, 3f, 1 << LayerMask.NameToLayer("Villagers"));
                    if (colliders.Length > 0)
                    {
                        for (int i = 0; i < colliders.Length; i++)
                        {
                            //Only check for AI script if the object has the AI tag
                            if (colliders[i].gameObject.CompareTag(temp_ai_tag) && colliders[i].gameObject.GetComponent<AI>().allegiance != allegiance)
                            {
                                colliders[i].gameObject.GetComponent<Combat>().Attack();
                            }
                        }
                    }
                    else
                    {
                        attackSphere.GetComponent<Renderer>().enabled = false;
                    }
                }
            }
            else
            {
                attackSphere.GetComponent<Renderer>().enabled = false;
            }

            if (Input.GetButton("Selector " + playerNumber))
            {
                selectionSphere.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.2f);
                selectedUnits.Clear();
                Collider[] colliders;
                colliders = Physics.OverlapSphere(selectionSphere.transform.position, 3f, 1 << LayerMask.NameToLayer("Villagers"));
                if (colliders.Length > 0)
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        //Only check for AI script if the object has the AI tag
                        if (colliders[i].gameObject.CompareTag(temp_ai_tag))
                        {
                            AI hit_ai;
                            //Has an AI script attached, is of the same allegiance, and has not already been selected
                            hit_ai = colliders[i].gameObject.GetComponent<AI>();
                            if (hit_ai.allegiance == this.allegiance && !selectedUnits.Contains(hit_ai))
                            {
                                Debug.Log("adding ai");
                                selectedUnits.Add(hit_ai);
                            }
                        }
                    }
                }
            }
            else if (Input.GetButton("Cancel " + playerNumber)) //Remove all selected AI
            {
                selectedUnits.Clear();
                selectionSphere.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.2f);
            }
            else if (Input.GetButton("Assign " + playerNumber))
            {
                selectionSphere.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 0.2f);
                
                foreach(AI unit in selectedUnits)
                {
                    unit.gameObject.GetComponent<Movement>().GiveTarget(selectionSphere.transform.position);
                }
            }
            else
            {
                if (selectionSphere.GetComponent<Renderer>().material.color != (new Color(1, 1, 1, 0.2f)))
                {
                    selectionSphere.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.2f);
                }
            }

            if (Input.GetButton("LB" + playerNumber))
            {
                if (gameObject.GetComponent<Camera_Controller>().cam.orthographicSize < 20)
                {
                    gameObject.GetComponent<Camera_Controller>().cam.orthographicSize++;
                }
            }
            else if (Input.GetButton("RB" + playerNumber))
            {
                Debug.Log("zooming");
                if (gameObject.GetComponent<Camera_Controller>().cam.orthographicSize > 8)
                {
                    gameObject.GetComponent<Camera_Controller>().cam.orthographicSize--;
                }
            }
        }
    }



    private void NewUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit_info;

            if (Physics.Raycast(ray, out hit_info, max_cast_distance))
            {
                if (!hit_info.collider.CompareTag(temp_ai_tag))
                    return;

                AI ai;
                if (!(ai = hit_info.collider.GetComponent<AI>()) ||
                    ai.allegiance != this.allegiance ||
                    selectedUnits.Contains(ai))
                    return;

                selectedUnits.Add(ai);
            }
        }
    }



    private void NewUnitRemoval()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit_info;

            if (Physics.Raycast(ray, out hit_info, max_cast_distance))
            {
                if (!hit_info.collider.CompareTag(temp_ai_tag))
                    return;

                AI ai;
                if (!(ai = hit_info.collider.GetComponent<AI>()) ||
                    ai.allegiance != this.allegiance ||
                    !selectedUnits.Contains(ai))
                    return;

                selectedUnits.Remove(ai);
            }
        }
    }

    private void HealthRegen()
    {
        if (healthTick >= 1f)
        {
            if (health < 100)
            {
                health++;
                healthTick = 0f;
            }
        }
    }
    //Now using a sphere cast
    //void OnTriggerEnter(Collider other)
    //{
    //	if (other.CompareTag("Friendly"))
    //{
    //		selectedUnits.Add(other.gameObject);
    //	}
    //}


    //Pete has made a system for taking damage which doesn't take x amount of damage but a potential min and max damage based on params (so thinking this is obsolete
    //void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        if (time > attackSpeed)
    //        {
    //            if (buttonPressedAttackBasic)
    //            {
    //                other.GetComponent<PlayerControl>().health -= damage;
    //                time = 0f;
    //            }

    //        }
    //    }
    //}
}







/* Stuff not being used
 * 
        //if (Input.GetAxis("Use") != 0 )
        //{
        //    if (buttonPressedUse == false)
        //    {
        //        Debug.Log("Use");
        //        buttonPressedUse = true;
        //    }
        //}

        //if (Input.GetAxis("Use") == 0)
        //    buttonPressedUse = false;

 */
