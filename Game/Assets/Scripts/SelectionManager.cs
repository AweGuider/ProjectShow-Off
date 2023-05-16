using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject amountButtons;

    [SerializeField]
    private InputAction playerInput;

    [SerializeField]
    private Transform cursorPanel;

    [SerializeField]
    private List<UnityEngine.InputSystem.PlayerInput> connectedPlayers = new();
    [SerializeField]
    private int playersAmount;

    bool kPressed;
    [SerializeField]
    private List<GameObject> cursorPrefabs;

    private void Awake()
    {
        connectedPlayers = new();
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform.parent.gameObject.SetActive(false);

        PlayerInputManager.instance.playerPrefab = cursorPrefabs[0];
        PlayerInputManager.instance.JoinPlayer();


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) & !kPressed)
        {
            PlayerInputManager.instance.JoinPlayer();
            kPressed = true;
            Debug.Log("BUTTON PRESSED");
        }
        kPressed = false;
    }



    private void OnPlayerJoined(UnityEngine.InputSystem.PlayerInput playerInput)
    {
        //GameObject playerCursor = Instantiate(cursorPrefabs[0], cursorPanel);

        playerInput.gameObject.transform.SetParent(cursorPanel);
        connectedPlayers.Add(playerInput);
        playersAmount++;
        Debug.Log($"Player Joined");

    }

    private void OnPlayerLeft(UnityEngine.InputSystem.PlayerInput playerInput)
    {
        connectedPlayers.Remove(playerInput);
        playersAmount--;
        Debug.Log($"Player Left");

    }

    public List<UnityEngine.InputSystem.PlayerInput> GetConnectedPlayers()
    {
        return connectedPlayers;
    }

    //private void OnEnable()
    //{
    //    //PlayerInputManager.instance.
    //    PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    //    PlayerInputManager.instance.onPlayerLeft += OnPlayerLeft;
    //}

    //private void OnDisable()
    //{
    //    PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    //    PlayerInputManager.instance.onPlayerLeft -= OnPlayerLeft;
    //}
}
