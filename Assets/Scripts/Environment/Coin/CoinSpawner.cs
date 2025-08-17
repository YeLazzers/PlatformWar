using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private int _coinTotal;
    [SerializeField] private CoinSpot[] _spots;

    private void OnEnable()
    {
        foreach(CoinSpot spot in GetRandomSpots(_spots, _coinTotal))
            spot.SpawnCoin();
    }

    private CoinSpot GetRandomSpot(List<CoinSpot> spots)
    {
        return spots[Random.Range(0, _spots.Length)];
    }

    private CoinSpot[] GetRandomSpots(CoinSpot[] spots, int count)
    {
        int totalCount = count > spots.Length ? spots.Length : count;

        List<CoinSpot> tempSpots = spots.ToList();
        List<CoinSpot> totalSpots = new();

        for (int i = 0; i < totalCount; i++)
        {
            CoinSpot spot = GetRandomSpot(tempSpots);
            totalSpots.Add(spot);
            tempSpots.Remove(spot);
        }
        return totalSpots.ToArray();
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _spots = new CoinSpot[pointCount];

        for (int i = 0; i < _spots.Length; i++)
            _spots[i] = transform.GetChild(i).GetComponent<CoinSpot>();
    }
#endif
}
