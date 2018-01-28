using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {
    public GameObject gameTile;

    private GameObject tile;
    private TileScript tileScript;

    static int mapSize = 100;
    private GameObject[,] gameTiles = new GameObject[mapSize,mapSize]; 

    // Use this for initialization
    void Start () {
        
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                //float height = Mathf.Round(Random.value * 3)/2;
                float height = Mathf.Round(Mathf.PerlinNoise((float)x/ mapSize*10, (float)y/ mapSize*10) *5)/2;
                tile = Instantiate(gameTile, new Vector3(x, height, y), Quaternion.identity);

                gameTiles[x, y] = tile;

                tileScript = (TileScript)tile.GetComponent<TileScript>();
                tileScript.setHeight(height);
                //tileScript.UpdateTexture((int)(Random.value*tileScript.getTextureArraySize()));
                if (tileScript.getHeight() == 0.5) {
                    tileScript.UpdateTexture(1);
                }

                //angleTileUpInDirection(ref tileScript, new Vector3(0, 0, 1));
                tileScript.setIsVisible(true);
                tileScript.setGameCubeVisibility(false);
                
            }
        }

        for (int y = 1; y < mapSize-1; y++)
        {
            for (int x = 1; x < mapSize-1; x++)
            {
                bool heightChange = false;
                for (int i = -1; i <= 1; i++) {
                    for (int j = -1; j <= 1; j++) {
                        GameObject currentTile = gameTiles[x, y];
                        TileScript currentTileScript = currentTile.GetComponent<TileScript>();
                        GameObject adjacentTile = gameTiles[x + i, y + j];
                        TileScript adjacentTileScript = adjacentTile.GetComponent<TileScript>();
                        if (currentTileScript.getHeight()<adjacentTileScript.getHeight()) {
                            currentTileScript.setIsVisible(false);
                            currentTileScript.setGameCubeVisibility(true);
                        }
                    }
                }

            }
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void angleTileUpInDirection(ref TileScript tileScript, Vector3 direction) {
        Vector3 scaleVector = direction * 0.18f;
        scaleVector += new Vector3(1, 1, 1);

        if (direction.x > 0)
            tileScript.getTransform().Rotate(0,0,-27);
        if(direction.x<0)
            tileScript.getTransform().Rotate(0, 0, 27);
        if(direction.z>0)
            tileScript.getTransform().Rotate(-27, 0, 0);
        if(direction.z<0)
            tileScript.getTransform().Rotate(27, 0, 0);

        tileScript.getTransform().localScale = scaleVector;
    }
}
