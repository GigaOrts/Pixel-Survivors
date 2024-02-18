using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private const float MinSpeed = 0f;
    private const float MaxSpeed = 4f;
    private const float SpeedPhysicsFactor = 100f;

    private Quaternion TurnLeft = Quaternion.Euler(0f, 180f, 0f);
    private Quaternion TurnRight = Quaternion.identity;

    [Range(MinSpeed, MaxSpeed)]
    [SerializeField] private float _speed;

    public Vector3 Position => _transform.position;
    protected bool CanMove { get; private set; }

    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private float _totalSpeed;

    private Coroutine _pushBackCoroutine;

    private void Awake()
    {
        CanMove = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }

    public void Move(Vector3 direction)
    {
        if (CanMove == false)
        {
            return;
        }

        direction.Normalize();

        _totalSpeed = _speed * SpeedPhysicsFactor;
        _rigidbody.velocity = _totalSpeed * Time.deltaTime * direction;

        Rotate(_rigidbody.velocity.x);
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        Vector2 direction = targetPosition - transform.position;
        Move(direction);
    }

    public void Lock()
    {
        CanMove = false;
    }

    public void Unlock()
    {
        CanMove = true;
    }

    private void Rotate(float velocityX)
    {
        switch (velocityX)
        {
            case > 0:
                transform.localRotation = TurnRight;
                break;
            case < 0:
                transform.localRotation = TurnLeft;
                break;
        }
    }

    public void PushBack(Vector2 velocity)
    {
        if (_pushBackCoroutine != null)
        {
            StopCoroutine(_pushBackCoroutine);
        }

        Lock();
        _pushBackCoroutine = StartCoroutine(PushBackAsync(velocity));
    }
    
    private IEnumerator PushBackAsync(Vector2 force)
    {
        float pushBackDuration = 0.4f;
        WaitForSeconds delay = new WaitForSeconds(pushBackDuration);

        _rigidbody.AddForce(force);

        yield return delay;
        Unlock();
    }
}
