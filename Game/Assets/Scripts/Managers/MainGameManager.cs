using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    private int playerCount;

    [SerializeField]
    private List<UnityEngine.InputSystem.PlayerInput> players;

    [SerializeField]
    private List<GameObject> playerPrefabs;

    [SerializeField]
    private List<Transform> playerPositions;
    [SerializeField]
    private List<LayerMask> playerLayers;
    private bool kPressed;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInputManager.instance.playerPrefab = playerPrefabs[0];
        //Debug.LogWarning(GameData.Instance.PlayersConfigurations.Count);
        for (int i = 0; i < GameData.Instance.PlayersPlaying; i++)
        {
            var pConfig = GameData.Instance.PlayersConfigurations[i];
            // TODO: Fix later and add prefabs
            int prefabIndex = pConfig.character > (playerPrefabs.Count - 1) ? 0 : pConfig.character;
            PlayerInputManager.instance.playerPrefab = playerPrefabs[prefabIndex];

            PlayerInputManager.instance.JoinPlayer(pairWithDevice: pConfig.device);
            Debug.LogWarning($"Player {i} using {pConfig.device} chose character {pConfig.character}");

        }
        players = new();
        //playerPrefabs = new();
        //Debug.Log(PlayerInputManager.instance.playerCount);
        //foreach (InputDevice dev in InputSystem.devices)
        //{
        //    Debug.Log(dev.name);
        //    Debug.Log(dev.deviceId);
        //    Debug.Log(dev.layout);
        //}
        //PlayerInputManager.instance.JoinPlayer(pairWithDevice: )

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K) & !kPressed)
        //{
        //    PlayerInputManager.instance.playerPrefab = playerPrefabs[playerCount];
        //    PlayerInputManager.instance.JoinPlayer();
        //    //for (int i = 0; i < playerCount; i++)
        //    //{
        //    //    if (i > playerPrefabs.Count) break;
        //    //    PlayerInputManager.instance.playerPrefab = playerPrefabs[i];
        //    //    PlayerInputManager.instance.JoinPlayer();
        //    //}
        //    kPressed = true;
        //    Debug.Log("BUTTON PRESSED");
        //}
        //kPressed = false;
    }

    //private void OnPlayerJoined()
    //{
    //    playerCount++;
    //}

    //private void OnPlayerLeft()
    //{
    //    playerCount--;
    //}

    //private void OnEnable()
    //{
    //    PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    //    PlayerInputManager.instance.onPlayerLeft += OnPlayerLeft;
    //}

    //private void OnDisable()
    //{
    //    PlayerInputManager.instance.onPlayerJoined -= OnPlayerJoined;
    //    PlayerInputManager.instance.onPlayerLeft -= OnPlayerLeft;
    //}

    private void OnPlayerJoined(UnityEngine.InputSystem.PlayerInput pInput)
    {
        players.Add(pInput);
        playerCount++;

        Transform player = pInput.transform;

        player.position = playerPositions[pInput.playerIndex].transform.position;

        int layerToAdd = (int)Mathf.Log(playerLayers[playerCount - 1].value, 2);

        // Set the layer
        player.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;

        //Add the layer
        player.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;

        //player.GetComponentInChildren<InputHandler>
    }

    private void OnPlayerLeft(UnityEngine.InputSystem.PlayerInput pInput)
    {
        players.Remove(pInput);
        playerCount--;
    }
}
