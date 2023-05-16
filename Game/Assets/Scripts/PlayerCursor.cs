using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCursor : MonoBehaviour
{
    public Button[] buttons;
    public float jumpDistance = 1f;

    [SerializeField]
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction selectAction;

    private int currentButtonIndex;

    private void OnEnable()
    {
        moveAction = playerInput.UI.Navigate;
        //moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick");
        moveAction.Enable();

        //selectAction = playerInput.UI.
        ////selectAction = new InputAction("Select", binding: "<Gamepad>/buttonSouth");
        //selectAction.Enable();

        moveAction.performed += OnMove;
        //selectAction.performed += OnSelect;

        // Initialize the cursor position to the first button
        currentButtonIndex = 0;
        transform.position = buttons[currentButtonIndex].transform.position;
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        //selectAction.performed -= OnSelect;

        moveAction.Disable();
        //selectAction.Disable();
    }

    private void Update()
    {
        // Perform selection logic if the player pressed the select button
        //if (selectAction.ReadValue<float>() > 0.5f)
        //{
        //    // Perform the desired selection action (e.g., invoke a button click event)
        //    buttons[currentButtonIndex].onClick.Invoke();
        //}
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        // Determine the desired button index based on the move input direction
        int desiredButtonIndex = currentButtonIndex;
        if (moveInput.x > 0)
        {
            desiredButtonIndex = Mathf.Min(desiredButtonIndex + 1, buttons.Length - 1);
        }
        else if (moveInput.x < 0)
        {
            desiredButtonIndex = Mathf.Max(desiredButtonIndex - 1, 0);
        }

        // Jump to the desired button
        JumpToButton(desiredButtonIndex);
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        // Perform the desired selection action (e.g., invoke a button click event)
        buttons[currentButtonIndex].onClick.Invoke();
    }

    private void JumpToButton(int buttonIndex)
    {
        currentButtonIndex = buttonIndex;
        transform.position = buttons[currentButtonIndex].transform.position;
    }
}
