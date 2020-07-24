using System;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject objectToPan;
    Enemy targetEnemy;
    [Tooltip("NumOfCubes for shooting range")] [SerializeField] int xRange = 2;
    [Tooltip("NumOfCubes for shooting range")] [SerializeField] int yRange = 2;
    public int damage = 33;
    float gridSize;
    float shootingRange;
    float distanceToClosestEnemySquared;

    
    AudioSource shootingSound;

    public TowerWaypoint baseWaypoint;

    private void Start()
    {
        SetShootingRange();
        shootingSound = GetComponentInChildren<AudioSource>();
        
    }

    private void SetShootingRange()
    {
        var waypoint = FindObjectOfType<Waypoint>();
        gridSize = waypoint.GetGridSize();
        // !!! Для 1 варианта из Shooting: shootingRange = new Vector2(xRange * gridSize, yRange * gridSize).Magnitude;
        //Для 2 варианта берем квадрат shootingRange, (потому что быстрее обработка в Unity!):
        shootingRange = new Vector2(xRange * gridSize, yRange * gridSize).sqrMagnitude;
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
        var enemiesOnField = FindObjectsOfType<Enemy>(); 
        if (enemiesOnField.Length == 0) { return; } //если нет врагов, то нет цели

        Enemy closestEnemy = enemiesOnField[0]; //по умолчанию ставим первого врага как ближайшего

        foreach (Enemy testEnemy in enemiesOnField) //проводим проверку на расстояние
        {
            closestEnemy = GetClosestEnemy(closestEnemy, testEnemy); //получаем ближайшего врага из GetClosestEnemy
        }
        targetEnemy = closestEnemy;
    }

    private Enemy GetClosestEnemy(Enemy closestEnemy, Enemy testEnemy) //передаем ближайшего врага и тестируемого из массива
    {
        if ((gameObject.transform.position - testEnemy.transform.position).sqrMagnitude <  //если до тестируемого расстояние меньше, чем до ближайшего
            (gameObject.transform.position - closestEnemy.transform.position).sqrMagnitude)
        {
            closestEnemy = testEnemy;
        }
        return closestEnemy;
    }

    void LookAtTarget()
    {
        objectToPan.transform.LookAt(targetEnemy.transform); //крутим пушку башни на врага
    }
    void FireAtEnemy()
    {
        /* !!! Первый вариант расчета дистанции до цели, но он дольше выполняется из-за расчета корня, 
        лучше брать sqr.magnitude если просто сравниваем расстояния!

        Vector2 distanceVector = new Vector2 (targetEnemy.position.x - objectToPan.position.x, targetEnemy.position.z - objectToPan.position.z);
        float distanceFloat = distanceVector.magnitude;*/


        // !!! Второй вариант расчета дистанции до цели, с sqr.magnitude

        Vector2 distanceVector = new Vector2(targetEnemy.transform.position.x - gameObject.transform.position.x, // вектор расстояния до цели
                                            targetEnemy.transform.position.z - gameObject.transform.position.z);
        distanceToClosestEnemySquared = distanceVector.sqrMagnitude; //значение отрезка до цели
        if (distanceToClosestEnemySquared <= shootingRange) //проверка что расстояние до цели входит в область поражения
        {
            Shooting(true);
        }
        else
        {
            Shooting(false);
        }
    }

    void Shooting(bool isActive) //сама стрельба, с выключением при уничтожении цели
    {
        var bulletEmission = GetComponentInChildren<ParticleSystem>().emission;
        bulletEmission.enabled = isActive;
        shootingSound.enabled = isActive;
    }

}
