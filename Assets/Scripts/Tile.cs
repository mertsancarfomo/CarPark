using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public enum TileType{
    Way,
    Car,
    ParkingLot,
    Empty
}

public class Tile : MonoBehaviour
{
    public TileType tileType = TileType.Way;

    
}
