using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //private Transform transform;
    //public GameObject target;

    [SerializeField]
    private List<Transform> targets = new List<Transform>();
    [SerializeField]
    private float camera_distance = -10.0f;
    private float camera_min_size = 5.0f;
    [SerializeField]
    private float camera_limit = 2.0f;
    [SerializeField]
    private float camera_speed = 5.0f;

    private void Start()
    {
        Vector3 target = FindCenterPosition();

        Vector3 init_camera_position = new Vector3(target.x, target.y, camera_distance);
        Camera.main.transform.position = init_camera_position;

        if (targets.Count <= 0)
        {
            print("Target has not been set, please drag a game object into the script CameraController");
            return;
        }
    }

    private void Update()
    {
        Vector3 target = FindCenterPosition();

        Vector2 a = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        Vector2 b = new Vector2(target.x, target.y);
        float distance = Vector2.Distance(a, b);

        if (distance >= camera_limit)
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(target.x,
                                                        target.y,
                                                        camera_distance), camera_speed * Time.deltaTime);
    }

    public void AddTarget(Transform target)
    {
        if (targets.Contains(target))
            return;

        targets.Add(target);
    }

    private Vector3 FindCenterPosition()
    {
        if (targets.Count == 0)
            return new Vector3();
        else if (targets.Count == 1)
            return targets[0].position;

        Vector3 back_position = targets[0].position;
        Vector3 front_position = targets[0].position;

        foreach (Transform option in targets)
        {
            if (back_position.x > option.position.x)
            {
                back_position = option.position;
                continue;
            }

            if (front_position.x < option.position.x)
            {
                front_position = option.position;
                continue;
            }
        }

        Vector3 difference = front_position - back_position;
        difference /= 2;

        Vector3 target = back_position + difference;
        
        float zoom_target = (front_position.x - back_position.x) / 4;

        if (zoom_target < camera_min_size)
            zoom_target = camera_min_size;

        if (Camera.main.orthographicSize < zoom_target)
        {
            float new_size = Camera.main.orthographicSize;
            new_size += (zoom_target - Camera.main.orthographicSize) * Time.deltaTime;

            if (new_size > zoom_target)
                new_size = zoom_target;

            Camera.main.orthographicSize = new_size;
        }
        else if (Camera.main.orthographicSize > zoom_target)
        {
            float new_size = Camera.main.orthographicSize;
            new_size -= (Camera.main.orthographicSize - zoom_target) * Time.deltaTime;

            if (new_size < zoom_target)
                new_size = zoom_target;

            Camera.main.orthographicSize = new_size;
            //Camera.main.orthographicSize -= ((Camera.main.orthographicSize - zoom_target) * Time.deltaTime);
        }
        
        return target;
    }
} 
