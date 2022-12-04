using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CodeMonkey.Utils {
    
    public class UtilsClass : MonoBehaviour
    {

        public static void CreateTile(string gridPosition, PathNode node, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000) {
            CreateTile(parent, node, gridPosition, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }
        
        public static void CreateTile(Transform parent, PathNode node, string gridPosition, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
        {

            GameObject gameObject = new GameObject(gridPosition);
            
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;

            //2D
            // gameObject.AddComponent<BoxCollider2D>();
            // BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
            // boxCollider2D.isTrigger = true;
            // boxCollider2D.size = new Vector2(10f, 10f);
            // boxCollider2D.offset = new Vector2(0f, 0f);

            //3D
            gameObject.AddComponent<BoxCollider>();
            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            boxCollider.size = new Vector3(4f, 2f, 4f);
            boxCollider.center = new Vector3(0f, 0.6f, 0f);
            

            if (node.tile == TileType.Way)
            {
                GameObject wayObject =  Instantiate(Prefabs.wayPrefab);
                wayObject.SetActive(true);
                Transform wayObjectTransform = wayObject.transform;
                wayObjectTransform.SetParent(gameObject.transform, false);
                wayObjectTransform.localPosition = new Vector3(0, 0, 0);
                
                ColorUtility.TryParseHtmlString("#E7E6E6", out var wayColor);
                wayObjectTransform.GetComponent<SpriteRenderer>().color = wayColor;
            }

            else if (node.tile == TileType.Car)
            {
                
                GameObject wayObject =  Instantiate(Prefabs.wayPrefab);
                wayObject.SetActive(true);
                Transform wayObjectTransform = wayObject.transform;
                wayObjectTransform.SetParent(gameObject.transform, false);
                wayObjectTransform.localPosition = new Vector3(0, 0, 0);
                
                ColorUtility.TryParseHtmlString("#E7E6E6", out var wayColor);
                wayObjectTransform.GetComponent<SpriteRenderer>().color = wayColor;
                
                GameObject carObject =  Instantiate(Prefabs.instance.carPrefabs[0]);
                carObject.SetActive(true);
                Transform carObjectTransform = carObject.transform;
                carObjectTransform.SetParent(gameObject.transform, false);
                // carObjectTransform.localPosition = new Vector3(0, 0, 0); 2D 
                carObjectTransform.localPosition = new Vector3(0, 0.5f, 0); //3D
                carObjectTransform.localRotation =  Quaternion.Euler(-90,0,0);//3D
                // carObjectTransform.GetComponent<SpriteRenderer>().color = node.car.color;
                

                if (node.car.carDirection == CarDirection.Up)
                {
                    carObjectTransform.localRotation = Quaternion.Euler(-90,0,0);
                }
                else if (node.car.carDirection == CarDirection.Down)
                {
                    carObjectTransform.localRotation = Quaternion.Euler(-90,0,180f);
                }
                else if (node.car.carDirection == CarDirection.Right)
                {
                    carObjectTransform.localRotation = Quaternion.Euler(-90,0,-90f);
                }
                else if (node.car.carDirection == CarDirection.Left)
                {
                    carObjectTransform.localRotation = Quaternion.Euler(-90,0,90f);
                }
            }
            
            else if (node.tile == TileType.ParkingLot)
            {
                GameObject wayObject =  Instantiate(Prefabs.wayPrefab); //Prefabs.parkinglotPrefab TODO: fix later
                wayObject.SetActive(true);
                Transform wayObjectTransform = wayObject.transform;
                wayObjectTransform.SetParent(gameObject.transform, false);
                wayObjectTransform.localPosition = new Vector3(0, 0, 0);

                // Transform parkingText = wayObject.transform.GetChild(0);
                // parkingText.GetComponent<SpriteRenderer>().color = node.parkingLot.color;
            }
            
        }
                
        
    }


}