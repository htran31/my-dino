using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 0.5f)]
        public float spawnChance;
        public string type;
    }

    public SpawnableObject[] objects;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;
    public float moveSpeed = 0.6f;

    // private float lastCloudX = Mathf.NegativeInfinity;
    // private float minCloudSpacing = 0.5f;



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
        float score = GameManager.Instance.Score;
        float spawnChance = Random.value;

        foreach (SpawnableObject obj in objects)
        {
            bool isCloud = obj.type == "Cloud";
            bool isFish = obj.type == "Fish";

            if ((isCloud && (score < 5 || score > 80)) || (isFish && (score < 5 || score > 110)))
                continue;

            // if (isCloud && Mathf.Abs(transform.position.x - lastCloudX) < minCloudSpacing)
            // {
            //     Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
            //     return;
            // }

            if (spawnChance < obj.spawnChance)
            {
                float yPos = isCloud ? Random.Range(2.5f, 4.5f) : Random.Range(-8.5f, -5f);
                Vector3 spawnPos = new Vector3(transform.position.x, yPos, transform.position.z);

                GameObject movingObj = Instantiate(obj.prefab, spawnPos, Quaternion.identity);
                movingObj.AddComponent<Cloud>().speed = moveSpeed;

                // if (isCloud)
                // {
                //     lastCloudX = transform.position.x;
                // }

                break;
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    public void ResetClouds()
    {
        // Reset biến khoảng cách để spawn lại từ đầu
        // lastCloudX = Mathf.NegativeInfinity;

        GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");
        GameObject[] fishes = GameObject.FindGameObjectsWithTag("Fish");
        foreach (GameObject cloud in clouds)
        {
            Destroy(cloud);
        }
        foreach (GameObject fish in fishes)
        {
            Destroy(fish);
        }
    }

}
