using UnityEngine;

public class DirectionFlipper2D
{
    private Rigidbody2D _rigidBody;

    public DirectionFlipper2D(Rigidbody2D rigidBody)
    {
        _rigidBody = rigidBody;
    }

    public void FlipHorizontal(float xDirection)
    {
        if (xDirection > 0)
            TurnRight();
        else if (xDirection < 0)
            TurnLeft();
    }
    public void FlipHorizontal(Vector2 direction)
    {
        float deltaX = direction.normalized.x;

        if (deltaX > 0)
            TurnRight();
        else if (deltaX < 0)
            TurnLeft();
    }

    private void TurnLeft()
    {
        
        _rigidBody.transform.right = Vector2.left;
    }

    private void TurnRight()
    {
        _rigidBody.transform.right = Vector2.right;
    }
}
