using UnityEngine;

public class Parallax : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 0.5f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;
    public float minSpawnRate = 0.5f;
    public float maxSpawnRate = 1.5f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        // if (!gameObject.activeInHierarchy) return; 

        float spawnChance = Random.value;
        float randomY = Random.Range(-0.6f, 2.2f);
        Vector3 seaweedPosition = new Vector3(transform.position.x, randomY, transform.position.z);

        Debug.Log(randomY);
        foreach (SpawnableObject obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab, seaweedPosition, Quaternion.identity);
                obstacle.transform.position += transform.position;
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
