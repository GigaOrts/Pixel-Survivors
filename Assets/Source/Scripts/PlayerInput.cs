using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerInput : MonoBehaviour
{
    private Movement _movement;
    private float _horizontal;
    private float _vertical;
    private Vector2 _direction;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _direction = new Vector2(_horizontal, _vertical);
        _movement.Move(_direction);
    }
}
