using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyManager.KeyColor keyColor;
    public float respawnTime = 10f;

    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;

    private Renderer rend;
    private bool isActive = true;

    void Start()
    {
        rend = GetComponent<Renderer>();
        SetupAppearance();
    }

    void Update()
    {
        if (isActive)
        {
            // Ðý×ªÐ§¹û
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
            if (keyManager != null)
            {
                keyManager.PickupKey(keyColor);
                Pickup();
            }
        }
    }

    void Pickup()
    {
        isActive = false;
        rend.enabled = false;
        GetComponent<Collider>().enabled = false;

        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        isActive = true;
        rend.enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}