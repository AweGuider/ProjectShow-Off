using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonCustomHandler : MonoBehaviour, ISelectHandler
{

    public UnityEvent onSelect;
    // Define actions to be executed when the button is selected
    public void OnButtonSelected()
    {
        onSelect?.Invoke();
        Debug.Log("Button selected!");
        // Add your custom actions here
    }

    // Implement the ISelectHandler interface method
    public void OnSelect(BaseEventData eventData)
    {
        OnButtonSelected();
    }
}
