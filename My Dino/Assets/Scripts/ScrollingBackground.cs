using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float moveSpeed = 6f;
    public int minScore = 20;
    public int maxScore = 60;
    public int currentScore = 0;

    void Update()
    {
        if (!GameManager.Instance.gameOver) 
        {
        float score = GameManager.Instance.score;
        if (score >= minScore && score <= maxScore)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
            
        }
    }
}
