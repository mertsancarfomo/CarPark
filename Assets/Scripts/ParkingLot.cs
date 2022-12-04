using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingLot
{
    public int x;
    public int y;

    public Color color;
    
    public ParkingLot(int x, int y, Color color)
    {
        this.x = x;
        this.y = y;

        this.color = color;
    }
}
