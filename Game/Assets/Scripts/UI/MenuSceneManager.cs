using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameData pData;

    [SerializeField]
    private int _amountOfPlayers;

    [Header("Buttons")]
    [SerializeField]
    private GameObject twoPlayers;
    [SerializeField]
    private GameObject fourPlayers;


    // Start is called before the first frame update
    void Start()
    {
        twoPlayers.GetComponent<Button>().onClick.AddListener(() =>
            OnButtonClicked(twoPlayers));
        fourPlayers.GetComponent<Button>().onClick.AddListener(() =>
            OnButtonClicked(fourPlayers));

        GameData.Instance.AmountOfLaps = 1;
        GameData.Instance.Testing = false;
    }

    private void OnButtonClicked(GameObject buttonObj)
    {
        MenuPlayerAmountButton button = buttonObj.GetComponent<MenuPlayerAmountButton>();
        _amountOfPlayers = button.AmountOfPlayers;
        pData.SetNumberOfPlayers(_amountOfPlayers);

        StartCoroutine(LoadScene(_amountOfPlayers));
    }

    private IEnumerator LoadScene(int amount)
    {

        if (amount == 0)
        {
            //Debug.LogError($"Amount of players wasn't set!");
        }

        yield return new WaitForSeconds(2f);

        if (amount == 2)
        {
            SceneManager.LoadScene("2 Player Selection");
        }
        if (amount == 4)
        {
            SceneManager.LoadScene("4 Player Selection");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameData.Instance.AmountOfLaps = 1;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameData.Instance.AmountOfLaps = 2;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameData.Instance.AmountOfLaps = 3;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
        {
            GameData.Instance.Testing = !GameData.Instance.Testing;
        }
    }
}
