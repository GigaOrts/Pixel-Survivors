using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Health : MonoBehaviour
{
    public event Action<int> Changed;

    private const int MinValue = 0;
    private const int MaxValue = 100;

    [Range(MinValue, MaxValue)]
    [SerializeField] private int _value;

    private bool IsAlive => _value > 0;

    public void TakeDamage(int damage)
    {
        _value -= damage;
        _value = Mathf.Clamp(_value, MinValue, MaxValue);

        Changed?.Invoke(_value);

        if (IsAlive == false)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
