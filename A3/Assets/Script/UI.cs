// UIManager.cs
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text keyText;
    public Text doorsOpenedText;
    public Slider speedSlider;

    private ClickToMove playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<ClickToMove>();
        if (speedSlider != null && playerMovement != null)
        {
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
            playerMovement.SetSpeed(value * 5f); 
        }
    }
}