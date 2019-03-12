using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleController : MonoBehaviour
{

    private SpriteRenderer spr_renderer;
    public Sprite[] sprites;
    public int frame = 0;
    public float time_between_ani = 3f;
    public float timer = 0f;
    public bool timer_on = false;
    public float max_health = 10f;
    public float health = 10f;
    public float frame_health = 0f;
    public float damage = 0.0f;

    void Start ()
    {
        spr_renderer = this.GetComponent<SpriteRenderer>();
        spr_renderer.sprite = sprites[frame];
        frame_health = health / sprites.Length;
	}

    void Update()
    {
        if (timer_on)
        {
            timer += Time.deltaTime;
            if (timer >= time_between_ani)
            {
                timer = 0f;
                timer_on = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (frame == -1) //"Animation" is finished
            return;

        float health_lost = 0f;

        if (col.gameObject.tag == "Player" &&
            !timer_on)
        {
            damage = (col.collider.attachedRigidbody.mass * col.collider.attachedRigidbody.velocity.magnitude)
               * Vector2.Dot(col.collider.attachedRigidbody.velocity.normalized, new Vector2((this.gameObject.transform.position.x - col.collider.attachedRigidbody.position.x),
               (this.gameObject.transform.position.y - col.collider.attachedRigidbody.position.y)));


            health -= damage;
            health_lost = max_health - health;
            frame = (int)(health_lost / frame_health);
            timer_on = true;
        }

        if (frame >= sprites.Length ||
            health <= 0f)
        {
            this.gameObject.SetActive(false);
            frame = -1;
            return;
        }

        spr_renderer.sprite = sprites[frame];
       
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            damage = (col.attachedRigidbody.mass * col.attachedRigidbody.velocity.magnitude)
               * Vector2.Dot(col.attachedRigidbody.velocity.normalized, new Vector2((this.gameObject.transform.position.x - col.attachedRigidbody.position.x),
               (this.gameObject.transform.position.y - col.attachedRigidbody.position.y)));

            if (damage >= health)
            {
                this.gameObject.SetActive(false);
                frame = -1;
                return;
            }
        }
    }
}
