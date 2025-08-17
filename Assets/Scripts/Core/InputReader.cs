using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal;

    public event Action JumpPressed;
    public event Action<float> HorizontalMoving;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            JumpPressed?.Invoke();
    }

    private void FixedUpdate()
    {
        HorizontalMoving?.Invoke(Input.GetAxis(nameof(Horizontal)));
    }
}
