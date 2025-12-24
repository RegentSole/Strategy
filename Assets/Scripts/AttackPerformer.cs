// AttackPerformer.cs (обновленная версия)
using UnityEngine;
using UnityEngine.UI;
using System;

public class AttackPerformer : MonoBehaviour
{
    [SerializeField] private CharacterContext _characterContext;
    [SerializeField] private Button _buttonAttack1, _buttonAttack2, _buttonAttack3;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _selectedColor = Color.yellow;

    public event Action<int> OnStrategyChanged; // Добавляем событие

    private IAttackStrategy _basicAttack = new BasicAttackStrategy();
    private IAttackStrategy _comboAttack = new ComboAttackStrategy();
    private IAttackStrategy _heavyAttack = new HeavyAttackStrategy();

    private int currentStrategyIndex = 0;

    private void Start()
    {
        _buttonAttack1.onClick.AddListener(() => SetAttackStrategy(_basicAttack, _buttonAttack1, 0));
        _buttonAttack2.onClick.AddListener(() => SetAttackStrategy(_comboAttack, _buttonAttack2, 1));
        _buttonAttack3.onClick.AddListener(() => SetAttackStrategy(_heavyAttack, _buttonAttack3, 2));

        SetAttackStrategy(_basicAttack, _buttonAttack1, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _characterContext.PerformAttack();
        }

        // Для тестирования: смена врага по клавише E
        if (Input.GetKeyDown(KeyCode.E))
        {
            var enemyManager = FindObjectOfType<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.SwitchToNextEnemy();
            }
        }
    }

    private void SetAttackStrategy(IAttackStrategy strategy, Button selectedButton, int strategyIndex)
    {
        _characterContext.SetStrategy(strategy);
        HighlightButton(selectedButton);

        currentStrategyIndex = strategyIndex;
        OnStrategyChanged?.Invoke(strategyIndex); // Уведомляем о смене стратегии
    }

    private void HighlightButton(Button selected)
    {
        _buttonAttack1.image.color = _normalColor;
        _buttonAttack2.image.color = _normalColor;
        _buttonAttack3.image.color = _normalColor;
        selected.image.color = _selectedColor;
    }
}