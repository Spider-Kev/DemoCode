using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    #region UNITY_METHODS
    public override void StartValues()
    {
        base.StartValues();
        currentSpeed *= -1;
    }

    public override void Die()
    {
        EventManager.TriggerEvent("EnemyKilled", this);
        base.Die();
    }
    #endregion
}
