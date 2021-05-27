using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamagable
{
    float healt { get; }
    void ReceiveDamage(float amount);
    void Die();
}
