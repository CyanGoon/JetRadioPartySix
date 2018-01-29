using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public GameObject[] tileQuads = new GameObject[5];
    public GameObject gameCube;
    public GameObject fog;

    public bool isDiscovered = false;

    public bool isAngled = false;
    public bool isCube = false;

    int currentTexture;
    float height=0;
    public bool isVisible = false;
    public bool cubeIsVisible=false;

    public Texture2D[] textures;

    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    //Update the current texture
    public void UpdateTexture(int newTexture) {
        

        for (int i = 0; i < tileQuads.Length; i++) {
            Material tileMaterial;
            tileMaterial = tileQuads[i].GetComponent<Renderer>().material;
            tileMaterial.mainTexture = textures[newTexture];
        }
        currentTexture = newTexture;
        
    }

    public void testFunction() {
        Debug.Log("test function works");
    }

    public int getTextureArraySize() {
        return textures.Length;
    }

    public void setHeight(float height) {
        this.height = height;
    }

    public float getHeight() {
        return height;
    }

    public Transform getTransform() {
        return GetComponent<Transform>();
    }

    public void setIsVisible(bool isVisible) {
        this.isVisible = isVisible;

        for (int i = 0; i < tileQuads.Length; i++)
        {
            //tileQuads[i].GetComponent<MeshRenderer>().enabled = isVisible;
        }        
    }

    public void setGameCubeVisibility(bool isVisible)
    {
        MeshRenderer mr = gameCube.GetComponent<MeshRenderer>();
        mr.enabled = isVisible;
        cubeIsVisible = isVisible;
    }

    public void setGameCubeHeight(float scalar) {
        Transform cubeTransform = gameCube.GetComponent<Transform>();
        cubeTransform.localScale = new Vector3(1, scalar, 1);
    }

    public void discover() {
        isDiscovered = true;
        fog.GetComponent<MeshRenderer>().enabled = false;
    }
}
