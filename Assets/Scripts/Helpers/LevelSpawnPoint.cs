using UnityEngine;

public class LevelSpawnPoint : MonoBehaviour
{
    public static LevelSpawnPoint Instance { get; private set; }

    private void Awake()
    {
        // Register this spawn point the microsecond the scene loads
        Instance = this;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawRay(transform.position, transform.forward * 1f);
    }
}
