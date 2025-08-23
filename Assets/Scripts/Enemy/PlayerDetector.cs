using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerDetector : MonoBehaviour
{
    private bool _isPlayerVisible = false;

    public event Action<Player> Detected;
    public event Action<Player> Missed;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            Detected?.Invoke(player);
            _isPlayerVisible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            Missed?.Invoke(player);
            _isPlayerVisible = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isPlayerVisible ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
