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
    public static Dictionary<int, string> carColors = new Dictionary<int, string>();
    
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
    
    public static void SetCarColors()
    {
        TextAsset txt = (TextAsset)Resources.Load("Other/carColors", typeof(TextAsset));
        string[] colors = txt.text.Split("\n");
        
        for (int i = 0; i < colors.Length; i++)
        {
            try
            {
                carColors.Add(i, colors[i].Replace("\r", ""));
            }
            catch
            {
                continue;
            }
        }

    }

}
