using UnityEngine;

public class LootSpot: MonoBehaviour
{
    private bool _isSpawned;

    public bool IsLootSpawned => _isSpawned;

    public T Spawn<T>(T _prefab) where T: LootBase
    {
        T loot = Instantiate(_prefab, transform);
        _isSpawned = true;

        return loot;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
