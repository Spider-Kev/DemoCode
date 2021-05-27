using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour, IDamagable
{
    public float startingHealt;
    private float totalHealth;
    public float healt { get { return totalHealth; } }

    public Image imageHealthbar;

    private void Start()
    {
        totalHealth = startingHealt;
    }


    public void Die()
    {
        EventManager.TriggerEvent("GameOver", false);
    }

    public void ReceiveDamage(float amount)
    {
        totalHealth -= amount;
        imageHealthbar.fillAmount = totalHealth / startingHealt;
        if (totalHealth <= 0)
            Die();
    }
}
