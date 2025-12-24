// Bootstrapper.cs
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Header("Настройки врагов")]
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform enemySpawnPoint;

    [Header("Настройки игрока")]
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        InitializeEnemySystem();
        Debug.Log("Система врагов инициализирована!");
    }

    private void InitializeEnemySystem()
    {
        // Создаем и настраиваем EnemyManager
        var enemyManagerObject = new GameObject("EnemyManager");
        var enemyManager = enemyManagerObject.AddComponent<EnemyManager>();

        // Настраиваем EnemyManager через метод Configure
        enemyManager.Configure(enemyPrefabs, enemySpawnPoint, playerAnimator);
    }
}