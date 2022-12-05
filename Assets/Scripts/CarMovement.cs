using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    
        public static void MoveCartoParkinglot(Car car, Transform currentParent, Transform newParent, Vector3[] path)
        {
            GameObject carObject = currentParent.GetChild(1).gameObject;
            float carSpeed = path.Length * 0.125f;
            carObject.transform.DOPath(path, carSpeed, PathType.Linear, PathMode.TopDown2D).SetEase(Ease.Linear).OnComplete(() => SetCarPositiontoParkinglot(carObject.transform, newParent))
                .OnWaypointChange((positionIndex) => SetNewCarRotation(car, carObject.transform, path, positionIndex));
            
            DOTween.Play(carObject.transform);
        }
        
        public static void MoveCar(Car car, Transform currentParent, Transform newParent, Vector3[] path)
        {
            GameObject carObject = currentParent.GetChild(1).gameObject;
            float carSpeed = path.Length * 0.125f;
            carObject.transform.DOPath(path, carSpeed, PathType.Linear, PathMode.TopDown2D).SetEase(Ease.Linear).OnComplete(() => SetNewCarPosition(carObject.transform, newParent))
                .OnWaypointChange((positionIndex) => SetNewCarRotation(car, carObject.transform, path, positionIndex));
            
            DOTween.Play(carObject.transform);
        }

        private static void SetNewCarPosition(Transform carTransform, Transform newParent)
        {
            carTransform.SetParent(newParent, false);
            carTransform.localPosition = new Vector3(0, 1, 0);
        }
        
        private static void SetCarPositiontoParkinglot(Transform carTransform, Transform newParent)
        {
            carTransform.SetParent(newParent, false);
            carTransform.localPosition = new Vector3(0, 1, 0);
            
            Destroy(newParent.transform.GetComponent<BoxCollider>());

            LevelManager.UpdateCarCounter();
        }

        private static void SetNewCarRotation(Car car, Transform carTransform, Vector3[] path, int positionIndex)
        {
            if (path.Length-1 == positionIndex)
            {
                return;
            }
            
            if (path[positionIndex].z < path[positionIndex+1].z)
            {
                carTransform.rotation = Quaternion.Euler(-90,0,0f);
                car.carDirection = CarDirection.Up;
            }
            else if (path[positionIndex].z > path[positionIndex+1].z)
            {
                carTransform.rotation = Quaternion.Euler(-90,0,180f);
                car.carDirection = CarDirection.Down;
            }
            else if (path[positionIndex].x < path[positionIndex+1].x)
            {
                carTransform.rotation = Quaternion.Euler(-90,0,90f);
                car.carDirection = CarDirection.Right;
            }
            else if (path[positionIndex].x > path[positionIndex+1].x)
            {
                carTransform.rotation = Quaternion.Euler(-90,0,-90f);
                car.carDirection = CarDirection.Left;
            }
        }
        

}
