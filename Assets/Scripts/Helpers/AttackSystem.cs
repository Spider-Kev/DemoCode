using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    #region PUBLIC_REFERENCES
    public IDamagable targetToAttack;
    public List<IDamagable> damagablesInSight;
    public Character characterAssigned;
    #endregion

    #region PRIVATE_REFERENCE
    private IDamagable damagableObject;
    #endregion

    #region UNITY_METHODS
    private void OnEnable()
    {
        EventManager.StartListening("EnemyKilled", OnEnemyKilled);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EnemyKilled", OnEnemyKilled);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damagableObject = collision.GetComponentInParent<IDamagable>();
        AssignAttackTarget(damagableObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        damagableObject = collision.GetComponentInParent<IDamagable>();
        RemoveAttackTarget(damagableObject);
    }
    #endregion

    #region PUBLIC_METHODS
    public void SetStartValues()
    {
        if (damagablesInSight == null)
            damagablesInSight = new List<IDamagable>();
        damagablesInSight.Clear();
        targetToAttack = null;
    }

    public void AssignAttackTarget(IDamagable target)
    {
        damagablesInSight.Add(target);
        if (targetToAttack == null)
        {
            targetToAttack = target;
            characterAssigned.characterState = Character.EnumCharacterState.Attacking;
        }

    }

    public void AttackToTarget()
    {
        targetToAttack.ReceiveDamage(characterAssigned.currentDamage);
        CheckIfTargetStillsAlive();
    }

    public void CheckIfTargetStillsAlive()
    {
        if (targetToAttack == null || targetToAttack.healt < 0)
            AssignNewTarget();

        else
            characterAssigned.characterState = Character.EnumCharacterState.Attacking;
    }

    public void AssignNewTarget()
    {
        targetToAttack = null;
        if (damagablesInSight.Count > 0)
        {
            for (int i = 0; i < damagablesInSight.Count; i++)
            {
                if (damagablesInSight[i].healt > 0)
                {
                    targetToAttack = damagablesInSight[i];
                    characterAssigned.characterState = Character.EnumCharacterState.Attacking;
                    break;
                }
                else
                {
                    damagablesInSight.RemoveAt(i);
                    i--;
                }
            }
        }

        if (targetToAttack == null)
            characterAssigned.characterState = Character.EnumCharacterState.Moving;
    }

    public void RemoveAttackTarget(IDamagable damagable)
    {
        damagablesInSight.Remove(damagable);
        AssignNewTarget();
    }
    #endregion

    #region LISTENER_METHODS
    public void OnEnemyKilled(object param)
    {
        CheckIfTargetStillsAlive();
    }
    #endregion
}
