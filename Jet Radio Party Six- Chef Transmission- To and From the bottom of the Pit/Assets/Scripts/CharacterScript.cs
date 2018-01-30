using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {
    Rigidbody rb;

    public Sprite[] runningLeft;
    public Sprite[] runningRight;
    public Sprite standing;
    public Sprite jumping;

    public GameObject Shot;

    public Animator animator;

    string Class = "Honey DILF";

    Vector3 lastDirection = new Vector3(1,0,1);

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Debug.Log("h-"+horizontal+" v-"+vertical);
        //rb.AddForce(new Vector3(horizontal*10, 0, vertical*10));
        rb.velocity = new Vector3(horizontal, rb.velocity.y, vertical);

        lastDirection = new Vector3(horizontal, 0, vertical);

        if (horizontal > 0f && vertical > 0f) {
            //right
            animator.SetInteger("Direction", 3);
        }
        if (horizontal < 0f && vertical < 0f)
        {
            //Left
            animator.SetInteger("Direction", 1);
        }
        if (horizontal < 0f && vertical > 0f)
        {
            //up
            animator.SetInteger("Direction", 2);
        }
        if (horizontal > 0f && vertical < 0f)
        {
            //down
            animator.SetInteger("Direction", 0);
        }
        if (horizontal == 0 && vertical == 0)
        {
            animator.StopPlayback();
            animator.enabled = false;
        }
        else {
            animator.enabled = true;
        }

        if (Input.GetButtonDown("xbox_controller1_r_trigger")){
            fireShot(lastDirection);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetButtonDown("xbox_controller1_a")&&rb.velocity.y<=0.5)
        {
            rb.AddForce(new Vector3(0, 100, 0));
            Debug.Log("Jump");
            
        }
    }

    public void fireShot(Vector3 direction) {
        if (direction.x == 0 && direction.z == 0)
            direction = new Vector3(1,0,-1);
        Vector3 spawnPoint = new Vector3(transform.position.x+direction.x, transform.position.y, transform.position.z+direction.z);
        Instantiate(Shot, spawnPoint, transform.rotation).GetComponent<shotScript>().setDirection(direction*5);
    }
}
