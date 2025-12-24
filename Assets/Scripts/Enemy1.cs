// Enemy1.cs - Враг №1 (одноразовая атака при появлении)
using UnityEngine;

public class Enemy1 : Enemy
{
    protected override void PlayInitialAnimation()
    {
        animator.Play("Attack");
        Debug.Log("Враг 1: Показываю анимацию атаки 1 раз");
    }

    protected override void SetupBehavior(Animator playerAnimator)
    {
        // Бездействует после атаки
        Invoke(nameof(PlayIdle), 1f);
    }

    protected override void CleanupBehavior()
    {
        CancelInvoke();
    }

    private void PlayIdle()
    {
        animator.Play("Idle");
    }
}