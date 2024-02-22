using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float currentHealth;

    [SerializeField] bool canTakeDamage = true;

    [SerializeField] Slider healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    private void Update()
    {
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            //Destroy(gameObject);
            Cursor.visible = false;
            SceneManagerExtended.ReloadScene();
            
        }
    }

    public void Damage(float damage)
    {
        if(canTakeDamage ==true)
        {
            currentHealth = currentHealth - damage;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemies"))
        {
           
        }
    }
}
