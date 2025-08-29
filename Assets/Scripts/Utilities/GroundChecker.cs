using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private bool _isOnlyGround;

    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isOnlyGround == false || _isOnlyGround && collision.TryGetComponent(out Ground ground))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isOnlyGround == false || _isOnlyGround && collision.TryGetComponent(out Ground ground))
        {
            _isGrounded = false;
        }
    }
}
