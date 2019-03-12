using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public AI path_finder;
    private float radius = 0.5f;

    private void Start()
    {
        SphereCollider sphere_col = this.gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        sphere_col.radius = radius;
        sphere_col.isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(path_finder.gameObject == other.gameObject)
        {
            path_finder.target_reached = true;
            path_finder.target_set = false;
            Destroy(this.gameObject);
        }
    }
}
