// Enemy.cs
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    // Шаблонный метод - определяет общий алгоритм поведения врага
    public void Initialize(Animator playerAnimator = null)
    {
        ShowEnemy();
        PlayInitialAnimation();
        SetupBehavior(playerAnimator);
    }

    // Шаблонные методы для переопределения в наследниках
    protected abstract void PlayInitialAnimation();
    protected abstract void SetupBehavior(Animator playerAnimator);
    protected abstract void CleanupBehavior();

    // Общие методы (нельзя переопределять)
    private void ShowEnemy()
    {
        gameObject.SetActive(true);
        Debug.Log($"{GetType().Name} появился на сцене");
    }

    public void HideEnemy()
    {
        CleanupBehavior();
        gameObject.SetActive(false);
        Debug.Log($"{GetType().Name} скрылся со сцены");
    }
}