// CharacterContext.cs
using UnityEngine;
using Unity.Collections;

public class CharacterContext : MonoBehaviour
{
    private IAttackStrategy _currentStrategy;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        // Стратегия по умолчанию
        SetStrategy(new BasicAttackStrategy());
    }

    // Метод для изменения стратегии (открыт для расширения)
    public void SetStrategy(IAttackStrategy newStrategy)
    {
        _currentStrategy = newStrategy;
        Debug.Log($"Strategy changed to: {_currentStrategy.GetAnimationName()}");
    }

    // Метод для выполнения атаки (закрыт для модификаций)
    public void PerformAttack()
    {
        if (_currentStrategy != null)
        {
            _currentStrategy.PerformAttack(_animator);
        }
    }
}