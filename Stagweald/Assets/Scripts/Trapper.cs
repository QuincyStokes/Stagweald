
using TMPro;
using UnityEngine;

public class Trapper : MonoBehaviour
{
    [Header("References")]
    public Collider interactionCollider;
    public PlayerMovement playerMovement;
    public CameraMovement cameraMovement;
    public Crossbow crossbow;
    

    [Header("Keybinds")]
    public KeyCode interactionKeyCode;


    [Header("UI")]
    public GameObject shopMenu;
    public GameObject interactionUI;
    public GameObject inventoryUI;
    public Vector3 trapperInventoryPosition;
    private Vector3 baseInventoryPosition;
    public GameObject inventoryPositionHolder;


    private bool playerInRange;
    private Collider player;

    //walk into range of trapper man, presse E to interact with him
    //when you do, dialogue box/menu appears, maybe camera moves towards trapper man, locks player movement

    void Start()
    {
        shopMenu.SetActive(false);
        baseInventoryPosition = inventoryUI.transform.position;
    }

    

    void Update()
    {
        if(playerInRange && Input.GetKeyDown(interactionKeyCode))
        {
            //player is within range and pressed E. 
            OpenShop();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other; 
            //enable E pressing
            interactionUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null; 
            //enable E pressing
            interactionUI.SetActive(false);
        }
    }

    void OpenShop()
    {
        //need to lock player movement, menu appears, maybe camera moves
       
        playerMovement.enabled = false;
        cameraMovement.enabled = false;
        crossbow.enabled = false;
        shopMenu.SetActive(true);
        inventoryUI.SetActive(true);
        inventoryUI.transform.position = inventoryPositionHolder.transform.position;

        //need to unlock cursor, make it visible, disable camera script
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        interactionUI.SetActive(false);


    }


    public void CloseShop()
    {
        
        playerMovement.enabled = true;
        cameraMovement.enabled = true;
        crossbow.enabled = true;
        shopMenu.SetActive(false);
        inventoryUI.SetActive(true);
        inventoryUI.transform.position = baseInventoryPosition;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interactionUI.SetActive(false);

    }
}
