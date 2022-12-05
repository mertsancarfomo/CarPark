using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs instance;
    
    public static GameObject carPrefab;
    //public List<GameObject> carPrefabs;
    public static GameObject wayPrefab;
    public static GameObject parkinglotPrefab;


    void Awake()
    {
        instance = this;
        
        // 2D
        // carPrefab = gameObject.transform.Find("Car").gameObject;
        // wayPrefab = gameObject.transform.Find("Way").gameObject;
        // parkinglotPrefab = gameObject.transform.Find("Parkinglot").gameObject;
 
        // 3D
        // for (int i = 1; i <= 10; i++)
        // {
        //     carPrefabs.Add(gameObject.transform.Find("car" + i).gameObject);
        // }
        carPrefab = gameObject.transform.Find("car1").gameObject;
        wayPrefab = gameObject.transform.Find("Way").gameObject;
        parkinglotPrefab = gameObject.transform.Find("Parkinglot").gameObject;

        gameObject.SetActive(false);
    }
    
}
