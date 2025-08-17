using UnityEngine;

public class CoinSpot : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    public Coin SpawnCoin()
    {
        Coin coin = Instantiate(_coinPrefab, transform);

        return coin;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
