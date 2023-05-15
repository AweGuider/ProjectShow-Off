using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject amountButtons;

    [SerializeField]
    private List<PlayerInput> connectedPlayers = new();

    // Start is called before the first frame update
    void Start()
    {
        //transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
        PlayerInputManager.instance.onPlayerLeft += OnPlayerLeft;
    }

    private void OnDisable()
    {
        PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
        PlayerInputManager.instance.onPlayerLeft -= OnPlayerLeft;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        connectedPlayers.Add(playerInput);
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        connectedPlayers.Remove(playerInput);
    }

    public List<PlayerInput> GetConnectedPlayers()
    {
        return connectedPlayers;
    }
}
