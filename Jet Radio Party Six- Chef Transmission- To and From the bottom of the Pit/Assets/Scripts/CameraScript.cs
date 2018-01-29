using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject character;    

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(character.transform.position.x + 6, 12, character.transform.position.z-5);
        
    }
}
