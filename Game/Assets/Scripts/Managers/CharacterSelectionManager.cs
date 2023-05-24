using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelectionManager : MonoBehaviour
{
    [SerializeField]
    private InputAction _playerInput;

    [SerializeField]
    private Transform _playerPanel;

    [SerializeField]
    private List<UnityEngine.InputSystem.PlayerInput> _connectedPlayers = new();
    [SerializeField]
    private int _playersAmount;

    bool _kPressed;
    [SerializeField]
    private List<GameObject> _cursorPrefabs;
    public static CharacterSelectionManager instance
    {
        get;
        private set;

    }
    public event Action<int> PlayerJoined = delegate { };


    private void Awake()
    {
        _connectedPlayers = new();
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform.parent.gameObject.SetActive(false);

        //PlayerInputManager.instance.JoinPlayerFromAction()

        //PlayerInputManager.instance.playerPrefab = cursorPrefabs[0];
        //PlayerInputManager.instance.JoinPlayer();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K) & !_kPressed)
        //{
        //    PlayerInputManager.instance.JoinPlayer();
        //    _kPressed = true;
        //    Debug.Log("BUTTON PRESSED");
        //}
        //_kPressed = false;
    }



    private void OnJoinActionTriggered(InputAction.CallbackContext context)
    {
        PlayerInputManager.instance.playerPrefab = _cursorPrefabs[0];
        PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);
    }

    private void OnPlayerJoined(UnityEngine.InputSystem.PlayerInput playerInput)
    {
        PlayerJoined.Invoke(_playersAmount);

        playerInput.gameObject.transform.SetParent(_playerPanel);

        _playersAmount++;

        if (_playersAmount == PlayerInputManager.instance.maxPlayerCount)
        {
            PlayerInputManager.instance.DisableJoining();
        }

        _connectedPlayers.Add(playerInput);

        Debug.Log($"Player Joined");
    }

    private void OnPlayerLeft(UnityEngine.InputSystem.PlayerInput playerInput)
    {
        _connectedPlayers.Remove(playerInput);
        _playersAmount--;

        if (_playersAmount < PlayerInputManager.instance.maxPlayerCount)
        {
            PlayerInputManager.instance.EnableJoining();
        }
        Debug.Log($"Player Left");

    }

    public List<UnityEngine.InputSystem.PlayerInput> GetConnectedPlayers()
    {
        return _connectedPlayers;
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
