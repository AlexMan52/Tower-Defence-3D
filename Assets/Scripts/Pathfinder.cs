using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, finishWaypoint;
    

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>(); //создаем словарь с координатами вэйпойнтов
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;

    Waypoint currentSearchCenter;

    Vector2Int[] directions = {  // создаем массив с направлениями (для поиска пути)
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        Pathfind();
    }

    private void LoadBlocks() // добавляем в список вэйпойнты с проверкой, не накладываются ли они друг на друга
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var waypointPos = waypoint.GetWaypointPos();
            if (grid.ContainsKey(waypointPos))
            {
                Debug.LogWarning("Is overlapping");
            }
            else
            {
                grid.Add(waypointPos, waypoint);
            }
        }
    }
    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);
        startWaypoint.SetTopColor(Color.yellow);
        while (queue.Count > 0 && isRunning)
        {
            currentSearchCenter = queue.Dequeue();
            print("Searching from: " + currentSearchCenter);
            HaltIfFoundEnd();
            currentSearchCenter.isExplored = true;
            ExploreNeighbours();
        }
    }
    private void HaltIfFoundEnd()
    {
        if (currentSearchCenter == finishWaypoint)
        {
            print("Found finish: " + finishWaypoint);
            finishWaypoint.SetTopColor(Color.red);
            isRunning = false;
        }
    }
    private void ExploreNeighbours() // алгоритм для поиска пути
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourPos = currentSearchCenter.GetWaypointPos() + direction;
            try //убираем ошибку в консоли, если у начального вэйпойнта нет соседа с какой-либо стороны
            {
                QueueNewNighbour(neighbourPos);

            }
            catch //тут прописывают что делать при ошибке, мы ничего не делаем
            {
                //do nothing
            }
        }
    }
    private void QueueNewNighbour(Vector2Int neighbourPos)
    {
        Waypoint neighbour = grid[neighbourPos];
        if (!neighbour.isExplored)
        {
            queue.Enqueue(neighbour);
            print("Added to queue:" + neighbour);
            neighbour.isExplored = true;
            neighbour.exploredFrom = currentSearchCenter;
        }
    }

}
