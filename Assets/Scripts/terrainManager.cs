using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainManager : MonoBehaviour
{

    private GameObject myPrefab;

    public GameObject grass;
    public GameObject sand;
    public GameObject transitionPiece;
    public GameObject transitionRoad;
    public GameObject grassHZRoad;
    public GameObject grassVTRoad;
    public GameObject sandHZRoad;
    public GameObject sandVTRoad;
    public GameObject grassRTDNTurn;
    public GameObject grassRTUPTurn;
    public GameObject sandLTDNTurn;
    public GameObject sandLTUPTurn;

    //Dictionary to relate tiles to thier numeric equivilent
    private Dictionary<int, GameObject> tileValue = new Dictionary<int, GameObject>();


    //scaling and field setup
    private Vector3 startingPos = new Vector3(-9f, 4.5f, 0f);
    private float widthScale = 1.28f;
    private float heightScale = -1.28f;

    //the variables to build the map
    private int[,] mapTemplate;
    private ILevel currentLevel;
    private GameObject[,] field;

    


    // Start is called before the first frame update
    void Start()
    {
        //Setup GameData Singleton
        currentLevel = GameData.Instance.getCurrLvlData();

        //Grab the map template from the Game Data
        this.mapTemplate = currentLevel.getMapTemplate();

        //Make all the empty GameObjects for the tiles
        this.field = new GameObject[this.mapTemplate.GetLength(1), this.mapTemplate.GetLength(0)];
        
        this.buildDictionary();
        this.createMapObjects();

    }

    private void buildDictionary()
    {
        //build dictionary to relate tile codes to gameobjects 
        tileValue.Add(1, grass);
        tileValue.Add(0, sand);
        tileValue.Add(2, transitionPiece);
        tileValue.Add(200, transitionRoad);
        tileValue.Add(102, grassHZRoad);
        tileValue.Add(103, grassVTRoad);
        tileValue.Add(100, sandHZRoad);
        tileValue.Add(101, sandVTRoad);
        tileValue.Add(113, grassRTDNTurn);
        tileValue.Add(112, grassRTUPTurn);
        tileValue.Add(110, sandLTDNTurn);
        tileValue.Add(111, sandLTUPTurn);

    }

    private void createMapObjects()
    {
        // Instantiate field.
        for (int i = 0; i < this.mapTemplate.GetLength(1); i++)
        {
            for (int j = 0; j < this.mapTemplate.GetLength(0); j++)
            {
                myPrefab = tileValue[this.mapTemplate[j, i]];

                field[i, j] = Instantiate(myPrefab) as GameObject;
                field[i, j].GetComponent<Transform>().position = (startingPos + new Vector3(i * widthScale, j * heightScale, 0.0f));
            }
        }

    }


}
