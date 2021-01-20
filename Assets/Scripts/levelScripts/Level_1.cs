using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1: MonoBehaviour, ILevel
{
    //basic level variables
    private int initialCash = 100;
    private int troopsPerWave = 10;
    private int intrusionLimit = 10;

    //map layout
    private int[,] mapTemplate =   {{001,001,001,001,001,103,001,002,000,000,000,000,000,000,000},
                                    {001,001,001,001,001,103,001,002,000,000,000,000,000,000,000},
                                    {001,001,001,001,001,112,102,200,100,100,100,110,000,000,000},
                                    {001,001,001,001,001,001,001,002,000,000,000,101,000,000,000},
                                    {001,001,001,001,001,001,001,002,000,000,000,101,000,000,000},
                                    {001,001,113,102,102,102,102,200,100,100,100,111,000,000,000},
                                    {001,001,103,001,001,001,001,002,000,000,000,000,000,000,000},
                                    {001,001,103,001,001,001,001,002,000,000,000,000,000,000,000},
                                    };



    public int [,] getMapTemplate()
    {
        return this.mapTemplate;
    }

    public int getInitialCash()
    {
        return this.initialCash;
    }

    public int getTroopsPerWave()
    {
        return this.troopsPerWave;
    }

    public int getIntrusionLimit()
    {
        return this.intrusionLimit;
    }
}
