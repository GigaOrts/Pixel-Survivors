using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponMelee : Weapon
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _attackRadius;
    [SerializeField] private bool _canAttackNonTarget;

    [SerializeField] private bool _canPushBack;
    [ShowIf(nameof(_canPushBack))]
    [SerializeField] private float _pushBackForce;

    [Header("Gizmos")]
    [SerializeField] private bool _canDrawGizmo;

    // TODO вернуть кеширование трансформа
    private Transform _transform;
    private Collider2D[] _colliders;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (CanAttack)
        {
            AttackMany();
        }
    }

    private void AttackMany()
    {
        _colliders = Physics2D.OverlapCircleAll(transform.position, _attackRadius, _targetMask);

        if (_canAttackNonTarget == false)
        {
            if (_colliders.Length == 0)
                return;
        }

        foreach (var enemy in _colliders)
        {
            if (_canPushBack && enemy.TryGetComponent(out Movement movement))
            {
                Vector2 velocity = (movement.Position - transform.position).normalized * _pushBackForce;
                movement.PushBack(velocity);
            }

            if (enemy.TryGetComponent(out Health health))
            {
                health.TakeDamage(ComputeDamage());
            }
        }

        ResetCooldown();
    }

    private void OnDrawGizmos()
    {
        if (_canDrawGizmo)
        {
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
        }
    }
}
