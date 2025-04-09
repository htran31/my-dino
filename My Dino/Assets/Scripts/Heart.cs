// using UnityEngine;

// public class Heart : MonoBehaviour
// {
//     private float leftEdge;
//     public float speedBoostDuration = 5f; // Duration of the speed boost
//     public float speedBoostFactor = 1.5f; // How much faster the player should run


//     private void Start() {
//         leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
//     }

//     private void Update() {
//         transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;
//         if (transform.position.x < leftEdge) {
//             Destroy(gameObject);
//         }
//     }

//      private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             // Player collects the heart
//             GameManager.Instance.CollectHeart();
//             ApplySpeedBoost(other.gameObject);
//             Destroy(gameObject);
//         }
//     }

//     private void ApplySpeedBoost(GameObject player)
//     {
//         Player playerScript = player.GetComponent<Player>();
//         if (playerScript != null)
//         {
//             // Apply a speed boost to the player for a specified duration
//             playerScript.ApplySpeedBoost(speedBoostFactor, speedBoostDuration);
//         }
//     }
// }
