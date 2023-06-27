using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIController : MonoBehaviour
{
    private int _playerID;
    private int _selectedCharacterIndex;

    [SerializeField]
    private GameObject canvas;
    private CanvasGroup _canvasGroup;

    public Transform charactersParent;
    [SerializeField]
    private Dictionary<GameObject, int> _characters;

    [SerializeField]
    private PlayerState _state;
    private PlayerState _stateOld;
    public PlayerState State { get => _state;}

    public event Action<int, int> Select = delegate { };
    public event Action<int, bool> Ready = delegate { };

    public enum PlayerState
    {
        NotReady,
        Ready
    }
    private void Awake()
    {
        _playerID = transform.GetSiblingIndex();
        if (_canvasGroup == null && canvas != null) _canvasGroup = canvas.GetComponent<CanvasGroup>();

        //if (charactersParent == null) charactersParent = transform.GetChild(0).transform.GetChild(0);

        //_characters = new();
        //for (int i = 0; i < charactersParent.childCount; i++)
        //{
        //    GameObject button = charactersParent.GetChild(i).gameObject;
        //    _characters.Add(button, i);
        //}
    }

    private void Start()
    {
        _state = PlayerState.NotReady;
    }
    private void OnEnable()
    {

        foreach (Transform pCharacterWindow in canvas.transform)
        {
            pCharacterWindow.gameObject.SetActive(false);
        }

        canvas.transform.GetChild(_playerID).gameObject.SetActive(true);
    }
    public void OnButtonSelect(int buttonID)
    {
        _selectedCharacterIndex = buttonID;
        //Debug.Log($"UIController: Button ID {buttonID}");
        Select?.Invoke(_playerID, buttonID);
    }
    public void OnButtonClick()
    {
        bool isReady = _state == PlayerState.Ready;
        Ready?.Invoke(_playerID, isReady);
        if (isReady)
        {
            _canvasGroup.gameObject.SetActive(false);
            GameData.Instance.AddOrSetPlayerConfiguration(_playerID, character: _selectedCharacterIndex);
        }
    }

    private void OnDisable()
    {
        //foreach (Transform button in charactersParent)
        //{
        //    button.GetComponent<CharacterButton>().Select -= OnButtonSelect;
        //}
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            float value = context.action.ReadValue<float>();
            //Debug.Log(context.action.ReadValue<float>());

            UpdateStateOnValue(value);

            if (State == PlayerState.NotReady)
            {
                _canvasGroup.gameObject.SetActive(true);
            }
            //UpdateStateLogic();
        }

    }

    private void UpdateStateOnValue(float value)
    {
        switch (State)
        {
            case PlayerState.NotReady:
                if (value > 0)
                {
                    _state += 1;
                }
                break;
            case PlayerState.Ready:
                if (value < 0)
                {
                    _state -= 1;
                }
                break;
        }
    }

    public void UpdateStateLogic()
    {
        switch (State)
        {
            case PlayerState.NotReady:
                _canvasGroup.gameObject.SetActive(true);

                //_canvasGroup.interactable = true;
                break;
            case PlayerState.Ready:

                //_canvasGroup.interactable = false;
                _canvasGroup.gameObject.SetActive(false);

                break;
        }
    }
}
