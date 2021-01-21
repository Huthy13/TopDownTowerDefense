using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerManager : MonoBehaviour
{

    public enum status {starting, running, paused, finished };
    status stat = status.running;

    public GameObject tower;
    private GameObject wave;
    private GameObject wallet;

    GameObject newTower;

    private List<GameObject> towers = new List<GameObject>();


    // Update is called once per frame
    void Update()
    {

        if (this.stat == status.running)
        {

            if (Input.GetMouseButtonDown(0))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (this.tower.GetComponent<towerBase>().cost <= this.wallet.GetComponent<levelCashManager>().getBalance())
                    {
                        placeTower(hit.transform.gameObject.transform);
                        this.wallet.GetComponent<levelCashManager>().withdrawl(this.tower.GetComponent<towerBase>().cost);
                    }
                    else
                    {
                        Debug.Log("not enough cash");
                    }
                }
            }
        }
    }

    public void pause()
    {
        this.stat = status.paused;
    }

    public void resume()
    {
        this.stat = status.running;
    }


    public void setWave(GameObject wave)
    {
        this.wave = wave;
        this.setWaveExistingTowers();
    }

    public void setWallet(GameObject wallet)
    {
        this.wallet = wallet;
    }
    
    public void setWaveExistingTowers()
    {
        foreach(GameObject tower in towers){
            tower.GetComponent<towerBase>().setenemyUnits(this.wave.GetComponent<waveManager>().getWave());
        }

    }


    private void placeTower(Transform newObjPos)
    {  
        //instantiate new nower
        GameObject newTower1 = Instantiate(tower) as GameObject;
        //add the tower to the towers list
        towers.Add(newTower1);
        //set the new towers position
        newTower1.GetComponent<Transform>().position = (newObjPos.position + new Vector3(0f, 0f, -1.0f));
        //assign the current wave to the tower
        newTower1.GetComponent<towerBase>().setenemyUnits(this.wave.GetComponent<waveManager>().getWave());
    }
}
