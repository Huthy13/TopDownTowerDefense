using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{

    public GameObject level_1;
    public GameObject level_2;
  

    public void loadLevel_1()
    {
        GameData.Instance.setCurrLvlData(this.level_1.GetComponent<Level_1>());
        this.loadLevel();
    }

    public void loadLevel_2()
    {
        GameData.Instance.setCurrLvlData(this.level_2.GetComponent<Level_2>());
        this.loadLevel();
    }


    public void loadLevel()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
    }


}
