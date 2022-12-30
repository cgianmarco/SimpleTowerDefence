using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;




public class Turret : Unit
{
    
    public float range = 10f;


    private void Start()
    {
        GetComponent<Aim>().OnTargetAcquired += () => GetComponent<Shooter>()?.StartShooting();
        GetComponent<Aim>().OnTargetLost += () => GetComponent<Shooter>()?.StopShooting();
    }
    

}
