using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public GameObject tank;
    private GameObject wallet;


    private List<GameObject> wave = new List<GameObject>(); 

    private bool waveDone = false;
    private bool waveActive = false;
    private float waveTime = 0f;

    private float waveSpacing = .5f;
    private int waveActivateIndex = 0;
    



    private float[,] path = {{-6.5f,-7f},           //future bring in the path when instatiating the tank {10f,-3f},
                             {-6.5f,-2f},
                             {5.25f,-2f},              //this works smoothest if we pass in only the corners
                             {5.25f,2f},
                             {-2.5f,2f},
                             {-2.5f,7f},
                            };


    void Awake()
    {

        ILevel currentLevel = GameData.Instance.getCurrLvlData();

        int numOfTroops = currentLevel.getTroopsPerWave();

        Debug.Log("Troops in wave: " + numOfTroops);
        
        
        //this.wave = new GameObject[numOfTroops];
        for (int i = 0; i < numOfTroops; i++){
            this.wave.Add(Instantiate(this.tank) as GameObject);
            this.wave[i].GetComponent<tankScript>().setWallet(this.wallet);
            this.wave[i].GetComponent<Transform>().position = new Vector3(path[0, 0], path[0, 1], -0.01f);
        }
    }



    public void startWave(int numOfTroops)
    {
        for (int i = 0; i < numOfTroops; i++)
        {
            this.wave[i].GetComponent<tankScript>().setWallet(this.wallet);
        }

        this.waveActive = true;
        this.waveTime = Time.time;
    }

    void Update()
    { 
        if (this.waveActive)
        {
            setUpTroops();
            this.checkTroops();
        }
    }

    //set up the troop inital spacing
    private void setUpTroops()
    {
        if (waveActivateIndex < this.wave.Count)
        {
            if (Time.time > this.waveTime)
            {
                this.wave[waveActivateIndex].GetComponent<tankScript>().activateTroop(this.path);
                waveActivateIndex++;
                this.waveTime += this.waveSpacing;
            }
        }
    }

    //checks to see if the wave is done yet
    private void checkTroops()
    {
        bool flag = false;

        for (int i = 0; i < wave.Count; i++)
        {
            if(wave[i].GetComponent<tankScript>().getLifeStat())
            {
                flag = true;
            }
        }

        if (flag == false)
        {
            this.waveDone = true;
        }
    }

    //returns the active wave gameobjects
    public List<GameObject> getWave()
    {
        return this.wave;
    }

    //returns when the wave is finnished
    public bool getDoneStat()
    {
        return this.waveDone;
    }

    //set the wallet so the troops know where to send the money
    public void setWallet(GameObject wallet)
    {
        this.wallet = wallet;
    }

    //destroy all the troops in the wave when we destroy the wave
    public void OnDestroy()
    {
        for (int i = wave.Count - 1 ; i > -1; i--)
        {
            Destroy(this.wave[i]);
        }
    }

}
