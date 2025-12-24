using UnityEngine;

// IAttackStrategy.cs
public interface IAttackStrategy
{
    // Метод для выполнения логики атаки
    void PerformAttack(Animator animator);
    // Метод для получения имени анимации (опционально, для удобства)
    string GetAnimationName();
}