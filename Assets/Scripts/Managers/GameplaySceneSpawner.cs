using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySceneSpawner : Singleton<GameplaySceneSpawner>
{
    public event Action<GameObject> OnPlayerSpawned;

    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;

    [Header("Setting")]
    [SerializeField] private string[] excludedScenes;

    // ---

    private void OnEnable()
    {
        // Subscribe to Unity's scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Always unsubscribe when disabled to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Don't spawn inside the Bootstrap or Main Menu scenes!
        foreach (string excludedScene in excludedScenes)
        {
            string currentScene = scene.name;
            if (currentScene == excludedScene) return;
        }

        SpawnPlayerInScene();
    }

    private void SpawnPlayerInScene()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab is missing in the Spawner Inspector!");
            return;
        }

        // Search the newly loaded scene for a spawn point
        LevelSpawnPoint spawnPoint = LevelSpawnPoint.Instance;
        if (spawnPoint == null)
        {
            spawnPoint = FindAnyObjectByType<LevelSpawnPoint>();
        }

        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        if (spawnPoint != null)
        {
            spawnPos = spawnPoint.transform.position;
            spawnRot = spawnPoint.transform.rotation;
        }
        else
        {
            Debug.LogWarning("No LevelSpawnPoint found in this level! Spawning at (0,0,0).");
        }



        GameObject playerInstance = SpawnService.Spawn(playerPrefab, spawnPos, spawnRot);
        OnPlayerSpawned?.Invoke(playerInstance);
    }
}
