using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private bool _isOnlyGround;

    private bool isGrounded;

    public bool IsGrounded => isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isOnlyGround || _isOnlyGround && collision.TryGetComponent(out Ground ground))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isOnlyGround || _isOnlyGround && collision.TryGetComponent(out Ground ground))
        {
            isGrounded = false;
        }
    }
}
