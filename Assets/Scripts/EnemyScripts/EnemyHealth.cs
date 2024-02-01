using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyMaxHealth = 5;
    [SerializeField] float enemyCurrentHealth;

    [SerializeField] bool canTakeDamage = true;

    [SerializeField] Transform explotionAudio;
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    void Update()
    {
        if(enemyCurrentHealth <= 0)
        {
            Instantiate(explotionAudio, transform.position, Quaternion.identity);
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
