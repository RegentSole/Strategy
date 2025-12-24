// Enemy2.cs - Враг №2 (зацикленная атака с выстрелами)
using UnityEngine;
using System.Collections;

public class Enemy2 : Enemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackInterval = 1f;

    private Coroutine attackCoroutine;

    protected override void PlayInitialAnimation()
    {
        animator.Play("Idle");
        Debug.Log("Враг 2: Начинаю зацикленные атаки");
    }

    protected override void SetupBehavior(Animator playerAnimator)
    {
        attackCoroutine = StartCoroutine(AttackRoutine());
    }

    protected override void CleanupBehavior()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        animator.Play("Attack");
        // Создаем выстрел
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Враг 2: Выстрелил!");
    }
}