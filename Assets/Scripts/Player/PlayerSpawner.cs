using System;
using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _prefab;

    private Player _player;
    private float _respawnTime = 3f;

    public event Action<Player> Spawned;
    public event Action<Player> Destroyed;

    private void Start()
    {
        StartCoroutine(Spawn(null));
    }

    private void OnDied(Player player)
    {
        player.Destroyed -= OnDied;
        Destroy(player.gameObject);
        
        Destroyed?.Invoke(player);

        StartCoroutine(Spawn(new WaitForSeconds(_respawnTime)));
    }

    private IEnumerator Spawn(YieldInstruction preWaiter)
    {
        if (preWaiter != null)
        {
            yield return preWaiter;
        }

        _player = Instantiate(_prefab, transform);
        
        _player.Destroyed += OnDied;

        Spawned?.Invoke(_player);
    }
}
