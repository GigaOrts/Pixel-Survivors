using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform _transform;

    public Vector2 Position => _transform.position;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
}
