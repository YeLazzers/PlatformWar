using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _target.position + _offset);
        transform.position = screenPoint;
    }
}
