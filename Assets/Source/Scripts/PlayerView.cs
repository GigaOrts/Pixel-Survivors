using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _TMPro;

    private void Awake()
    {
        _health.Changed += OnHealthChanged;
    }

    private void OnHealthChanged(int value)
    {
        _TMPro.text = $"Health: {value}";
    }
}
