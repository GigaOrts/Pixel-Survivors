using UnityEngine;

public class MovementAI : Movement
{
    private Player _player;

    private void Start()
    {
        _player = FindAnyObjectByType<Player>();
    }

    private void FixedUpdate()
    {
        if (_player.enabled == false)
        {
            return;
        }

        MoveTowards(_player.Position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player _))
        {
            Lock();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player _))
        {
            Unlock();
        }
    }
}
