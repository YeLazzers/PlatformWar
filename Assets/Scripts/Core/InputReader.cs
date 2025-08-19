using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal;
    private readonly int MouseLeftButton = 0;

    public event Action JumpPressed;
    public event Action<float> HorizontalMoving;
    public event Action Attacked;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            JumpPressed?.Invoke();
        if (Input.GetMouseButtonDown(MouseLeftButton))
            Attacked?.Invoke();
    }

    private void FixedUpdate()
    {
        HorizontalMoving?.Invoke(Input.GetAxis(nameof(Horizontal)));
    }
}
