using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    //Needs setting before running (will update to update later)
    public float size_x = 1f;
    public float size_y = 1f;
    public float size_z = 1f;

    public float pos_x = 0f;
    public float pos_y = 0f;
    public float pos_z = 0f;

    public bool cam_2d = false;
    public bool camera_lock = false;

    private Rigidbody target_rb;
    private Rigidbody2D target_rb_2d;

    public Camera cam;

    public float camera_speed = 0.005f;
    private int sides = 6;

    private string[] names = { "Cam_Top", "Cam_Bottom", "Cam_Left",
                               "Cam_Right", "Cam_Back", "Cam_Front" };

    public bool in_center = true;
    public bool in_sides = false;
    public bool face_target = false;

    private GameObject border;

    public Vector3 offset = Vector3.zero;

    private void Start()
    {
        if (!cam)
            cam = Camera.main;

        if (camera_lock)
        {
            Vector3 target_position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) + offset;
            cam.transform.position = target_position;

            return;
        }

        border = new GameObject();
        border.SetActive(true);
        border.transform.position = transform.position;
        border.name = "Cam_Center";

        if (cam_2d)
        {
            makeTriggerCollider(border, new Vector2(size_x, size_y));
            sides = 4;
            cameraSetup2D();
            targetSetup2D();
            cam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f) + offset;
        }
        else
        {
            makeTriggerCollider(border, new Vector3(size_x, size_y, size_z));
            sides = 6;
            cameraSetup();
            targetSetup();
            cam.transform.position = this.transform.position + offset;
        }


    }



    private void Update()
    {
        if(camera_lock)
        {
            Vector3 target_position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) + offset;
            Vector3 camera_smoothed_position = Vector3.Lerp(cam.transform.position, target_position, camera_speed);
            cam.transform.position = camera_smoothed_position;
        }
        else if (cam_2d)
        {
            if (!in_center && !in_sides)
            {
                Vector3 target_position = new Vector3(this.transform.position.x, this.transform.position.y, 0f) + offset;

                Vector3 camera_smoothed_position = Vector3.Lerp(cam.transform.position, target_position, camera_speed * 2);
                cam.transform.position = camera_smoothed_position;

                Vector2 border_smoothed_position = Vector2.Lerp(border.transform.position, target_position, camera_speed * 2);
                border.transform.position = border_smoothed_position;
            }
            else if (!in_center && in_sides)
            {
                Vector3 target_position = new Vector3(this.transform.position.x, this.transform.position.y, 0f) + offset;

                Vector3 cam_smoothed_position = Vector3.Lerp(cam.transform.position, target_position, camera_speed);
                cam.transform.position = cam_smoothed_position;

                Vector2 border_smoothed_position = Vector2.Lerp(border.transform.position, target_position, camera_speed);
                border.transform.position = border_smoothed_position;
            }
        }
        else
        {
            if (face_target)
                cam.transform.LookAt(this.transform);


            if (!in_center && !in_sides)
            {
                Vector3 cam_target_position = this.transform.position + offset;
                Vector3 cam_smoothed_position = Vector3.Lerp(cam.transform.position, cam_target_position, camera_speed * 2);
                cam.transform.position = cam_smoothed_position;

                Vector3 border_target_position = this.transform.position;
                Vector3 border_smoothed_position = Vector3.Lerp(border.transform.position, border_target_position, camera_speed * 2);
                border.transform.position = border_smoothed_position;
            }
            else if (!in_center && in_sides)
            {
                Vector3 target_position = this.transform.position + offset;
                Vector3 smoothed_position = Vector3.Lerp(cam.transform.position, target_position, camera_speed);
                cam.transform.position = smoothed_position;

                Vector3 border_target_position = this.transform.position;
                Vector3 border_smoothed_position = Vector3.Lerp(border.transform.position, border_target_position, camera_speed);
                border.transform.position = border_smoothed_position;
            }
        }
    }



    private void cameraSetup2D()
    {
        for (int i = 0; i < sides; i++)
        {
            GameObject go = new GameObject();
            go.SetActive(true);

            Vector2 position = Vector3.zero;
            Vector2 col_size = Vector3.zero;
            float collider_vol = 0.1f;
            string name = names[i];

            float temp_size_x = 0f;
            float temp_size_y = 0f;

            switch (name)
            {
                case "Cam_Top":

                    position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                    position += new Vector2(0f, (size_y / 2));

                    temp_size_x = size_x - collider_vol;
                    temp_size_y = collider_vol;

                    break;

                case "Cam_Bottom":

                    position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                    position += new Vector2(0f, (-size_y / 2));

                    temp_size_x = size_x - collider_vol;
                    temp_size_y = collider_vol;

                    break;

                case "Cam_Left":

                    position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                    position += new Vector2((-size_x / 2), 0f);

                    temp_size_x = collider_vol;
                    temp_size_y = size_y - collider_vol;

                    break;

                case "Cam_Right":

                    position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                    position += new Vector2((size_x / 2), 0f);

                    temp_size_x = collider_vol;
                    temp_size_y = size_y - collider_vol;

                    break;

                default:
                    break;
            }

            go.name = name;
            go.transform.position = position;
            go.transform.parent = border.gameObject.transform;

            col_size = new Vector2(temp_size_x, temp_size_y);

            makeTriggerCollider(go, col_size);
        }
    }



    private void targetSetup2D()
    {
        if (!(target_rb_2d = this.GetComponent<Rigidbody2D>()))
        {
            target_rb_2d = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            target_rb_2d.gravityScale = 0f;
        }

        if (!this.GetComponent<BoxCollider2D>())
        {
            BoxCollider2D temp_col = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            temp_col.size = Vector2.one;
        }
    }



    private void cameraSetup()
    {
        for (int i = 0; i < sides; i++)
        {
            GameObject go = new GameObject();
            go.SetActive(true);

            Vector3 position = Vector3.zero;
            Vector3 col_size = Vector3.zero;
            float collider_inside = 0.1f;
            string name = names[i];

            float temp_size_x = 0f;
            float temp_size_y = 0f;
            float temp_size_z = 0f;
            float change = 0f;

            switch (name)
            {
                case "Cam_Top":

                    change = (size_y / 2) + (collider_inside / 2);
                    position = this.gameObject.transform.position + new Vector3(0f, change, 0f);

                    temp_size_x = size_x;
                    temp_size_y = collider_inside;
                    temp_size_z = size_z;

                    break;

                case "Cam_Bottom":

                    change = (-size_y / 2) - (collider_inside / 2);
                    position = this.gameObject.transform.position + new Vector3(0f, change, 0f);

                    temp_size_x = size_x;
                    temp_size_y = collider_inside;
                    temp_size_z = size_z;

                    break;

                case "Cam_Left":

                    change = (-size_x / 2) - (collider_inside / 2);
                    position = this.gameObject.transform.position + new Vector3(change, 0f, 0f);

                    temp_size_x = collider_inside;
                    temp_size_y = size_y;
                    temp_size_z = size_z;

                    break;

                case "Cam_Right":

                    change = (-size_x / 2) - (collider_inside / 2);
                    position = this.gameObject.transform.position + new Vector3(change, 0f, 0f);

                    temp_size_x = collider_inside;
                    temp_size_y = size_y;
                    temp_size_z = size_z;

                    break;

                case "Cam_Back":

                    change = (size_z / 2) + (collider_inside / 2);
                    position = this.gameObject.transform.position + new Vector3(0f, 0f, change);

                    temp_size_x = size_x;
                    temp_size_y = size_y;
                    temp_size_z = collider_inside;

                    break;

                case "Cam_Front":

                    change = (-size_z / 2) - (collider_inside / 2);
                    position = this.gameObject.transform.position + new Vector3(0f, 0f, change);

                    temp_size_x = size_x;
                    temp_size_y = size_y;
                    temp_size_z = collider_inside;

                    break;

                default:
                    break;
            }

            go.name = name;
            go.transform.position = position;
            go.transform.parent = border.gameObject.transform;

            col_size = new Vector3(temp_size_x, temp_size_y, temp_size_z);

            makeTriggerCollider(go, col_size);
        }
    }



    private void targetSetup()
    {
        if (!(target_rb = this.GetComponent<Rigidbody>()))
        {
            target_rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            target_rb.isKinematic = true;
            target_rb.useGravity = false;
        }

        if (!this.GetComponent<BoxCollider>())
        {
            BoxCollider temp_col = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
            temp_col.size = Vector3.one;
        }
    }



    private void OnTriggerExit2D(Collider2D col)
    {
        string name = col.name;

        switch (name)
        {
            case "Cam_Center":

                in_center = false;

                break;

            case "Cam_Top":
            case "Cam_Bottom":
            case "Cam_Left":
            case "Cam_Right":

                in_sides = false;

                break;

            default:
                break;
        }
    }



    private void OnTriggerExit(Collider col)
    {
        string name = col.name;

        switch (name)
        {
            case "Cam_Center":

                in_center = false;

                break;

            case "Cam_Top":
            case "Cam_Bottom":
            case "Cam_Left":
            case "Cam_Right":
            case "Cam_Back":
            case "Cam_Front":

                in_sides = false;

                break;

            default:
                //        print(name);
                break;
        }
    }



    private void OnTriggerStay2D(Collider2D col)
    {
        string name = col.name;

        switch (name)
        {
            case "Cam_Center":
                in_center = true;

                break;

            case "Cam_Top":
            case "Cam_Bottom":
            case "Cam_Left":
            case "Cam_Right":

                in_sides = true;

                break;

            default:
                break;
        }
    }



    private void OnTriggerStay(Collider col)
    {
        string name = col.name;

        switch (name)
        {
            case "Cam_Center":

                in_center = true;

                break;

            case "Cam_Top":
            case "Cam_Bottom":
            case "Cam_Left":
            case "Cam_Right":
            case "Cam_Back":
            case "Cam_Front":

                in_sides = true;

                break;

            default:
                break;
        }
    }



    private void makeTriggerCollider(GameObject go, Vector2 size)
    {
        BoxCollider2D col = go.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        col.size = size;
        col.isTrigger = true;

    }



    private void makeTriggerCollider(GameObject go, Vector3 size)
    {
        BoxCollider col = go.AddComponent(typeof(BoxCollider)) as BoxCollider;
        col.size = size;
        col.isTrigger = true;
    }
}