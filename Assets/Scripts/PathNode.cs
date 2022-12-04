using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public TileType tile;
    public int x;
    public int y;

    public Car car;
    public ParkingLot parkingLot;

    public int gCost; // mevcut konumdan başlangıç konumuna olan maaliyet
    public int hCost; // mevcut konumdan hedefe konuma kalan maaliyet
    public int fCost; // h + g

    public PathNode cameFromNode;
    
    // public PathNode(Grid grid, int x, int y)
    // {
    //     _grid = grid;
    //     this.x = x;
    //     this.y = y;
    // }
    
    public PathNode(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public PathNode(int x, int y, Car car)
    {
        this.x = x;
        this.y = y;
        this.car = car;
        tile = TileType.Car;
    }
    
    public PathNode(int x, int y, ParkingLot parkingLot)
    {
        this.x = x;
        this.y = y;
        this.parkingLot = parkingLot;
        tile = TileType.ParkingLot;
    }
    
    
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    
    public override string ToString()
    {
        return x + " , " + y;
    }

}
