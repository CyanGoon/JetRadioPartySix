using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {
    public GameObject gameTile;

    private GameObject tile;
    private TileScript tileScript;

    // Use this for initialization
    void Start () {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                tile = Instantiate(gameTile, new Vector3(x, 0, y), Quaternion.identity);
                tileScript = (TileScript)tile.GetComponent<TileScript>();
                tileScript.UpdateTexture(1);
                //tileScript.testFunction();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
