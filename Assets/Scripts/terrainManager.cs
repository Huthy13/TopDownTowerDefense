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



    private static int fieldHeight = 8;
    private static int fieldWidth = 15;

    public GameObject[,] field = new GameObject[fieldWidth, fieldHeight];


    public int[,] template =   {{001,001,001,001,001,103,001,002,000,000,000,000,000,000,000},
                                {001,001,001,001,001,103,001,002,000,000,000,000,000,000,000},
                                {001,001,001,001,001,112,102,200,100,100,100,110,000,000,000},
                                {001,001,001,001,001,001,001,002,000,000,000,101,000,000,000},
                                {001,001,001,001,001,001,001,002,000,000,000,101,000,000,000},
                                {001,001,113,102,102,102,102,200,100,100,100,111,000,000,000},
                                {001,001,103,001,001,001,001,002,000,000,000,000,000,000,000},
                                {001,001,103,001,001,001,001,002,000,000,000,000,000,000,000},
                                };



    //scaling and field setup
    private Vector3 startingPos = new Vector3(-9f, 4.5f, 0f);
    private float widthScale = 1.28f;
    private float heightScale = -1.28f;



    // Start is called before the first frame update
    void Start()
    {
        //build dictionary
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


        // Instantiate field.
        for (int i = 0; i < fieldWidth; i++)
        {
            for (int j = 0; j < fieldHeight; j++)
            {
                myPrefab = tileValue[template[j, i]];


                field[i, j] = Instantiate(myPrefab) as GameObject;
                field[i, j].GetComponent<Transform>().position = (startingPos + new Vector3(i * widthScale, j * heightScale, 0.0f));
            }
        }

    }


}
