using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField]
    private Transform _buttonObj;
    [SerializeField]
    private Button _button;
    //[SerializeField]
    //private Button _button;
    [SerializeField]
    private PlayerUIController _UIController;


    public event Action<int> Select = delegate { };
    public event Action Click = delegate { };

    private void Awake()
    {
        if (_buttonObj == null) _buttonObj = transform.GetChild(0);
        if (_button  == null) _button = _buttonObj.GetComponent<Button>();
        _UIController = transform.parent.parent.parent.GetComponent<PlayerUIController>();

    }
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        Select += _UIController.OnButtonSelect;
        Click += _UIController.OnButtonClick;
    }

    public void OnSelect()
    {
        Select?.Invoke(transform.GetSiblingIndex());
    }

    private void OnClick()
    {
        //Debug.Log($"{transform.GetSiblingIndex()} button was clicked");
        Click?.Invoke();
    }
    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
        Select -= _UIController.OnButtonSelect;
        Click -= _UIController.OnButtonClick;
    }
}
