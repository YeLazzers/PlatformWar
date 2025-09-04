using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal;
    private readonly int MouseLeftButton = 0;
    private readonly KeyCode JumpKeyCode = KeyCode.Space;
    private readonly KeyCode AbilityPrimaryKeyCode = KeyCode.Q;

    public event Action JumpPressed;
    public event Action<float> HorizontalMoving;
    public event Action Attacked;
    public event Action<AbilitySlot> AbilityPressed;

    private void Update()
    {
        if (Input.GetKeyDown(JumpKeyCode))
            JumpPressed?.Invoke();
        if (Input.GetMouseButtonDown(MouseLeftButton))
            Attacked?.Invoke();

        if (Input.GetKeyDown(AbilityPrimaryKeyCode))
            AbilityPressed?.Invoke(AbilitySlot.Primary);
    }

    private void FixedUpdate()
    {
        HorizontalMoving?.Invoke(Input.GetAxis(nameof(Horizontal)));
    }
}
