using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public string spawnerName;
    [SerializeField] GameObject objectPrefab;
    [SerializeField] Transform spawnPos;

    public void SpawnObject()
    {
        Instantiate(objectPrefab, spawnPos.position, spawnPos.rotation);
    }
}
