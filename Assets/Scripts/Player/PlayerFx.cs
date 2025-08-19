using UnityEngine;

public class PlayerFx: MonoBehaviour
{
    private readonly string GroundDustFx;

    [SerializeField] private Transform _groundDustFxPrefab;

    public void GroundDust()
    {
        Instantiate(_groundDustFxPrefab, transform.position, transform.rotation);
    }

    public void Destroy(string prefabName)
    {

    }
}