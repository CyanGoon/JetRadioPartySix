using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public GameObject tileQuad;

    int currentTexture;

    public Texture2D[] textures;

    Material tileMaterial;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    //Update the current texture
    public void UpdateTexture(int newTexture) {
        tileMaterial = tileQuad.GetComponent<Renderer>().material;
        currentTexture = newTexture;
        tileMaterial.mainTexture = textures[newTexture];
    }

    public void testFunction() {
        Debug.Log("test function works");
    }
}
