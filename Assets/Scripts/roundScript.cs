using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundScript : MonoBehaviour
{

    public float speed;
    private float damageDelt;
    private bool isActive = false;
    private GameObject target;

    private Transform targetPos;


    public void setStartingPoint(Transform startPos)
    {
        this.transform.position = (startPos.position + new Vector3(0f,0f,0.5f)); //adding to the z to set spawn behind the tower
    }

    public void setTarget(GameObject target, float damageDelt)
    {
        this.target = target;
        this.damageDelt = damageDelt;
        this.isActive = true;

    }

    private void setAngle()
    {
        Vector3 v_diff;
        float atan2;
        v_diff = (this.target.transform.position - this.transform.position);
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        this.transform.rotation = Quaternion.Euler(0f, 0f, (atan2+67.55f) * Mathf.Rad2Deg); 

    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //check if target is still alive
            if (this.target.GetComponent<tankScript>().getLifeStat())
            {
                transform.position = Vector3.MoveTowards(transform.position, target.GetComponent<Transform>().position, Time.deltaTime * this.speed);
                checkForImpact();
                this.setAngle();
            }
            //if target is gone destroy bullet
            else
            {
                Object.Destroy(this.gameObject);
            }
        }
    }

    void checkForImpact()
    {
        if (Vector3.Distance(this.target.GetComponent<Transform>().position, this.transform.position) < .001f)
        {
            isActive = false;
            Object.Destroy(this.gameObject);
            //this is what deals damage to the object
            target.GetComponent<tankScript>().hit(damageDelt);
        }
    }
}
