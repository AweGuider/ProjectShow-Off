using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuPlayerAmountButton : MonoBehaviour
{
    [SerializeField]
    private int _amountOfPlayers;


    public int AmountOfPlayers { get => _amountOfPlayers; }

    // Start is called before the first frame update
    void Start()
    {
        if (_amountOfPlayers == 0) _amountOfPlayers = int.Parse(name);
    }

    private void OnEnable()
    {
        
    }
}
