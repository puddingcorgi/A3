// KeyPickup.cs
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyManager.KeyColor keyColor;
    public float respawnTime = 10f;

    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;

    private Renderer rend;
    private Collider keyCollider;
    private bool isActive = true;
    private Vector3 startPosition;

    void Start()
    {
        rend = GetComponent<Renderer>();
        keyCollider = GetComponent<Collider>();
        startPosition = transform.position;
        SetupAppearance();
    }

    void Update()
    {
        if (isActive)
        {
            // 钥匙浮动效果
            float newY = startPosition.y + Mathf.Sin(Time.time * 2f) * 0.2f;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // 钥匙旋转效果
            transform.Rotate(0, 50 * Time.deltaTime, 0);
        }
    }

    void SetupAppearance()
    {
        switch (keyColor)
        {
            case KeyManager.KeyColor.Red:
                rend.material = redMaterial;
                break;
            case KeyManager.KeyColor.Green:
                rend.material = greenMaterial;
                break;
            case KeyManager.KeyColor.Blue:
                rend.material = blueMaterial;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            KeyManager keyManager = other.GetComponent<KeyManager>();
            if (keyManager != null && keyManager.currentKey == KeyManager.KeyColor.None)
            {
                keyManager.PickupKey(keyColor);
                PickupKey();
            }
        }
    }

    void PickupKey()
    {
        isActive = false;
        rend.enabled = false;
        if (keyCollider != null)
            keyCollider.enabled = false;

        Invoke("RespawnKey", respawnTime);
    }

    void RespawnKey()
    {
        isActive = true;
        rend.enabled = true;
        if (keyCollider != null)
            keyCollider.enabled = true;
    }
}