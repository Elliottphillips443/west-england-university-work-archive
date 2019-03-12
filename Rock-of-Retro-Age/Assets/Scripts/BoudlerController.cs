using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoudlerController : MonoBehaviour
{
    //The players physics body
    public Rigidbody2D boulder;

    //Speed and Direction variables 
    public float terminal_velocity = 10.0f;
    public float acceleration = 800.0f;
    public Vector2 direction = Vector2.zero;
    public float thrust = 5.0f;
    public bool grounded = false;
    static public float max_velocity = 0.0f;
   // public bool canTeleport = true;
	public float initialMass = 5.0f;
    public float velocityMag = 0.0f;
    public int timesTeleported = 0;

	//Health etc
	public float health = 1.0f;

	private Vector2 groundNormal = new Vector2(0,0);

    // Use this for initialization
    private void Start ()
    {
        boulder = this.GetComponent<Rigidbody2D>();
		boulder.mass = initialMass;
    }

    // Update is called once per frame
    private void Update ()
    {
        direction = boulder.velocity;
        inputController();
	updateMass ();

        if(boulder.velocity.magnitude > max_velocity)
        {
            max_velocity = boulder.velocity.magnitude;
        }

        //tracker for seeing vel
        velocityMag = boulder.velocity.magnitude;

    }

    /*private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Surface")
            grounded = true;
    }*/

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Surface")
            grounded = false;       
    }

    private void OnCollisionStay2D(Collision2D col)
	{
        if (col.gameObject.tag == "Surface")
        {
            grounded = true;
        }
        groundNormal.x = 0;
		groundNormal.y = 0;
		int count = 0;
		foreach (ContactPoint2D contact in col.contacts)
		{
			groundNormal += contact.normal;
			count++;
		}
		groundNormal /= count;
	}

    private void inputController()
    {
       // reachedTerminalVelocity();

        if (Input.GetAxis("Horizontal") < 0)
            direction += new Vector2(-acceleration * Time.deltaTime, 0.0f);
        
        if(Input.GetAxis("Horizontal") > 0)
            direction += new Vector2(acceleration * Time.deltaTime, 0.0f);
        //Space or A button (xbox)
        if (Input.GetAxis("Fire1") > 0 && grounded)
        {
            boulder.AddForce(groundNormal * thrust * Time.deltaTime * health, ForceMode2D.Impulse);
        }

        if (Input.GetAxis("Vertical") > 0 && grounded)
        {
            boulder.AddForce(groundNormal * thrust * Time.deltaTime * health, ForceMode2D.Impulse);
        }

        //print(direction.ToString());
        boulder.AddForce(direction);

       

    }

    private void reachedTerminalVelocity()
    {
         float velocity_x = direction.x;
         float velocity_y = direction.y;

         if (velocity_x >= terminal_velocity)
             velocity_x = terminal_velocity;
         else if (velocity_x <= terminal_velocity * -1) //Making Terminal velocity a negative to check opposite direction
             velocity_x = terminal_velocity * -1;

         if (velocity_y >= terminal_velocity)
             velocity_y = terminal_velocity;
         else if (velocity_y <= terminal_velocity * -1) //Making Terminal velocity a negative to check opposite direction
             velocity_y = terminal_velocity * -1;

         direction = new Vector2(velocity_x, velocity_y);

        //trying a new fix for terminal velocity (jon)
        /*if(boulder.velocity.magnitude>terminal_velocity)
        {
            //find out how much we need to ajust by to go back to TV
            float ajust = (terminal_velocity / boulder.velocity.magnitude); 
            //change the vel to TV using that fraction
            boulder.velocity.Set(boulder.velocity.x * ajust, boulder.velocity.y * ajust);

        }*/

    }

    private void updateMass()
	{
		if (health > 0.1f)
		{
			boulder.mass = initialMass * health;
			boulder.transform.localScale = new Vector3 (health, health, health);
			boulder.drag = 0.5f/health;
		}
	}

	public void damage(float dValue)
	{
		health -= dValue;
		if (health < 0.0f)
			health = 0.0f;
	}
}
