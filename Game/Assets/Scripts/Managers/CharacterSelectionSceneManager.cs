using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterSelectionSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameData pData;

    [SerializeField]
    private Transform _playerPanel;

    [SerializeField]
    private int _playersAmount;

    [SerializeField]
    private List<GameObject> _playerPrefabs;
    public static CharacterSelectionSceneManager Instance { get; private set; }
    public event Action<int, GameObject> PlayerJoined = delegate { };
    public event Action<int, GameObject> PlayerLeft = delegate { };
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //DontDestroyOnLoad(PlayerInputManager.instance.gameObject);
    }

    private void OnPlayerJoined(UnityEngine.InputSystem.PlayerInput playerInput)
    {
        PlayerJoined?.Invoke(playerInput.playerIndex, playerInput.gameObject);

        playerInput.gameObject.transform.SetParent(_playerPanel);

        _playersAmount++;
        if (_playersAmount == PlayerInputManager.instance.maxPlayerCount)
        {
            PlayerInputManager.instance.DisableJoining();
        }

        GameData.Instance.AddOrSetPlayerConfiguration(playerInput.playerIndex, playerInput.devices[0].device);

        Debug.LogWarning($"Player Joined");
    }
    private void OnPlayerLeft(UnityEngine.InputSystem.PlayerInput playerInput)
    {
        PlayerLeft?.Invoke(playerInput.playerIndex, playerInput.gameObject);

        _playersAmount--;
        if (_playersAmount < PlayerInputManager.instance.maxPlayerCount)
        {
            PlayerInputManager.instance.EnableJoining();
        }

        //GameData.Instance.RemovePlayerConfiguration(playerInput.playerIndex);

        Debug.LogWarning($"Player Left");
    }

    public void StartGameOnAllPlayersReady(bool allPlayersReady, int currentAmountOfPlayers)
    {
        if (allPlayersReady && (pData.IsTesting || PlayerInputManager.instance.maxPlayerCount == currentAmountOfPlayers))
        {
            pData.PlayersPlaying = currentAmountOfPlayers;
            // TODO: Depending on amount of initially selected players and so probably the amount of playing
            // load according scene
            // Will probably have 2 game scenes for 2P and 4P modes.
            SceneManager.LoadScene("TestScene");
        }
    }
}
