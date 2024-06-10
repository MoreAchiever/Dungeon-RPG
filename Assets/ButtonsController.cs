using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonsController : MonoBehaviour
{
    public List<Button> allButtons;
    public float delayDuration = 2f;

    private GameController ButtonsAccess;

    private void Start()
    {

        foreach (Button button in allButtons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));

        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        // Disable all buttons
        foreach (Button button in allButtons)
        {
            button.interactable = false;
        }
        bool Active = ButtonsAccess.getButtons();

        // Start a coroutine to reactivate the buttons after the specified delay
        if (Active == true)
        {
            StartCoroutine(ReactivateButtons());
        }
        
    }

    private IEnumerator ReactivateButtons()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(delayDuration);

        // Re-enable all buttons
        foreach (Button button in allButtons)
        {
            button.interactable = true;
        }
    }

    void Update()
    {
        ButtonsAccess = GameObject.FindObjectOfType<GameController>();
        bool Active = ButtonsAccess.getButtons();
    }
}
