using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankScript : MonoBehaviour
{
    //set up state machine
    public enum status {waiting,alive,paused,dead,finished};
    status stat = status.waiting;

    public float health;        //how much health does the troop have
    public float speed;         //lower number slower
    public int killReward;    //cash recieved for killing troop

    private Vector3 target;

    private int pathPos = 0;
    private float[,] path;

    private GameObject wallet;


    public void activateTroop(float[,] path)
    {
        this.path = path;

        transform.position = new Vector3(path[pathPos, 0], path[pathPos, 1],-0.01f);          //set starting set point of as first Waypoint
        pathPos++;                                                                     //increment waypoint pointer
        target = new Vector3(path[pathPos, 0], path[pathPos, 1], -0.01f);                 //set target to the second waypoint

        this.stat = status.alive;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the unit is alive
        if (this.stat == status.alive)
        {
            moveTroop();        //step to next waypoint
            checkHealth();      //check if we have health
        }
    }

    private void moveTroop()
    {
        //move towards target (next waypoint)
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * this.speed);

        //check if we are close to the target, if so setup for the next waypoint
        if (Vector3.Distance(this.target, this.transform.position) < .001f)
        {
            setNextWayPoint();
        }
    }

    private void setNextWayPoint()
    {
        //adjust position pointer
        if (this.pathPos < (this.path.Length / 2) - 1) //lenght counts all of the elements 
        {
            this.pathPos++;
        }
        else
        {
            this.stat = status.finished;
            reachedGoal();
        }

        //set the next waypoint as target
        this.target = new Vector3(path[pathPos, 0], path[pathPos, 1], -1f);

        //rotate troop to direction of road
        this.setAngle();
    }

    public void reachedGoal()
    {
        var fieldSetup = GameObject.Find("Main Camera");
        fieldSetup.GetComponent<fieldSetup>().troopIntrusion();
    }

    public void hit(float damage)
    {
        this.health = this.health - damage;
        //Debug.Log("unit health = " + this.health);
    }

    private void checkHealth()
    {
        if (this.health <= 0)
        {
            this.stat = status.dead;
            this.transform.position = new Vector3(0f, 0f, 1f);
            this.wallet.GetComponent<levelCashManager>().deposit(this.killReward);
            //Debug.Log("unit is dead");
        }
    }

    public bool getLifeStat()
    {
        if (this.stat == status.waiting || this.stat == status.alive || this.stat == status.paused)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isTargetable()
    {
        if (this.stat == status.alive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void setAngle()
    {
        Vector3 v_diff;
        float atan2;
        v_diff = (this.target - this.transform.position);
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        this.transform.rotation = Quaternion.Euler(0f, 0f, (atan2 + 67.55f) * Mathf.Rad2Deg);
    }

    public void setWallet(GameObject wallet)
    {
        this.wallet = wallet;
    }
}
