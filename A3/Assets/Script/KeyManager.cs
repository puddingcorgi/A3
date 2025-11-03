// KeyManager.cs
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public enum KeyColor { None, Red, Green, Blue }
    public KeyColor currentKey = KeyColor.None;

    private int doorsOpened = 0;
    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        UpdateUI();
    }

    public void PickupKey(KeyColor keyColor)
    {
        if (currentKey == KeyColor.None)
        {
            currentKey = keyColor;
            UpdateUI();
        }
    }

    public bool UseKey(KeyColor requiredColor)
    {
        if (currentKey == requiredColor)
        {
            currentKey = KeyColor.None;
            doorsOpened++;
            UpdateUI();
            return true;
        }
        return false;
    }

    public int GetDoorsOpened()
    {
        return doorsOpened;
    }

    void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateKeyDisplay(currentKey);
            uiManager.UpdateDoorsOpened(doorsOpened);
        }
    }
}