using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotScript : MonoBehaviour {

    float lifeTime = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(1,1,1);

        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
            Destroy(this);
    }
}
