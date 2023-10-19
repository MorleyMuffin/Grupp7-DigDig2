using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float currentHealth;

    [SerializeField] bool canTakeDamage = true;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        if(canTakeDamage ==true)
        {
            currentHealth = currentHealth - damage;
        }
    }
}
