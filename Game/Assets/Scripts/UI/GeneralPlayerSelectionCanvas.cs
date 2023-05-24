using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPlayerSelectionCanvas : MonoBehaviour
{
    
    [SerializeField] CharacterSelectionManager _selectionManager;

    public Transform playerBoxesParent;
    [SerializeField] List<PlayerBox> _playerBoxes;

    private void Awake()
    {
        if (_selectionManager == null) _selectionManager = CharacterSelectionManager.instance;
        if (playerBoxesParent == null) playerBoxesParent = transform.GetChild(0);

        if (_playerBoxes.Count == 0)
        {
            foreach (Transform child in playerBoxesParent)
            {
                _playerBoxes.Add(child.GetComponent<PlayerBox>());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    private void OnEnable()
    {
        _selectionManager.PlayerJoined += OnPlayerJoined;
    }

    private void OnDisable()
    {
        _selectionManager.PlayerJoined -= OnPlayerJoined;

    }

    public void OnPlayerJoined(int id)
    {
        _playerBoxes[id].JoinPlayer();
    }
}
