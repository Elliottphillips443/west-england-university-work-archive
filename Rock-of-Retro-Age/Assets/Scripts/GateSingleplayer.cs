using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GateSingleplayer : MonoBehaviour {

    public bool isAlive = true;
    public float hp = 100;
    public float damageTaken = 0;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;

    }
	
	// Update is called once per frame
	void Update () {
        //checking if its alive
        if (this.gameObject)
        {
            if (this.isAlive == false)
            {
                this.gameObject.SetActive(false);
            }
        }
	}

    //taking damage from player based on momentun
    void OnTriggerEnter2D(Collider2D myCollider)
    {
        if (myCollider.gameObject.tag == "Player" )
        {
            // Vector2 hitRef = 
            //     new Vector2((this.gameObject.transform.position.x - myCollider.attachedRigidbody.position.x), 
            //     (this.gameObject.transform.position.y - myCollider.attachedRigidbody.position.y));
            // hitRef.Normalize();

            

            float damage = (myCollider.attachedRigidbody.mass * myCollider.attachedRigidbody.velocity.magnitude)
                * Vector2.Dot(myCollider.attachedRigidbody.velocity.normalized, new Vector2((this.gameObject.transform.position.x - myCollider.attachedRigidbody.position.x),
                (this.gameObject.transform.position.y - myCollider.attachedRigidbody.position.y)));
            
            
            damageTaken = damageTaken + damage;
            hp = hp - damage;

           
            
            if (hp <= 0)
            {
                isAlive = false;
                //run death thing

                //declare win
                //SceneManager.LoadScene(1);
            }
			audioSource.Play();
        }
    }

}
