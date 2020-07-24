using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] float startHealth = 10;
    private float currentHealth;

    [Header("Unity Stuff")]
    [SerializeField] Image healthBar;

    private void Start()
    {
        currentHealth = startHealth;
    }
    void Update()
    {
        DestroyBase();
        healthBar.fillAmount = currentHealth / startHealth;
    }

    private void DestroyBase()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentHealth--;
    }

     

}
