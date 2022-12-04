using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static GameObject carPrefab;
    public static GameObject wayPrefab;
    public static GameObject parkinglotPrefab;
    
    public List<GameObject> carPrefabs;
    
    public static Prefabs instance;

    void Awake()
    {
        instance = this;
        
        // 2D
        // carPrefab = gameObject.transform.Find("Car").gameObject;
        // wayPrefab = gameObject.transform.Find("Way").gameObject;
        // parkinglotPrefab = gameObject.transform.Find("Parkinglot").gameObject;
 
        // 3D
        for (int i = 1; i <= 9; i++)
        {
            carPrefabs.Add(gameObject.transform.Find("car" + i).gameObject);
        }
        wayPrefab = gameObject.transform.Find("Way").gameObject;
        //parkinglotPrefab = gameObject.transform.Find("Parkinglot").gameObject;       


        gameObject.SetActive(false);
        
        //carPrefab.SetActive(false);
        //wayPrefab.SetActive(false);
        //parkinglotPrefab.SetActive(false);
    }
    
}
