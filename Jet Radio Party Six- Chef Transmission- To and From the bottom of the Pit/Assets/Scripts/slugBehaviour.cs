using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slugBehaviour : MonoBehaviour
{

    public GameObject target;
    private Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, rb.transform.position);

        if (distance <= 12)
        {
            Vector3 difference = target.transform.position - transform.position;
            difference = difference.normalized;

            rb.AddForce(difference * (420 /distance));
            //rb.velocity = difference * (420 / distance);
        }

    }

   public void setTarget(GameObject target)
    {

        this.target = target;
    }

}
