using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {
    public GameObject gameTile;

    private GameObject tile;
    private TileScript tileScript;

    static int mapSize = 100;
    private GameObject[,] gameTiles = new GameObject[mapSize,mapSize];

    int mapHeightScale = 10;
    int mapChangeDensity = 10;
    float mapHeightIncrement = 0.5f;

    // Use this for initialization
    void Start () {
        
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                //float height = Mathf.Round(Random.value * 3)/2;
                float height = Mathf.Round(Mathf.PerlinNoise((float)x/ mapSize* mapChangeDensity, (float)y/ mapSize* mapChangeDensity) *mapHeightScale)*mapHeightIncrement;
                tile = Instantiate(gameTile, new Vector3(x, height, y), Quaternion.identity);

                gameTiles[x, y] = tile;

                tileScript = (TileScript)tile.GetComponent<TileScript>();
                tileScript.setHeight(height);
                //tileScript.UpdateTexture((int)(Random.value*tileScript.getTextureArraySize()));
                if (tileScript.getHeight() <= 0.5) {
                    tileScript.UpdateTexture(1);
                }
                if (tileScript.getHeight() >= 4) {
                    tileScript.UpdateTexture(2);
                }

                //angleTileUpInDirection(ref tileScript, new Vector3(0, 0, 1));
                tileScript.setIsVisible(false);
                tileScript.setGameCubeVisibility(true);
                tileScript.setIsVisible(true);
                tileScript.setGameCubeVisibility(false);

            }
        }

        for (int y = 1; y < mapSize-1; y++)
        {
            for (int x = 1; x < mapSize-1; x++)
            {
                bool heightChange = false;

                GameObject currentTile = gameTiles[x, y];
                TileScript currentTileScript = currentTile.GetComponent<TileScript>();

                for (int i = -1; i <= 1; i++) {
                    for (int j = -1; j <= 1; j++) {                        
                        GameObject adjacentTile = gameTiles[x + i, y + j];
                        TileScript adjacentTileScript = adjacentTile.GetComponent<TileScript>();
                        if (currentTileScript.getHeight()< adjacentTileScript.getHeight()) {
                            if ((i == 0 || j == 0)&& adjacentTileScript.getHeight() - currentTileScript.getHeight() == 0.5f)//directly beside this tile
                            {
                                if (i != 0) {
                                    angleTileUpInDirection(ref currentTileScript, new Vector3(-i, 0, 0));

                                }
                                if (j != 0) angleTileUpInDirection(ref currentTileScript, new Vector3(0, 0, j));
                                tileScript.setIsVisible(true);
                                tileScript.setGameCubeVisibility(false);
                                currentTileScript.isAngled = true;
                                i = 2;
                                j = 2;
                                break;
                            }
                            else
                            {
                                //currentTileScript.setGameCubeHeight((adjacentTileScript.getHeight() - currentTileScript.getHeight()) * 2);
                                //currentTileScript.isCube = true;
                            }
                        }
                    }
                }

                if (currentTileScript.isCube && !currentTileScript.isAngled) {                    
                    currentTileScript.setIsVisible(false);
                    currentTileScript.setGameCubeVisibility(true);
                    currentTileScript.getTransform().rotation = Quaternion.identity;
                }

            }
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void angleTileUpInDirection(ref TileScript tileScript, Vector3 direction) {
        tileScript.getTransform().Translate(0,0.25f,0);

        if (direction.x > 0)
            tileScript.getTransform().Rotate(0,0,-27);
        if (direction.x < 0)
        {
            tileScript.getTransform().Rotate(0, 0, 27);
            direction.x *= -1;
        }
        if(direction.z>0)
            tileScript.getTransform().Rotate(-27, 0, 0);
        if (direction.z < 0)
        {
            tileScript.getTransform().Rotate(27, 0, 0);
            direction.x = Mathf.Abs(direction.x);
        }

        direction.x = Mathf.Abs(direction.x);
        direction.z = Mathf.Abs(direction.z);

        Vector3 scaleVector = direction * 0.118f;
        scaleVector += new Vector3(1, 1, 1);

        tileScript.getTransform().localScale = scaleVector;
    }
}
