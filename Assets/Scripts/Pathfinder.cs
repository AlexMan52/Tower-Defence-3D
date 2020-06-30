using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, finishWaypoint; //задаем старт и финиш Пути
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>(); //создаем Словарь (массив) с координатами вэйпойнтов (для быстрого поиска вэйпойнтов по координатам)
    Queue<Waypoint> queue = new Queue<Waypoint>(); //создаем Очередь (массив), потом добавим сюда соседние клетки при поиске пути
    bool isRunning = true; //задаем условие для выполнения цикла
    Waypoint currentSearchCenter; //создаем пустую переменную для текущего центра для поиска по соседним клеткам
    List<Waypoint> path = new List<Waypoint>(); //создаем массив для пути

    Vector2Int[] directions = {  // создаем массив с направлениями для поиска пути
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    public List<Waypoint> GetPath() //getter для Пути, чтобы передать путь объекту - вызывается в EnemyMovement
    {
        LoadBlocks(); //загрузка всех вэйпойнтов
        BreadthFirstSearch(); //выполняем поиск пути
        CreatePath(); //создаем Путь
        return path; //передаем найденный Путь
    }
    private void LoadBlocks() // добавляем в список вэйпойнты с проверкой, не накладываются ли они друг на друга
    {
        var waypoints = FindObjectsOfType<Waypoint>(); //находим все вэйпойнты и создаем из них массив
        foreach (Waypoint waypoint in waypoints) //для каждого вэйпойнта из массива:
        {
            var waypointPos = waypoint.GetWaypointPos(); //получаем координаты позиции
            if (grid.ContainsKey(waypointPos)) // если в Словаре уже есть вэйпойнт с такими координатами, то ничего не загружаем (чтобы в Словаре не дублировались случайно созданные в одном месте кубы)
            {
                Debug.LogWarning("Is overlapping"); 
            }
            else //если нет, то добавляем вэйпойнт в Словарь
            {
                grid.Add(waypointPos, waypoint);
            }
        }
    }
    private void BreadthFirstSearch() //алгоритм поиска пути
    {
        queue.Enqueue(startWaypoint); // добавляем в Очередь стартовую точку
        startWaypoint.SetTopColor(Color.yellow); //даем цвет стартовой точке
        while (queue.Count > 0 && isRunning) //пока в Очереди что-то есть и выполняется булевое значение 
        {
            currentSearchCenter = queue.Dequeue(); //передаем значение центру поиска равное тому, что мы убираем из очереди (т.е. стартовой точки)
            //print("Searching from: " + currentSearchCenter);
            HaltIfFoundEnd(); // останавливаем поиск, если нашли финиш
            currentSearchCenter.isExplored = true; //ставим метку, что клетка проверена, чтобы не проверять дважды из стартовой точки
            ExploreNeighbours(); //исследуем соседние клетки
        }
    }
    private void HaltIfFoundEnd() //останавливаем, если нашли финиш
    {
        if (currentSearchCenter == finishWaypoint) //проверяем, что текущий центр поиска равен финишной точке
        {
            finishWaypoint.SetTopColor(Color.red); //даем цвет финишной точке
            isRunning = false; //меняем булевое значение, чтобы предотвратить выполнение ExploreNeighbours из цикла while в BreadthFirstSearch
        }
    }
    private void ExploreNeighbours() // алгоритм для добавления в очередь соседних клеток (каждая потом становится currentSearchCenter)
    {
        if (!isRunning) { return; } //проверка, что не нашли финиш, если нашли то не выполняем дальше
        foreach (Vector2Int direction in directions) //для каждого направления
        {
            Vector2Int neighbourPos = currentSearchCenter.GetWaypointPos() + direction; //получаем значения координат всех соседних клеток поочереди
            if (grid.ContainsKey(neighbourPos)) //если в Словаре есть вэйпойнты с такими координатами, то
            {
                QueueNewNeighbour(neighbourPos); //запускаем метод по добавлению в очередь на проверку соседней клетки (с передаваемыми координатами)
            }
        }
    }
    private void QueueNewNeighbour(Vector2Int neighbourPos) // метод по добавлению в очередь на проверку клетки (координаты берём из ExploreNeighbours)
    {
        Waypoint neighbour = grid[neighbourPos]; //создаем объект, присваиваем координаты из Словаря (берем координаты при вызове)
        
        if (!neighbour.isExplored) // проверка, что клетка еще не добавлена в очередь
        {
            //print("added to queue: " + neighbour);
            queue.Enqueue(neighbour); //добавляем в очередь на исследование
            neighbour.isExplored = true; //присваиваем статус, что уже добавлена в очередь, чтобы не дублировалось
            neighbour.exploredFrom = currentSearchCenter; //присваиваем клетку, с которой нашли данного "соседа"
        }
    }
    private void CreatePath() // создаем динамический массив с вэйпойнтами от финиша до старта, затем разворачиваем и получаем путь
    {
        path.Add(finishWaypoint); // добавляем финишную точку в Путь
        Waypoint previous = finishWaypoint.exploredFrom; //создаем переменную, которая показывает предыдущий объект при поиске пути 
        while (previous != startWaypoint) //пока предыдущий объект не равен стартовой точке
        {
            path.Add(previous); //добавляем в Путь предыдущий объект
            previous = previous.exploredFrom; //ищем предыдущий объект для уже добавленного в Путь
        }
        path.Add(startWaypoint); // добавляем стартовый вэйпойнт
        path.Reverse(); //разворачиваем массив
    }

   
   

}
