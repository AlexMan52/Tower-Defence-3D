using System;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject objectToPan;
    //[SerializeField] GameObject targetEnemy;
    GameObject targetEnemy;
    [Tooltip("NumOfCubes for shooting range")] [SerializeField] int xRange = 2;
    [Tooltip("NumOfCubes for shooting range")] [SerializeField] int yRange = 2;
    public int damage = 33;
    float gridSize;
    float shootingRange;
    bool isActive = false;
    float distanceToClosestEnemySquared;
    private void Start()
    {
        SetShootingRange();
    }

    private void SetShootingRange()
    {
        var waypoint = FindObjectOfType<Waypoint>();
        gridSize = waypoint.GetGridSize();
        // !!! Для 1 варианта из Shooting: shootingRange = new Vector2(xRange * gridSize, yRange * gridSize).Magnitude;
        //Для 2 варианта берем квадрат shootingRange:
        shootingRange = new Vector2(xRange * gridSize, yRange * gridSize).sqrMagnitude;
        print("Distance to shoot = " + shootingRange);
    }

    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            LookAtTarget();
            FireAtEnemy();
        }
        else
        {
            Shooting(false);
        }
        
    }

    void SetTargetEnemy() //находим ближайшего врага
    {
        var enemiesOnField = FindObjectsOfType<Enemy>(); //ищем всех врагов на уровне
        if (enemiesOnField.Length == 0) { return; } //если их нет, то нет цели

        GameObject closestEnemy = enemiesOnField[0].gameObject; //по умолчанию ставим первого врага как ближайшего

        foreach (Enemy testEnemy in enemiesOnField) //для каждого найденного врага в массиве проводим проверку на расстояние
        {
            closestEnemy = GetClosestEnemy(closestEnemy, testEnemy.gameObject); //получаем ближайшего врага из соседней функции
        }
        targetEnemy = closestEnemy; //ставим цель на ближайшего врага
    }

    private GameObject GetClosestEnemy(GameObject closestEnemy, GameObject testEnemy) //передаем ближайшего врага и тестируемого из массива
    {
        if ((gameObject.transform.position - testEnemy.transform.position).sqrMagnitude <  //если до тестируемого расстояние меньше, чем до ближайшего
            (gameObject.transform.position - closestEnemy.transform.position).sqrMagnitude)
        {
            closestEnemy = testEnemy; //делаем ближайшего врага равного тестируемому
        }
        return closestEnemy; // возвращаем значение ближайшего врага
    }


    void LookAtTarget()
    {
        objectToPan.transform.LookAt(targetEnemy.transform);
    }
    void FireAtEnemy()
    {
        /* !!! Первый вариант расчета дистанции до цели, но он дольше выполняется из-за расчета корня, 
        лучше брать sqr.magnitude если просто сравниваем расстояния!

        Vector2 distanceVector = new Vector2 (targetEnemy.position.x - objectToPan.position.x, targetEnemy.position.z - objectToPan.position.z);
        float distanceFloat = distanceVector.magnitude;*/


        // !!! Второй вариант расчета дистанции до цели, с sqr.magnitude

        Vector2 distanceVector = new Vector2(targetEnemy.transform.position.x - objectToPan.transform.position.x, // вектор расстояния до цели
                                            targetEnemy.transform.position.z - objectToPan.transform.position.z);
        float distanceToClosestEnemySquared = distanceVector.sqrMagnitude; //значение отрезка до цели
        if (distanceToClosestEnemySquared <= shootingRange) //проверка что расстояние до цели входит в область поражения
        {
            Shooting(true);
        }
        else
        {
            Shooting(false);
        }
    }

    void Shooting(bool isActive) //сама стрельба, чтобы выключить при уничтожении цели
    {
        var bulletEmission = GetComponentInChildren<ParticleSystem>().emission;
        bulletEmission.enabled = isActive;
    }


}
