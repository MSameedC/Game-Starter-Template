using UnityEngine;

public static class SpawnService
{
    /// <summary>
    /// Safely spawn a GameObject at the given position.
    /// </summary>
    public static GameObject Spawn(GameObject prefab, Vector3 position)
    {
        return Spawn(prefab, position, Quaternion.identity, null);
    }

    /// <summary>
    /// Spawn a GameObject at the given position with optional rotation.
    /// </summary>
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Spawn(prefab, position, rotation, null);
    }

    /// <summary>
    /// The core instantiation method.
    /// </summary>
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is null");
            return null;
        }

        return Object.Instantiate(prefab, position, rotation, parent);
    }
}
