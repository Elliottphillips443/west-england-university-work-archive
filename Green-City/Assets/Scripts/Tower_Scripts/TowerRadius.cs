using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRadius : MonoBehaviour {
    [SerializeField]
    private float m_rateOfFire;
    [SerializeField]
    private float m_bonusROF;
    [SerializeField]
    private SphereCollider m_col;
    [SerializeField]
    private string m_resourceTag;

    public void SetResourceTag(string tag)
    {
        m_resourceTag = tag;
    }
    
    public void SetSpeed(float ROF)
    {
        m_rateOfFire = ROF;
        m_bonusROF = 10f;
        m_col = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == m_resourceTag)
        {
            other.GetComponent<Rigidbody>().velocity = transform.position - other.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == m_resourceTag)
        {
            Vector3 move_dir = transform.position - other.transform.position;
            //other.GetComponent<Rigidbody>().velocity = move_dir;
            other.GetComponent<Rigidbody>().AddForce(move_dir.normalized * m_rateOfFire, ForceMode.Acceleration);
            if (other.GetComponent<Rigidbody>().drag < 0.2)
            {
                other.GetComponent<Rigidbody>().drag += 0.01f;
            }
        }
    }
}
