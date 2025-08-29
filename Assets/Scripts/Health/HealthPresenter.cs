using UnityEngine;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private UIHealthBase _uiElement;
    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        _uiElement.Set(_health.Current, _health.Max);
    }

    private void OnEnable()
    {
        _health.Changed += _uiElement.Set;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Changed -= _uiElement.Set;
        _health.Died -= OnDied;
    }

    private void LateUpdate()
    {
        _uiElement.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + _offset);
    }

    private void OnDied()
    {
        Destroy(_uiElement.gameObject);
    }
}