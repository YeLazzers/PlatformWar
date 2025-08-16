using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    private readonly string Horizontal;

    public event UnityAction JumpPressed;
    public event UnityAction<float> HorizontalMoving;
    
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
