using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerBase : MonoBehaviour
{

    //publicly adjustable vars
    public int cost;                 //how much the tower costs to build
    public float range;              //how far is the range of the wepon
    public float chargeTime;         //how often a shell can be shot
    public float damageDelt;         //how much damage a shell deals
    public GameObject round;         //bullet/round gameobject

    //private vars needed for core functionality
    private bool charged;            //status of gun if avaiable to shoot
    private float chargeTimer;       //timer to track game timer
    private GameObject enemyUnits;   //holding spot for enemy units 
    private List<GameObject> waveOfUnits;

    //test vars
    private int roundCount = 0;     //var just for troubleshooting


    // Start is called before the first frame update
    void Start()
    {
        charged = false;         //set charge status to true so that it can fire right away
    }

    //called from outside to update the incoming enemys
    public void setenemyUnits(List<GameObject> enemyUnits)
    {
        this.waveOfUnits = enemyUnits;
    }

    // Update is called once per frame
    void Update()
    {
        //Update charge status
        updateCharge();

        GameObject target = getTarget();

        //if wepon is charged fire at a cose enemy
        if (target != null)
        {

            //rotate turret
            Vector3 v_diff;
            float atan2;
            v_diff = (target.transform.position - this.transform.position);
            atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
            this.transform.rotation = Quaternion.Euler(0f, 0f, (atan2 - 67.55f) * Mathf.Rad2Deg );  //the 90 degrees is just the current resting spot, may want to change that


            if (charged)
            {
                fireGun(target);
               
                //testing print
                roundCount++;
                //Debug.Log("Fire tank close!!        Round: " + roundCount);
            }
   
        }

        //check if enough time has passed to be able to fire again
        if (Time.time > chargeTimer)
        {
            charged = true;
        }
 
    }

    private void fireGun(GameObject target)
    {

        //instatiate round
        GameObject newRound = Instantiate(round) as GameObject;
        //set rounds starting pos and target
        newRound.GetComponent<roundScript>().setStartingPoint(this.transform);
        newRound.GetComponent<roundScript>().setTarget(target, this.damageDelt);

        chargeTimer = Time.time + chargeTime;       //reset charge time timer
        charged = false;                            //change status becuase gun was fired

    }

    private bool updateCharge()
    {
        if (Time.time > chargeTimer) return true;
   
        else return false;
    }

    private GameObject getTarget()
    {
        //loop through troops
        for (int i = 0; i < this.waveOfUnits.Count; i++)
        {
            //check if unit is dead
            if (waveOfUnits[i].GetComponent<tankScript>().isTargetable())
            {
                //check if unit is within shooting range
                if (Vector3.Distance(waveOfUnits[i].transform.position, this.transform.position) < this.range)
                {
                    //return unit
                    return waveOfUnits[i];
                }
                else continue;
            }
            else continue;
        }
        return null;
    }


}
