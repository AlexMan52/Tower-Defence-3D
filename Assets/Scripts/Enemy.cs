using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathVFX;
    [SerializeField] GameObject enemySelfDestructionVFX;
    [SerializeField] GameObject enemyDamageVFX;
    [SerializeField] float scorePerEnemy = 200f;
    [SerializeField] float startingHealth = 100f;
    float currentHealth;
    float damage;

    [SerializeField] Image healthBar;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        damage = FindObjectOfType<Tower>().damage;
        GameObject damageVFXInstance = Instantiate(enemyDamageVFX, transform.position, Quaternion.identity);
        float destroyDelay = damageVFXInstance.GetComponentInChildren<ParticleSystem>().main.duration; //можно взять время для уничтожения объекта частиц из самого блока частиц
        Destroy(damageVFXInstance, destroyDelay);
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / startingHealth;
        if (currentHealth <= 0)
        {
            KillEnemy(enemyDeathVFX);
        }
    }

    public void KillEnemy(GameObject deathVFX)
    {
        GameObject deathVFXInstance = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(deathVFXInstance, 2);
        Destroy(gameObject); 
        FindObjectOfType<LevelController>().AddToScore(scorePerEnemy);
    }

    private void OnTriggerEnter(Collider other) // использую при столкновении с базой игрока
    {
        KillEnemy(enemySelfDestructionVFX);
    }
}
