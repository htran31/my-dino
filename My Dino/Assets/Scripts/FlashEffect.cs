//using UnityEngine;
//using UnityEngine.UI;

//public class FlashEffect : MonoBehaviour
//{
//    public Image flashImage; 
//    public float flashDuration = 0.3f;
//    public float flickerInterval = 0.05f;
//    public Color flashColor = new Color(1f, 0f, 0f, 0.5f); 

//    private Color transparentColor;
//    private float flashTimer;
//    private float flickerTimer = 0f;
//    private bool isVisible = true;

//    void Start()
//    {
//        transparentColor = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
//        flashImage.color = transparentColor;
//    }

//    void Update()
//    {
//        if (flashTimer > 0)
//        {
//            flashTimer -= Time.deltaTime;
//            flickerTimer -= Time.deltaTime;

//            if (flickerTimer <= 0)
//            {
//                isVisible = !isVisible; // bật/tắt
//                flashImage.color = isVisible ? flashColor : transparentColor;
//                flickerTimer = flickerInterval;
//            }

//            // fade mượt dần về trong suốt ở cuối hiệu ứng
//            if (flashTimer < flashDuration * 0.3f)
//            {
//                float t = 1 - (flashTimer / (flashDuration * 0.3f));
//                flashImage.color = Color.Lerp(flashColor, transparentColor, t);
//            }
//        }
//        else
//        {
//            flashImage.color = transparentColor;
//        }
//    }

//    public void TriggerFlash( float index)
//    {
//        flashTimer = index;
//        flashImage.color = flashColor;
//        isVisible = true;
//        flickerTimer = 0f;
//    }
//}
