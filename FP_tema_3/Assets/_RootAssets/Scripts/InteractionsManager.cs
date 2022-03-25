using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsManager : MonoBehaviour
{

    public ZoneType PositionZone { get; private set; }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("UpZone"))
        {
            PositionZone = ZoneType.UpZone;
            print("UpZone");
        }
        
        if (other.CompareTag("DownZone"))
        {
            PositionZone = ZoneType.DownZone;
            print("DownZone");
        }
        
        if (other.CompareTag("LeftZone"))
        {
            PositionZone = ZoneType.LeftZone;
            print("LeftZone");
        }
        
        if (other.CompareTag("RightZone"))
        {
            PositionZone = ZoneType.RightZone;
            print("RightZone");
        }
    }
}


[System.Serializable]
public enum ZoneType
{
    UpZone,
    DownZone,
    LeftZone,
    RightZone
}
