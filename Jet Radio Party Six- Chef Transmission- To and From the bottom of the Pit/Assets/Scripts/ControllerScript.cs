using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour {
    public GameObject gameTile;
    public GameObject crystal;
    public GameObject directionalLight;

    public GameObject fungalTower;
    public GameObject slug;

    public float timeOfDay = 50;
    public bool timeIsGoingUp = true;

    public GameObject player1;

    private GameObject tile;
    private TileScript tileScript;

    static int mapSize = 100;
    private GameObject[,] gameTiles = new GameObject[mapSize,mapSize];

    int mapHeightScale = 20;
    int mapChangeDensity = 5;
    float mapHeightIncrement = 0.5f;

    float mapSeed;

    // Use this for initialization
    void Start () {
        mapSeed = Random.value * 100;
        
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                //float height = Mathf.Round(Random.value * 3)/2;
                float height = Mathf.Round(Mathf.PerlinNoise((mapSeed + (float)x)/ mapSize* mapChangeDensity, (mapSeed + (float)y)/ mapSize* mapChangeDensity) *mapHeightScale)*mapHeightIncrement;
                tile = Instantiate(gameTile, new Vector3(x, height, y), Quaternion.identity);

                if (x == Mathf.Round(mapSize / 2) && y == Mathf.Round(mapSize / 2))
                    player1.transform.position = new Vector3(x, height+1, y);

                gameTiles[x, y] = tile;

                tileScript = (TileScript)tile.GetComponent<TileScript>();
                tileScript.setHeight(height);
                //tileScript.UpdateTexture((int)(Random.value*tileScript.getTextureArraySize()));
                if (tileScript.getHeight() <= 0.5) {
                    tileScript.UpdateTexture(1);
                }
                if (tileScript.getHeight() >= 6.5) {
                    tileScript.UpdateTexture(2+(int)(Random.value*3));                    
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

                if ((int)(Random.value * 100) == 1) {
                    spreadFungus(x,y);                    
                }

                if ((int)(Random.value * 100) == 1)
                {
                    spreadSalt(x, y);
                }

            }
        }


    }

    public void spreadFungus(int x, int y) {
        GameObject currentTile = gameTiles[x, y];
        TileScript currentTileScript = currentTile.GetComponent<TileScript>();

        currentTileScript.UpdateTexture(7);

        GameObject sloathe = Instantiate(slug, new Vector3(currentTile.transform.position.x, currentTile.transform.position.y + 3.25f, currentTile.transform.position.z), player1.transform.rotation);
        sloathe.GetComponent<slugBehaviour>().setTarget(player1);

        if (Random.value * 10 > 3)
        {
            int desiredPosx = (int)(x + Random.value * 3 - 1);
            int desiredPosy = (int)(y + Random.value * 3 - 1);
            if(desiredPosx>0&&
                desiredPosx<mapSize&&
                desiredPosy>0&&
                desiredPosy<mapSize)
            spreadFungus(desiredPosx, desiredPosy);
        }
    }

    public void spreadSalt(int x, int y)
    {
        GameObject currentTile = gameTiles[x, y];
        TileScript currentTileScript = currentTile.GetComponent<TileScript>();

        currentTileScript.UpdateTexture(9+(int)(Random.value*2));

        GameObject fungoise = Instantiate(fungalTower, new Vector3(currentTile.transform.position.x, currentTile.transform.position.y+0.25f, currentTile.transform.position.z), player1.transform.rotation);
        fungoise.GetComponent<fungalTowerBehaviour>().setTarget(player1);
        fungoise.transform.Rotate(0,Random.value*360,0);
        float scale = Random.value;
        fungoise.transform.localScale = new Vector3(scale,scale,scale);

        if (Random.value * 10 > 3)
        {
            int desiredPosx = (int)(x + Random.value * 3 - 1);
            int desiredPosy = (int)(y + Random.value * 3 - 1);
            if (desiredPosx > 0 &&
                desiredPosx < mapSize &&
                desiredPosy > 0 &&
                desiredPosy < mapSize)
                spreadSalt(desiredPosx, desiredPosy);
        }
    }

    // Update is called once per frame
    void Update () {
        discoverFrom((int)player1.transform.position.x, (int)player1.transform.position.z);

        if (timeIsGoingUp)
        {
            timeOfDay += Time.deltaTime;
            if (timeOfDay > 100)
                timeIsGoingUp = false;
        }
        else {
            timeOfDay -= Time.deltaTime;
            if (timeOfDay < 0)
                timeIsGoingUp = true;
        }

        directionalLight.GetComponent<Light>().intensity = timeOfDay/50;
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

    public void discoverFrom(int x, int y)
    {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                GameObject adjacentTile = gameTiles[x + i, y + j];
                TileScript adjacentTileScript = adjacentTile.GetComponent<TileScript>();

                adjacentTileScript.discover();
            }
        }
    }
}
