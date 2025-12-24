// EnemyManager.cs
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs; // 0-Enemy1, 1-Enemy2, 2-Enemy3
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Animator playerAnimator;

    private Dictionary<int, Queue<Enemy>> enemyPools = new Dictionary<int, Queue<Enemy>>();
    private Enemy currentEnemy;
    private int currentEnemyIndex = 0;

    private void Start()
    {
        InitializePools();
        ShowEnemy(currentEnemyIndex);

        // Подписываемся на смену стратегии атаки у игрока
        var attackPerformer = FindObjectOfType<AttackPerformer>();
        if (attackPerformer != null)
        {
            // Добавляем обработчик смены стратегии
            attackPerformer.OnStrategyChanged += HandleStrategyChanged;
        }
    }

    private void InitializePools()
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            var pool = new Queue<Enemy>();

            // Создаем по 2 объекта каждого типа для пула
            for (int j = 0; j < 2; j++)
            {
                var enemy = Instantiate(enemyPrefabs[i], enemySpawnPoint.position, Quaternion.identity, transform);
                enemy.gameObject.SetActive(false);
                pool.Enqueue(enemy);
            }

            enemyPools.Add(i, pool);
        }
    }

    private void HandleStrategyChanged(int strategyIndex)
    {
        // Меняем врага при смене стратегии атаки
        SwitchEnemy(strategyIndex % enemyPrefabs.Length);
    }

    public void SwitchEnemy(int enemyIndex)
    {
        if (enemyIndex < 0 || enemyIndex >= enemyPrefabs.Length)
            return;

        // Скрываем текущего врага
        if (currentEnemy != null)
        {
            ReturnEnemyToPool(currentEnemyIndex, currentEnemy);
        }

        // Показываем нового врага
        currentEnemyIndex = enemyIndex;
        ShowEnemy(enemyIndex);
    }

    private void ShowEnemy(int enemyIndex)
    {
        currentEnemy = GetEnemyFromPool(enemyIndex);
        if (currentEnemy != null)
        {
            currentEnemy.Initialize(playerAnimator);
        }
    }

    private Enemy GetEnemyFromPool(int enemyIndex)
    {
        if (enemyPools.TryGetValue(enemyIndex, out var pool))
        {
            if (pool.Count > 0)
            {
                return pool.Dequeue();
            }
            else
            {
                // Если пул пуст, создаем новый объект
                var enemy = Instantiate(enemyPrefabs[enemyIndex], enemySpawnPoint.position,
                                       Quaternion.identity, transform);
                return enemy;
            }
        }

        return null;
    }

    private void ReturnEnemyToPool(int enemyIndex, Enemy enemy)
    {
        enemy.HideEnemy();

        if (enemyPools.TryGetValue(enemyIndex, out var pool))
        {
            pool.Enqueue(enemy);
        }
    }

    public void SwitchToNextEnemy()
    {
        int nextIndex = (currentEnemyIndex + 1) % enemyPrefabs.Length;
        SwitchEnemy(nextIndex);
    }

    // Методы для настройки через Bootstrapper
    public void Configure(Enemy[] prefabs, Transform spawnPoint, Animator animator)
    {
        enemyPrefabs = prefabs;
        enemySpawnPoint = spawnPoint;
        playerAnimator = animator;
    }
}