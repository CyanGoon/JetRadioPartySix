using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Debug.Log(horizontal);
        //rb.AddForce(new Vector3(horizontal*10, 0, vertical*10));
        rb.velocity = new Vector3(horizontal, rb.velocity.y, vertical);

    }
}
