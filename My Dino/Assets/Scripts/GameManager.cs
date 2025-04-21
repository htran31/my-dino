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
    public TextMeshProUGUI startGameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public Button retryButton;
    public Button startButton;
    private Player player;
    private BigDino bigDino;
    public Spawner spawner;
    public Parallax seaweed;
    public FallingSpawner heart;
    private Ground ground;
    private Vector3 initialGroundPosition;
    public CloudSpawner cloudSpawner;

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.055f;
    public float gameSpeed { get; private set; }
    public float score { get; private set; }
    public float Score => score;
    // private Camera mainCamera;
    private bool cameraMoved = false;
    private float cameraStopY = -10f; // The Y-position at which the camera should stop moving
    private Vector3 initialCameraPosition;
    private Vector3 playerStartGamePosition;
    private bool isGameRunning = false;
    public bool gameOver = false;
    private bool hasSpawnedBigDino = false;
    public FallingBackground fallingBackground1;
    public FallingBackground fallingBackground2;




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
        bigDino = FindObjectOfType<BigDino>();

        initialGroundPosition = ground.transform.position;
        playerStartGamePosition = player.transform.position;

        // Show only start UI
        startButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        hiscoreText.gameObject.SetActive(true);
        seaweed.gameObject.SetActive(false);
        heart.gameObject.SetActive(false);
        bigDino.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        score = 0f;
        scoreText.text = "00000";

        player.gameObject.SetActive(false);
        ground.gameObject.SetActive(true);
        // ground.ResetGround();
        ground.DisableScroll();
        fallingBackground1.gameObject.SetActive(true);
        fallingBackground2.gameObject.SetActive(true);


        UpdateHiscore();
    }

    public void OnStartButtonClicked()
    {
        startButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        heart.gameObject.SetActive(false);
        bigDino.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        hiscoreText.gameObject.SetActive(true);
        
        NewGame();
    }

    public void NewGame()
    {
        SoundManager.Instance.PlayStartGameSound();
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        FallingObstacle[] fallingObstacles = FindObjectsOfType<FallingObstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        foreach (var obstacle in fallingObstacles)
        {
            Destroy(obstacle.gameObject);
        }

        isGameRunning = true;

        // ground.ResetGround();
        ground.EnableScroll();

        // mainCamera.transform.position = initialCameraPosition;
        CameraController.cameraDirection = CameraDirection.DINO;
        ground.transform.position = initialGroundPosition;
        player.transform.position = playerStartGamePosition;

        fallingBackground1.ResetBackground();
        fallingBackground2.ResetBackground();
        cloudSpawner.ResetClouds();


        player.gameObject.SetActive(true);
        player.ResetPlayer();
        spawner.gameObject.SetActive(true);
        fallingBackground1.gameObject.SetActive(true);
        fallingBackground2.gameObject.SetActive(true);

        seaweed.gameObject.SetActive(false);
        heart.gameObject.SetActive(false);
        bigDino.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(false);
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
        // enabled = true;
        gameOver = false;
        hasSpawnedBigDino = false;
        UpdateHiscore();
    }

    public void GameOver()
    {
        SoundManager.Instance.PlayGameOveround();
        gameSpeed = 0f;
        gameOver = true;
        // enabled = false;
        isGameRunning = false;

        player.gameObject.SetActive(false);
        bigDino.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        seaweed.gameObject.SetActive(false);
        heart.gameObject.SetActive(false);
        ground.gameObject.SetActive(true);
        ground.DisableScroll();

        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(false);
        UpdateHiscore();
    }


    private void Update()
    {
        if (!isGameRunning)
        {
            if (Input.GetKeyDown(KeyCode.Space) && gameOver == true)
            {
                OnStartButtonClicked();
            }
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) 
        {
            SoundManager.Instance.PlayClickSound();
        }

        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        UpdateBasedOnScore();
        UpdateHiscore();
    }

    public void RotateUnderGround()
    {
        UnderGround underGround = FindObjectOfType<UnderGround>();
        if (underGround != null)
        {
            underGround.Rotate();
        }
    }


    public void UpdateBasedOnScore()
    {
        if (score >= 80 && score <= 100)
        {
            spawner.gameObject.SetActive(false);
            seaweed.gameObject.SetActive(false);
        }
        if (score >= 100 && score <= 110)
        {
            //spawner.gameObject.SetActive(false);
            // mainCamera.transform.Translate(Vector3.down * 0.0254f);
            CameraController.cameraDirection = CameraDirection.FB;
        }
        if (score >= 100)
        {
            player.CheckOutOfBounds();
        }
        if (score >= 110 && score <= 200)
        {
            spawner.gameObject.SetActive(false);
            seaweed.gameObject.SetActive(true);
        }

        if (score >= 200 && score <= 220)
        {
            fallingBackground1.gameObject.SetActive(true);
            fallingBackground2.gameObject.SetActive(true);
            fallingBackground1.StartFalling();
            fallingBackground2.StartFalling();
            seaweed.gameObject.SetActive(false);
        }


        UnderGround underGround = FindObjectOfType<UnderGround>();
        if (score >= 200 && score <= 220)
        {
            if (underGround != null)
            {
                underGround.Rotate();
            }
        }
        else
        {
            if (underGround != null)
            {
                underGround.ResetRotation();
            }
        }

        if (score >= 250 && score <= 400 && !hasSpawnedBigDino)
        {
            hasSpawnedBigDino = true;
            bigDino.gameObject.SetActive(true);
            seaweed.gameObject.SetActive(false);
            // player.transform.position = new Vector3(0, 0, 0);
        }

        if (score >= 252 && score <= 400)
        {
            seaweed.gameObject.SetActive(false);
            heart.gameObject.SetActive(true);
        }
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
