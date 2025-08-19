using UnityEngine;

public abstract class MoverBase
{
    protected bool _isActive = false;
    protected float _movementSpeed;
    protected Rigidbody2D _rigidbody;
    protected DirectionFlipper2D _directionFlipper;

    public bool IsActive => _isActive;

    public MoverBase(Rigidbody2D rigidbody, float speed)
    {
        _rigidbody = rigidbody;
        _movementSpeed = speed;

        _directionFlipper = new DirectionFlipper2D(rigidbody);
    }

    protected void Move(Vector2 direction)
    {
        if (!_isActive)
            return;

        _directionFlipper.FlipHorizontal(direction);
        _rigidbody.velocity = new Vector2(direction.normalized.x * _movementSpeed, _rigidbody.velocity.y);
    }

    protected void Move(float xDirection)
    {
        if (!_isActive)
            return;

        _directionFlipper.FlipHorizontal(xDirection);
        _rigidbody.velocity = new Vector2(xDirection * _movementSpeed, _rigidbody.velocity.y);
    }

    public abstract void Move();
    
    public void SetSpeed(float speed) => _movementSpeed = speed;

    public void Activate() => _isActive = true;

    public void Deactivate()
    {
        _isActive = false;
        _rigidbody.velocity = Vector2.zero;
    }
}