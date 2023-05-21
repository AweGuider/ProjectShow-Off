using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int playerCount;

    [SerializeField]
    private List<UnityEngine.InputSystem.PlayerInput> players;

    [SerializeField]
    private List<GameObject> playerPrefabs;
    private bool kPressed;

    // Start is called before the first frame update
    void Start()
    {
        players = new();
        //playerPrefabs = new();
        foreach (InputDevice dev in InputSystem.devices)
        {
            Debug.Log(dev.name);
            Debug.Log(dev.deviceId);
            Debug.Log(dev.layout);
        }
        //InputSystem.
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
    }

    private void OnPlayerLeft(UnityEngine.InputSystem.PlayerInput pInput)
    {
        players.Remove(pInput);
        playerCount--;
    }
}
