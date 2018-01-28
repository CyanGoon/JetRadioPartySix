using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public GameObject tileQuad;
    public GameObject gameCube;

    int currentTexture;
    float height=0;
    private bool isVisible = false;

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
        MeshRenderer mr = tileQuad.GetComponent<MeshRenderer>();
        mr.enabled = isVisible;
    }

    public void setGameCubeVisibility(bool isVisible)
    {
        MeshRenderer mr = gameCube.GetComponent<MeshRenderer>();
        mr.enabled = isVisible;
    }
}
