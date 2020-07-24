using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 3;
    [SerializeField] Tower towerPrefab;
    Queue <Tower> towerQueue = new Queue <Tower>(); //для кольцевого списка

    public void AddTower(TowerWaypoint baseWaypoint) //вызывается из Waypoint и берем baseWaypoint, на который указывает мышь
    {
        int numTowers = towerQueue.Count;
        
        if (numTowers < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
        
    }
    private void InstantiateNewTower(TowerWaypoint baseWaypoint)
    {
        Tower newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        baseWaypoint.isPlaceable = false;
        newTower.baseWaypoint = baseWaypoint;

        towerQueue.Enqueue(newTower);
    }
    private void MoveExistingTower(TowerWaypoint newBaseWaypoint) //берем первую в очереди башню, меняем её положение и ставим в конец очереди (экономит память)
    {
        var oldTower = towerQueue.Dequeue();

        oldTower.baseWaypoint.isPlaceable = true; //возвращаем возможность ставить башни на то место, откуда убираем старую башню
        newBaseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;

        towerQueue.Enqueue(oldTower);
    }
}
