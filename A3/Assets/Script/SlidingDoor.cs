// SlidingDoor.cs
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public enum DoorColor { Red, Green, Blue }
    public DoorColor doorColor;

    public float openDistance = 3f;
    public float slideSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private bool playerInRange = false;

    public GameObject doorModel; // 门的可视化部分

    void Start()
    {
        closedPosition = doorModel.transform.position;
        openPosition = closedPosition + transform.right * openDistance;
    }

    void Update()
    {
        // 开关门动画
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        doorModel.transform.position = Vector3.MoveTowards(
            doorModel.transform.position, targetPosition, slideSpeed * Time.deltaTime);

        // 检测按键开门
        if (playerInRange && Input.GetKeyDown(KeyCode.C))
        {
            TryOpenDoor();
        }

        // 自动关门检测
        if (isOpen && !playerInRange)
        {
            CloseDoor();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
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
        if (isOpen) return;

        KeyManager keyManager = FindObjectOfType<KeyManager>();
        if (keyManager != null && keyManager.UseKey((KeyManager.KeyColor)doorColor))
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
    }

    void CloseDoor()
    {
        isOpen = false;
    }
}