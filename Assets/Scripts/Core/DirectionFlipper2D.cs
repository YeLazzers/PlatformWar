using UnityEngine;

public class DirectionFlipper2D
{
    private Transform _characterTransform;

    public DirectionFlipper2D(Transform characterTransform)
    {
        _characterTransform = characterTransform;
    }

    public void FlipHorizontal(float xDirection)
    {
        if (xDirection > 0)
            TurnRight();
        else if (xDirection < 0)
            TurnLeft();
    }
    public void FlipHorizontal(Vector3 position)
    {
        float deltaX = position.x - _characterTransform.position.x;

        if (deltaX > 0)
            TurnRight();
        else if (deltaX < 0)
            TurnLeft();
    }

    private void TurnLeft()
    {
        _characterTransform.transform.right = Vector3.left;
    }

    private void TurnRight()
    {
        _characterTransform.transform.right = Vector3.right;
    }
}
