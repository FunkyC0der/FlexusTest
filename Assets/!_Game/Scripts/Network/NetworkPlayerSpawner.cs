using Sisus.Init;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlexusTest.Network
{
  public class NetworkPlayerSpawner : NetworkBehaviour<INetworkService>
  {
    [SerializeField]
    private NetworkObject _playerPrefab;

    [SerializeField]
    private Transform[] _spawnPoints;
    
    private int _spawnPointIndex;

    private INetworkService _networkService;
    
    protected override void Init(INetworkService networkService) => 
      _networkService = networkService;

    private void Start()
    {
      _spawnPointIndex = Random.Range(0, _spawnPoints.Length);
      _networkService.OnClientConnected += OnClientConnected;
    }

    public override void OnDestroy()
    {
      _networkService.OnClientConnected -= OnClientConnected;
      base.OnDestroy();
    }

    private void OnClientConnected(ulong clientId)
    {
      if (!IsServer)
        return;
      
      Transform spawnPoint = GetNextSpawnPoint();
      NetworkObject player = Instantiate(_playerPrefab, spawnPoint.position, spawnPoint.rotation);
      player.SpawnAsPlayerObject(clientId);
    }

    private Transform GetNextSpawnPoint()
    {
      _spawnPointIndex = (_spawnPointIndex + 1) % _spawnPoints.Length;
      return _spawnPoints[_spawnPointIndex];
    }
  }
}