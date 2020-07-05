using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Drawing;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathVFX;
    [SerializeField] GameObject enemyDamageVFX;
    //[SerializeField] Transform parent; //спользовалось для внесения появляющихся врагов под родительский объект, теперь они в Волнах

    [SerializeField] int scorePerEnemy = 200;
    [SerializeField] int healthPoints = 100;
    int damage;

    private void Start()
    {
        damage = FindObjectOfType<Tower>().damage;
    }
    private void OnParticleCollision(GameObject other)
    {
        GameObject damageVFXInstance = Instantiate(enemyDamageVFX, transform.position, Quaternion.identity);
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject deathVFXInstance = Instantiate(enemyDeathVFX, transform.position, Quaternion.identity);
        //deathVFXInstance.transform.parent = parent; // вносим создаваемые объекты под 1 родительский объект в иерархии
        Destroy(gameObject); // уничтожаем вражеский корабль
        //FindObjectOfType<ScoreBoard>().AddToScore(scorePerEnemy);
    }
}
