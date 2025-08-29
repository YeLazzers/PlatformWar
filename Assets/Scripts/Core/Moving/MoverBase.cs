using UnityEngine;

public abstract class MoverBase
{
    protected float MovementSpeed;
    protected Rigidbody2D Rigidbody;
    protected DirectionFlipper2D DirectionFlipper;
    protected ICoroutineRunner CoroutineRunner;

    public MoverBase(Rigidbody2D rigidbody, float speed, ICoroutineRunner coroutineRunner)
    {
        Rigidbody = rigidbody;
        MovementSpeed = speed;
        CoroutineRunner = coroutineRunner;

        DirectionFlipper = new DirectionFlipper2D(rigidbody);
        IsActive = false;
    }

    public bool IsActive { get; protected set; }

    public abstract void Move();

    public void SetSpeed(float speed)
    {
        MovementSpeed = speed;
    }

    public virtual void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        Rigidbody.velocity = Vector2.zero;
    }

    protected void Move(Vector2 direction)
    {
        if (IsActive == false)
            return;

        DirectionFlipper.FlipHorizontal(direction);
        Rigidbody.velocity = new Vector2(direction.normalized.x * MovementSpeed, Rigidbody.velocity.y);
    }

    protected void Move(float xDirection)
    {
        if (IsActive == false)
            return;

        DirectionFlipper.FlipHorizontal(xDirection);
        Rigidbody.velocity = new Vector2(xDirection * MovementSpeed, Rigidbody.velocity.y);
    }
}