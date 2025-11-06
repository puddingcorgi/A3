using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text keyText;
    public Text doorsOpenedText;
    public Slider speedSlider;

    private Playermove playerMovement;
    void Start()
    {
        playerMovement = Object.FindAnyObjectByType<Playermove>();
        if (speedSlider != null )
        {
            speedSlider.minValue = 0.5f;  
            speedSlider.maxValue = 4.0f;  

            speedSlider.value = 1.0f;

            speedSlider.onValueChanged.AddListener(OnSpeedChanged);
        }

        UpdateKeyDisplay(KeyManager.KeyColor.None);
        UpdateDoorsOpened(0);
    }

    public void UpdateKeyDisplay(KeyManager.KeyColor keyColor)
    {
        if (keyText != null)
        {
            string colorName = keyColor.ToString();
            keyText.text = "Key: " + (keyColor == KeyManager.KeyColor.None ? "null" : colorName);
        }
    }

    public void UpdateDoorsOpened(int count)
    {
        if (doorsOpenedText != null)
        {
            doorsOpenedText.text = "Opened " + count;
        }
    }

    void OnSpeedChanged(float value)
    {
        if (playerMovement != null)
        {
            playerMovement.SetSpeed(value);
        }
    }
}