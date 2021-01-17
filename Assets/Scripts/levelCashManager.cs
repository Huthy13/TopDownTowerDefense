using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelCashManager : MonoBehaviour
{

    private int levelCash = 0;
    GameObject cashUI;


    
    public void deposit(int deposit)
    {
        this.levelCash += deposit;
    }
    public void withdrawl(int withdrawl)
    {
        this.levelCash -= withdrawl;
    }

    private void Start()
    {
        cashUI = GameObject.Find("cashUI");
    }

    void Update()
    {
        cashUI.GetComponent<Text>().text = ("$ " + this.getBalance());
    }

    public int getBalance()
    {
        return this.levelCash;
    }
}
