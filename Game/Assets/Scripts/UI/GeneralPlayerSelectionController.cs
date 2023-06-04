using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralPlayerSelectionController : MonoBehaviour
{
    [SerializeField]
    private CharacterSelectionSceneManager _selectionManager;

    public Transform playerBoxesParent;
    [SerializeField]
    private List<PlayerBox> _playerBoxes;
    [SerializeField]
    private Dictionary<int, PlayerUIController> _playerControllers;

    [SerializeField]
    private List<Sprite> _charactersTextures;

    [SerializeField]
    private CharacterImageSetter _characterImageSetter;

    private void Awake()
    {
        if (_selectionManager == null) _selectionManager = CharacterSelectionSceneManager.Instance;

        if (playerBoxesParent == null) playerBoxesParent = transform.GetChild(0);
        _playerControllers = new();
        if (_playerBoxes.Count == 0)
        {
            foreach (Transform child in playerBoxesParent)
            {
                _playerBoxes.Add(child.GetComponent<PlayerBox>());
            }
        }
        if (_characterImageSetter == null) _characterImageSetter = transform.GetChild(2).GetComponent<CharacterImageSetter>();
        if (_characterImageSetter == null) Debug.LogError($"Check! Character Image Setter is MISSING!");
        _characterImageSetter.SetImages(_charactersTextures);
    }
    private void OnEnable()
    {
        _selectionManager.PlayerJoined += OnPlayerJoined;
        _selectionManager.PlayerLeft += OnPlayerLeft;
    }
    private void OnDisable()
    {
        _selectionManager.PlayerJoined -= OnPlayerJoined;
        _selectionManager.PlayerLeft -= OnPlayerLeft;
    }
    public void OnPlayerJoined(int id, GameObject player)
    {
        PlayerUIController pController = player.GetComponent<PlayerUIController>();
        pController.Select += OnSelected;
        pController.Ready += OnReady;
        _playerBoxes[id].JoinPlayer();
        _playerControllers.Add(id, pController);
        Debug.Log($"Name: {player.name}, Controller: {player.GetComponent<PlayerUIController>()}");
    }
    public void OnPlayerLeft(int id, GameObject player)
    {
        player.GetComponent<PlayerUIController>().Select -= OnSelected;
        player.GetComponent<PlayerUIController>().Ready -= OnReady;
    }
    private void OnSelected(int playerID, int buttonID)
    {
        _playerBoxes[playerID].Select(_charactersTextures[buttonID]);
    }

    private void OnReady(int playerID, bool isReady)
    {
        _playerBoxes[playerID].Ready(isReady);
        Debug.Log($"Player State Ready: {isReady}");

        _selectionManager.StartGameOnAllPlayersReady(CheckAllPlayersReady(), _playerControllers.Count);
    }

    private bool CheckAllPlayersReady()
    {
        return _playerControllers.Values.All(controller => controller.State == PlayerUIController.PlayerState.Ready);
    }
}
