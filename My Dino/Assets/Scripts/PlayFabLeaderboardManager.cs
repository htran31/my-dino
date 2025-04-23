
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class PlayFabLeaderboardManager : MonoBehaviour
{
    public static PlayFabLeaderboardManager Instance { get; private set; }
    [Header("UI References")]
    public TMP_InputField playerNameInput;
    //public int scoreInt;
    public Button PlayGameButton;
    public Button getLeaderboardButton;
    public GameObject leaderboardEntryPrefab;
    public Transform leaderboardContainer;
    private TouchScreenKeyboard keyboard;

    public GameObject leaderboardPanel;

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
    }
//    private void Update()
//    {
//#if UNITY_WEBGL && !UNITY_EDITOR
//        if (keyboard != null && keyboard.status != TouchScreenKeyboard.Status.Canceled)
//        {
//            // Gán text từ bàn phím vào TMP_InputField
//            if (playerNameInput.text != keyboard.text)
//            {
//                playerNameInput.text = keyboard.text;
//                playerNameInput.caretPosition = keyboard.text.Length;
//            }
//        }
//#endif
//    }

    private void Start()
    {
        LoginWithCustomID();

        PlayGameButton.onClick.AddListener(OnButtonClick);
        playerNameInput.onSelect.AddListener(ShowKeyboard);
        getLeaderboardButton.onClick.AddListener(OnGetLeaderboard);
    }

    void LoginWithCustomID()
    {
        string customId;

        if (PlayerPrefs.HasKey("custom_id"))
        {
            customId = PlayerPrefs.GetString("custom_id");
        }
        else
        {
            customId = System.Guid.NewGuid().ToString(); 
            PlayerPrefs.SetString("custom_id", customId);
        }
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, 
            result => Debug.Log("Đăng nhập PlayFab thành công"), 
            error => Debug.LogError("Lỗi đăng nhập PlayFab: " + error.GenerateErrorReport()));
    }
    public void OnSubmitScore( int scoreInt)
    {
        //string playerName = playerNameInput.text;
        int score = scoreInt;

        //SetDisplayName(playerName, () =>
        //{
        //    SubmitScore(score);
        //});
        SubmitScore(score);
    }

    void SetDisplayName(string name, Action onSuccess)
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = name };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            result => {
                Debug.Log("Tên người chơi đã cập nhật");
                onSuccess?.Invoke();
            },
            error => Debug.LogError("Lỗi khi cập nhật tên: " + error.GenerateErrorReport()));
    }

    void SubmitScore(int score)
    {

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate { StatisticName = "HighScore", Value = score }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request,
            result => Debug.Log("Gửi điểm thành công"),
            error => Debug.LogError("Lỗi gửi điểm: " + error.GenerateErrorReport()));
    }

    void OnGetLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        foreach (Transform child in leaderboardContainer)
            Destroy(child.gameObject);

        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScore",
            StartPosition = 0,
            MaxResultsCount = 30
        };

        PlayFabClientAPI.GetLeaderboard(request,
            result =>
            {
                foreach (var entry in result.Leaderboard)
                {
                    GameObject entryObj = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
                    //Text entryText = entryObj.GetComponent<Text>();
                    //string displayName = string.IsNullOrEmpty(entry.DisplayName) ? entry.PlayFabId : entry.DisplayName;
                    //entryText.text = $"{entry.Position + 1}. {displayName} - {entry.StatValue}";
                    entryObj.transform.Find("Rank").GetComponent<TMP_Text>().text = $"#{entry.Position + 1}";
                    entryObj.transform.Find("PlayerName").GetComponent<TMP_Text>().text = entry.DisplayName ?? "Unknown";
                    entryObj.transform.Find("Score").GetComponent<TMP_Text>().text = entry.StatValue.ToString();
                    entryObj.SetActive(true);
                }
            },
            error => Debug.LogError("Lỗi lấy bảng xếp hạng: " + error.GenerateErrorReport()));
    }

    void OnButtonClick()
    {
        if (ValidateInput())
        {
            string playerName = playerNameInput.text;
            SetDisplayName(playerName, () =>
            {
                GameManager.Instance.OnStartButtonClicked();
                playerNameInput.gameObject.SetActive(false);
            });
        }
        else
        {
            playerNameInput.text = "Invalid input. Please try again.";
        }
    }

    bool ValidateInput()
    {
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            return false; 
        }

        return true;  
    }

    void ShowKeyboard(string text)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
                keyboard = TouchScreenKeyboard.Open(playerNameInput.text, TouchScreenKeyboardType.Default, false, false, false, false);
#endif
    }
}
