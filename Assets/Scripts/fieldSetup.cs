using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fieldSetup : MonoBehaviour
{
    public enum status {starting, running, paused, finished};

    status stat = status.starting;

    //public Managers GameObjects
    public GameObject waveManager;
    public GameObject towerManager;
    public GameObject terrainManager;
    public GameObject levelWallet;

    //gameobjects
    GameObject wave;
    GameObject towers;
    GameObject terrain;
    GameObject wallet;

    GameObject intrusionGUI;

    //level variables
    private int initialCash = 100;
    private int troopsPerWave = 10;
    private int intrusionLimit = 10;



    void Start()
    {
        //instatiate the wave manager & the tower manager

        this.setupWallet();
        this.setupTowers();
        
        this.terrain = Instantiate(terrainManager) as GameObject;

        this.createNewWave();

        intrusionGUI = GameObject.Find("intrusion");

        this.stat = status.running;

    }

    void Update()
    {
        if(this.stat == status.running){
            checkWaveStatus();

            //update gui for intruders
            intrusionGUI.GetComponent<Text>().text = ("Intruders: " + this.intrusionLimit);

            //check if too many troops passed 
            if (this.intrusionLimit <= 0)
            {
                Debug.Log("GAME OVER");
                this.stat = status.finished;
            }
        }

        if (this.stat == status.finished)
        {
            this.towers.GetComponent<towerManager>().pause();
        }

    }

    public void troopIntrusion()
    {
        this.intrusionLimit--;
    }


    private void checkWaveStatus()
    {
        if(this.wave.GetComponent<waveManager>().getDoneStat() == true)
        {
            Object.Destroy(this.wave);
            Debug.Log("This wave is Done!!");
            createNewWave();
        }
    }

    private void createNewWave()
    {
        //create a new wave
        this.wave = Instantiate(waveManager) as GameObject;
        //send wallet to the new wave
        this.wave.GetComponent<waveManager>().setWallet(this.wallet);
        //send the current wave to the tower manager
        this.towers.GetComponent<towerManager>().setWave(this.wave);
        //start new wave
        this.wave.GetComponent<waveManager>().startWave(troopsPerWave);

    }

    private void setupWallet()
    {
        //instatiate the wallet
        this.wallet = Instantiate(levelWallet) as GameObject;
        //make initial deposit
        this.wallet.GetComponent<levelCashManager>().deposit(initialCash);
    }

    private void setupTowers()
    {
        //instatiate the towermanager
        this.towers = Instantiate(towerManager) as GameObject;
        //assign the wallet to the tower manager
        this.towers.GetComponent<towerManager>().setWallet(this.wallet);

    }






}
