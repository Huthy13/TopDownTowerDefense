using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    private ILevel currLevelData;


    void Awake()
    { 
        this.InstantiateController();
    }

    private void InstantiateController()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }
    }


    public void setCurrLvlData(ILevel currLevelData)
    {
        this.currLevelData = currLevelData;
    }

    public ILevel getCurrLvlData()
    {
        return this.currLevelData;
    }

}
