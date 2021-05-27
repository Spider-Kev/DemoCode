using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : Character
{
    public float maxDistanceToMove = 25f;
    public SpriteRenderer spriteRenderer;

    #region OVERRIDE_METHODS
    public override void Move()
    {
        if (Input.GetMouseButton(0))
        {
            currentSpeed = stats.speed *( (Input.mousePosition.x >= (Screen.width / 2)) ? 1 : -1);
            base.Move();
            this.transform.position = Vector3.ClampMagnitude(this.transform.position, maxDistanceToMove);
            
        }
        else
            Stop();

        spriteRenderer.flipX = currentSpeed >= 0;
    }

    public override void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            characterState = EnumCharacterState.Moving;
            animator.SetBool("Attack", false);
        }
        else
            base.Attack();
    }

    public override void Die()
    {
        EventManager.TriggerEvent("GameOver",false);
        base.Die();
    }
    #endregion
}
