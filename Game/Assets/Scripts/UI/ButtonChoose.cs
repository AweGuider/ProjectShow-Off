using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonChoose : MonoBehaviour
{
    [SerializeField]
    private PlayerSelectionData pData;

    [SerializeField]
    private int _amountOfPlayers;


    // Start is called before the first frame update
    void Start()
    {
        if (_amountOfPlayers == 0) _amountOfPlayers = int.Parse(name);
        GetComponent<Button>().onClick.AddListener(OnChoose);
    }

    public void OnChoose()
    {
        pData.SetNumberOfPlayers(_amountOfPlayers);
    }
}
