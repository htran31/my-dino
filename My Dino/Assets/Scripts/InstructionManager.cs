using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject openButton;
    public Button closeButton;
    public TMP_Text instructionText;

    [TextArea]
    public string[] randomInstructions;

    void Start()
    {
        openButton.SetActive(true);
        instructionPanel.SetActive(false);

        openButton.GetComponent<Button>().onClick.AddListener(OpenInstructions);
        closeButton.onClick.AddListener(CloseInstructions);
    }
    void OpenInstructions()
    {
        openButton.SetActive(true);
        instructionPanel.SetActive(true);

        if (randomInstructions.Length > 0)
        {
            int randomIndex = Random.Range(0, randomInstructions.Length);
            instructionText.text = randomInstructions[randomIndex];
        }
    }
    void CloseInstructions()
    {
        openButton.SetActive(true);
        instructionPanel.SetActive(false);
    }
}
