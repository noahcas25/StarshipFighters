using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;
    private int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable() 
    {
        currentHealth = health;
    }

    public void ModifyHealth(int amount) 
    {
        currentHealth += amount;
        float currentHealthPct = ((float)currentHealth / (float)health);
        OnHealthPctChanged(currentHealthPct);
    }

    private void Update()
    {
        if(GetComponent<PlayerController>().currentHealth != currentHealth) {
            int num1 = GetComponent<PlayerController>().currentHealth;
            ModifyHealth(num1 - currentHealth);
        }

    }
}
