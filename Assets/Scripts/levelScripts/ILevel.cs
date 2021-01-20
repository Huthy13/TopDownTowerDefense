using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevel
{
    int getInitialCash();
    int getTroopsPerWave();
    int getIntrusionLimit();
    int[,] getMapTemplate();




}
