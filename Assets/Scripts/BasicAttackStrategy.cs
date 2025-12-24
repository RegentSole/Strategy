// BasicAttackStrategy.cs
using UnityEngine;

public class BasicAttackStrategy : IAttackStrategy
{
    public void PerformAttack(Animator animator)
    {
        // Запускаем анимацию "Attack1"
        animator.SetTrigger("Attack1");
        // Здесь можно добавить логику нанесения урона, проверку попадания и т.д.
    }

    public string GetAnimationName() => "Attack1";
}

// ComboAttackStrategy.cs
public class ComboAttackStrategy : IAttackStrategy
{
    public void PerformAttack(Animator animator)
    {
        animator.SetTrigger("Attack2");
    }
    public string GetAnimationName() => "Attack2";
}

// HeavyAttackStrategy.cs
public class HeavyAttackStrategy : IAttackStrategy
{
    public void PerformAttack(Animator animator)
    {
        animator.SetTrigger("Attack3");
    }
    public string GetAnimationName() => "Attack3";
}