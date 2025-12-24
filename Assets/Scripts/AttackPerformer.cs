// AttackPerformer.cs
using UnityEngine;
using UnityEngine.UI;

public class AttackPerformer : MonoBehaviour
{
    [SerializeField] private CharacterContext _characterContext;
    [SerializeField] private Button _buttonAttack1, _buttonAttack2, _buttonAttack3;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _selectedColor = Color.yellow;

    private IAttackStrategy _basicAttack = new BasicAttackStrategy();
    private IAttackStrategy _comboAttack = new ComboAttackStrategy();
    private IAttackStrategy _heavyAttack = new HeavyAttackStrategy();

    private void Start()
    {
        // Привязываем обработчики к кнопкам
        _buttonAttack1.onClick.AddListener(() => SetAttackStrategy(_basicAttack, _buttonAttack1));
        _buttonAttack2.onClick.AddListener(() => SetAttackStrategy(_comboAttack, _buttonAttack2));
        _buttonAttack3.onClick.AddListener(() => SetAttackStrategy(_heavyAttack, _buttonAttack3));

        // Устанавливаем стратегию по умолчанию и подсвечиваем кнопку
        SetAttackStrategy(_basicAttack, _buttonAttack1);
    }

    private void Update()
    {
        // Обработка нажатия клавиши Q для атаки
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _characterContext.PerformAttack();
        }
    }

    // Устанавливает стратегию и подсвечивает активную кнопку
    private void SetAttackStrategy(IAttackStrategy strategy, Button selectedButton)
    {
        _characterContext.SetStrategy(strategy);
        HighlightButton(selectedButton);
    }

    // Подсвечивает только выбранную кнопку
    private void HighlightButton(Button selected)
    {
        _buttonAttack1.image.color = _normalColor;
        _buttonAttack2.image.color = _normalColor;
        _buttonAttack3.image.color = _normalColor;
        selected.image.color = _selectedColor;
    }
}