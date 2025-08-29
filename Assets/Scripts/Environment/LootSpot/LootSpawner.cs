using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private int _coinTotal;
    [SerializeField] private LootBase _coinPrefab;
    [SerializeField] private int _boxTotal;
    [SerializeField] private LootBase _boxPrefab;
    [SerializeField] private List<LootSpot> _spots;

    private void Start()
    {
        for (int i = 0; i < _coinTotal; i++)
        {
            LootBase coin = GetRandomAvailableSpot()
                .Spawn(_coinPrefab);

            coin.AnimationEvents.Interacted += OnInteractedDestroy;
        }

        for (int i = 0; i < _boxTotal; i++)
        {
            GetRandomAvailableSpot()
                .Spawn(_boxPrefab);
        }
    }

    private void OnInteractedDestroy(LootAnimationEvents lootEvents)
    {
        lootEvents.Interacted -= OnInteractedDestroy;

        Destroy(lootEvents.gameObject);
    }

    private LootSpot GetRandomAvailableSpot()
    {

        var availableSpots = _spots.Where(spot => !spot.IsLootSpawned).ToArray();
        return availableSpots[Random.Range(0, availableSpots.Length)];
    }


#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int spotCount = transform.childCount;
        _spots = new();

        for (int i = 0; i < spotCount; i++)
            _spots.Add(transform.GetChild(i).GetComponent<LootSpot>());
    }
#endif
}
