using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyMaxHealth = 5;
    [SerializeField] float enemyCurrentHealth;

    [SerializeField] bool canTakeDamage = true;
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    void Update()
    {
        if(enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DamageToEnemy(float damage)
    {
        if(canTakeDamage == true)
        {
            enemyCurrentHealth -= damage;
        }
    }
}
