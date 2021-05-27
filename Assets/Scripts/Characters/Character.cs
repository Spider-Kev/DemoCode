using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, IDamagable
{
    public float healt { get { return currentHealth; } }


    public enum EnumCharacterState
    {
        Moving,
        Attacking,
        Busy,
        Dying
    }
    public EnumCharacterState characterState;

    #region PUBLIC_PROPERTIES
    public float currentHealth;
    public float totalHealt;
    public float currentSpeed;
    public float currentDamage;
    public float timeRemainingDeath = 2f;
    public Animator animator;
    public Image imageHealth;
    #endregion

    #region PRIVATE_PROPERTIES
    private float lastTimeAttacked;
    #endregion

    #region PUBLIC_REFERENCES
    public AttackSystem attackSystem;
    public CharacterStats stats;
    #endregion

    #region UNITY_METHODS
    private void Start()
    {
        StartValues();
    }

    private void Update()
    {
        switch (characterState)
        {
            case EnumCharacterState.Moving:
                Move();
            break;

            case EnumCharacterState.Attacking:
                Attack();
            break;

            case EnumCharacterState.Dying:
                Die();
            break;

            case EnumCharacterState.Busy:
            break;

            default:
            break;
        }

        
    }
    #endregion

    #region PUBLIC_METHODS
    public void AssignCharacter(CharacterStats characterStats)
    {
        stats = characterStats;
        //if (animator == null)
        //    animator = GetComponentInChildren<Animator>();
    }

    public void ReceiveDamage(float amount)
    {
        currentHealth -= amount;
        imageHealth.fillAmount = currentHealth/totalHealt;

        if (currentHealth < 0)
        {
            characterState = EnumCharacterState.Dying;
            Die();
        }
            
    }
    #endregion


    #region VIRTUAL_METHODS
    public virtual void StartValues()
    {
        totalHealt = currentHealth = stats.totalHealt;
        currentSpeed = stats.speed;
        currentDamage = stats.damage;
        characterState = EnumCharacterState.Moving;
        imageHealth.fillAmount = 1;
        attackSystem.SetStartValues();
    }

    public virtual void Move()
    {
        animator.SetBool("Run", true);
        animator.SetBool("Attack", false);
        this.transform.position += Vector3.right * Time.deltaTime * currentSpeed;
    }

    public virtual void Stop()
    {
        currentSpeed = 0;
        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
        attackSystem.CheckIfTargetStillsAlive();
    }

    public virtual void Attack()
    {
        animator.SetBool("Attack", true);
        if (lastTimeAttacked<=0)
        {
            lastTimeAttacked = stats.timeBetweenAttack;
            attackSystem.AttackToTarget();            
        }           
        else
        {
            lastTimeAttacked -= Time.deltaTime;
        }
    }

    public virtual void Die()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
