using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private HealthBarSmooth _playerHealthBar;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _respawnTime = 3f;

    private void Start()
    {
        _player.Initialize(_startPoint.position);
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
    }

    private void OnPlayerDied(Player player)
    {
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        _player.gameObject.SetActive(false);
        _playerHealthBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(_respawnTime);

        _player.gameObject.SetActive(true);
        _playerHealthBar.gameObject.SetActive(true);
        _player.Initialize(_startPoint.position);
    }
}
