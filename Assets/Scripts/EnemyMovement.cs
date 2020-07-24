using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float timeToMove = 1f; //задаем скорость перемещения объекта по Пути
    List<Waypoint> path; //задаем ссылку на Путь, делаем доступной для всех функций в данном классе
    Transform target; //для поворота в сторону след вэйпойнта
    
    int targetIndex;
    void Start()
    {
        path = FindObjectOfType<Pathfinder>().GetPath(); //задаем значение Пути через ссылку на класс Pathfinder
        StartCoroutine(FollowPath(path)); // запускаем корутину по перемещению объекта
        target = path[0].transform;
    }

    IEnumerator FollowPath(List<Waypoint> path) //корутина по перемещению объекта по определенному пути, вставляемому при вызове
    {
        foreach (var pathElem in path)
        {
            transform.position = pathElem.transform.position;
            if (targetIndex < path.Count-1)
            {
                targetIndex++;
                target = path[targetIndex].transform;
                transform.LookAt(target);
            }
            
            yield return new WaitForSecondsRealtime(timeToMove);
        }
    }
}
