using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public KeyManager.KeyColor doorColor;
    public float openAngle = 90f;
    public float rotationSpeed = 90f;
    public bool opensOutward = true; 

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    private bool playerInRange = false;
    private bool isMoving = false;
    private float autoCloseTimer = 0f;
    public float autoCloseDelay = 3f; 

    void Start()
    {
        closedRotation = transform.rotation;

        float angle = opensOutward ? openAngle : -openAngle;
        openRotation = closedRotation * Quaternion.Euler(0, angle, 0);
    }

    void Update()
    {

        if (isMoving)
        {
            Quaternion targetRotation = isOpen ? openRotation : closedRotation;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isMoving = false;
                transform.rotation = targetRotation;
            }
        }


        if (isOpen && !playerInRange && !isMoving)
        {
            autoCloseTimer += Time.deltaTime;
            if (autoCloseTimer >= autoCloseDelay)
            {
                CloseDoor();
                autoCloseTimer = 0f;
            }
        }
        else
        {
            autoCloseTimer = 0f;
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.C) && !isOpen && !isMoving)
        {
            TryOpenDoor();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            autoCloseTimer = 0f; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void TryOpenDoor()
    {
        KeyManager keyManager = Object.FindAnyObjectByType<KeyManager>();
        if (keyManager != null && keyManager.currentKey == doorColor)
        {
            keyManager.UseKey(doorColor);
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        isMoving = true;
    }

    void CloseDoor()
    {
        isOpen = false;
        isMoving = true;
    }
}