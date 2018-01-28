using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {
    Rigidbody rb;

    string Class = "Honey DILF";

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log("h-"+horizontal+" v-"+vertical);
        //rb.AddForce(new Vector3(horizontal*10, 0, vertical*10));
        rb.velocity = new Vector3(horizontal, rb.velocity.y, vertical);

        if (Input.GetButtonDown("xbox_controller1_a"))
        {
            rb.AddForce(new Vector3(0,200,0));
            Debug.Log("Jump");
        }
    }
}
