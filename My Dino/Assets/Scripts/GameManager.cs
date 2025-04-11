using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // [SerializeField] private TextMeshProUGUI scoreText;
    // [SerializeField] private TextMeshProUGUI hiscoreText;
    // [SerializeField] private TextMeshProUGUI gameOverText;
    // [SerializeField] private Button retryButton;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public Button retryButton;
    private Player player;
    public Spawner spawner;
    public Parallax seaweed;
    private Ground ground;
    private Vector3 initialGroundPosition;

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }
    public float score { get; private set; }
    public float Score => score;
    // private Camera mainCamera;
    private bool cameraMoved = false;
    private float cameraStopY = -10f; // The Y-position at which the camera should stop moving
    private Vector3 initialCameraPosition;
    private Vector3 playerStartGamePosition;

    // private void Awake()
    // {
    //     if (Instance != null) {
    //         DestroyImmediate(gameObject);
    //     } else {
    //         Instance = this;
    //     }
    // }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        // mainCamera = Camera.main;
    }


    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        ground = FindObjectOfType<Ground>();
        player = FindObjectOfType<Player>();
        //spawner = FindObjectOfType<Spawner>();

        initialGroundPosition = ground.transform.position;
        // initialCameraPosition = mainCamera.transform.position;
        playerStartGamePosition = player.transform.position;

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        // mainCamera.transform.position = initialCameraPosition;
        CameraController.cameraDirection = CameraDirection.DINO;
        ground.transform.position = initialGroundPosition;
        player.transform.position = playerStartGamePosition;

        player.ResetPlayer();

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        seaweed.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        if (ground != null)
        {
            ground.gameObject.SetActive(true);
            // ground.transform.position = initialGroundPosition;
            ground.ResetGround();
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;
        UpdateHiscore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        seaweed.gameObject.SetActive(false);
        ground.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        if (score >= 44 && score <= 55)
        {
            //spawner.gameObject.SetActive(false);
            // mainCamera.transform.Translate(Vector3.down * 0.0254f);
            CameraController.cameraDirection = CameraDirection.FB;
        }
        // Check for the score reaching 60
        if (score >= 60)
        {
            // RespawnObstacles(); // Call the method to respawn obstacles
            spawner.gameObject.SetActive(false);
            seaweed.gameObject.SetActive(true);
            player.CheckOutOfBounds();
        }

        if (score >= 30 && score <= 55)
        {
            spawner.gameObject.SetActive(false);
            seaweed.gameObject.SetActive(false);
        }

        UpdateHiscore();
    }

    public void RespawnObstacles()
    {
        // spawner.gameObject.SetActive(false);
        // player.gameObject.SetActive(false);

        // You may also reset the camera position if it was moved down
        // mainCamera.transform.Translate(Vector3.up * 0.0254f); // Use an appropriate value to reset the camera
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
