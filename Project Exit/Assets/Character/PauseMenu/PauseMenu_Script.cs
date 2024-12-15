using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu_Script : MonoBehaviour
{
    public Canvas targetCanvas;

    public Character_Controller characterController;

    public Button continueButton;

    private bool isPaused = false;

    private void Start()
    {
        continueButton.onClick.AddListener(ClickedContinue);
    }

    // Method to toggle the Canvas
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ExitPauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        isPaused = !isPaused;

        if (targetCanvas != null)
        {
            // Check if the canvas is active, then toggle it
            bool isActive = targetCanvas.gameObject.activeSelf;
            targetCanvas.gameObject.SetActive(!isActive);

            if (isPaused)
            {
                characterController.DisableControls();
            }
            else
            {
                characterController.EnableControls();
            }
        }
        else
        {
            Debug.LogError("No Canvas assigned to the script.");
        }
    }

    private void ClickedContinue()
    {
        ExitPauseMenu();
    }

    private void ExitPauseMenu()
    {
        isPaused = false;
        targetCanvas.gameObject.SetActive(false);
        characterController.EnableControls();
    }
}
