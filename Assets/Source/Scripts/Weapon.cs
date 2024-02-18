using UnityEngine;

public class Weapon : MonoBehaviour
{
    private const int MinDamage = 0;
    private const int MaxDamage = 5;

    [Range(MinDamage, MaxDamage)]
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;

    private float _timer;
    protected bool CanAttack { get; private set; }

    private void Update()
    {
        CountCooldown();
    }

    private void CountCooldown()
    {
        _timer += Time.deltaTime;

        if (_timer >= _cooldown)
        {
            CanAttack = true;
        }
    }

    protected void ResetCooldown()
    {
        _timer = 0f;
        CanAttack = false;
    }

    public void Attack(Health health)
    {
        health.TakeDamage(ComputeDamage());
        Debug.Log(transform.parent.name + " damaged enemy by " + _damage);
    }

    public int ComputeDamage()
    {
        return _damage;
    }
}
