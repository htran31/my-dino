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
    private Ground ground;
    private Vector3 initialGroundPosition;

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
        bigDino.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        hiscoreText.gameObject.SetActive(true);

        NewGame();
    }

    public void NewGame()
    {
        SoundManager.Instance.PlayGameOveround();
        player.ResetPlayer();
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
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


        //player.ResetPlayer();

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        fallingBackground1.gameObject.SetActive(true);
        fallingBackground2.gameObject.SetActive(true);

        seaweed.gameObject.SetActive(false);
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
        ground.gameObject.SetActive(true);
        ground.DisableScroll();

        //  if (isGameRunning == false)
        //     {
        //         if (Input.GetKeyDown(KeyCode.Space))
        //         {
        //             OnStartButtonClicked();
        //         }
        //         return;
        //     }

        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(false);
        UpdateHiscore();
        //  if (Input.GetKeyDown(KeyCode.Space) && gameOver == false)
        //     {
        //         NewGame();
        //     }
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

        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        player.CheckOutOfBounds();

        if (score >= 44 && score <= 55)
        {
            //spawner.gameObject.SetActive(false);
            // mainCamera.transform.Translate(Vector3.down * 0.0254f);
            CameraController.cameraDirection = CameraDirection.FB;
        }
        // Check for the score reaching 60
        if (score >= 60 && score <= 90)
        {
            // RespawnObstacles(); // Call the method to respawn obstacles
            spawner.gameObject.SetActive(false);
            seaweed.gameObject.SetActive(true);
        }

        if (score >= 30 && score <= 55)
        {
            spawner.gameObject.SetActive(false);
            seaweed.gameObject.SetActive(false);
        }

        if (score >= 80 && score <= 110)
        {
            fallingBackground1.gameObject.SetActive(true);
            fallingBackground2.gameObject.SetActive(true);
            fallingBackground1.StartFalling();
            fallingBackground2.StartFalling();
            seaweed.gameObject.SetActive(false);
        }


        UnderGround underGround = FindObjectOfType<UnderGround>();
        if (score >= 80 && score <= 110)
        {
            fallingBackground1.gameObject.SetActive(true);
            fallingBackground2.gameObject.SetActive(true);
            fallingBackground1.StartFalling();
            fallingBackground2.StartFalling();
            seaweed.gameObject.SetActive(false);

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


        if (score >= 160 && score <= 200 && !hasSpawnedBigDino)
        {
            hasSpawnedBigDino = true;
            bigDino.gameObject.SetActive(true);
            seaweed.gameObject.SetActive(false);
            // player.transform.position = new Vector3(0, 0, 0);
        }

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
