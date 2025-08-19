using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class AnimationFx : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}