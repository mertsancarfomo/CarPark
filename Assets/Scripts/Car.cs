using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarDirection
{
    Right,
    Left,
    Up,
    Down
}

public class Car 
{
    public int startX;
    public int startY;
    public int targetX;
    public int targetY;

    public CarDirection carDirection;

    public Color color;
    
    public Car(int startX, int startY, ParkingLot parkingLot, CarDirection carDirection = CarDirection.Up)
    {
        this.startX = startX;
        this.startY = startY;
        targetX = parkingLot.x;
        targetY = parkingLot.y;
        
        this.carDirection = carDirection;

        color = parkingLot.color;
    }

}
