using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public UnityEvent PlayAgainClicked; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayAgain(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log($"Pressed to Play Again");
            PlayAgain();
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Menu");
    }
}
