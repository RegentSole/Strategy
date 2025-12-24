// Enemy3.cs - Враг №3 (синхронная атака с игроком)
using System.Collections;
using UnityEngine;

public class Enemy3 : Enemy
{
    private Animator playerAnimator;
    private bool isAttacking = false;

    protected override void PlayInitialAnimation()
    {
        animator.Play("Idle");
        Debug.Log("Враг 3: Жду синхронной атаки с игроком");
    }

    protected override void SetupBehavior(Animator playerAnimator)
    {
        this.playerAnimator = playerAnimator;

        if (playerAnimator != null)
        {
            // Подписываемся на события анимаций игрока
            StartCoroutine(MonitorPlayerAnimations());
        }
    }

    protected override void CleanupBehavior()
    {
        playerAnimator = null;
        isAttacking = false;
    }

    private IEnumerator MonitorPlayerAnimations()
    {
        while (playerAnimator != null)
        {
            var stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

            // Проверяем, атакует ли игрок
            if (stateInfo.IsName("Attack1") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    animator.Play("Attack");
                    Debug.Log("Враг 3: Атакую синхронно с игроком!");
                }
            }
            else if (isAttacking)
            {
                isAttacking = false;
                animator.Play("Idle");
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}