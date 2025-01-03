using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main Code of the SettingsMenu_Script was written by Wendt Hendrik
/// </summary>
public class SettingsMenu_Script : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;  // Dropdown: Selecting screen resolution

    public Toggle fullscreenToggle; // Toggle: enable or disable fullscreen mode

    Resolution[] resolutions;  // Array to store the supported screen resolution of the device

    int currentResolutionIndex = 0;  // Index of the currently active screen resolution

    public int minWidth = 1920;  // Allowed screen resolution (minimum)
    public int minHeight = 1080;  // Allowed screen resolution (minimum)

    public Slider sensitivitySlider;  // Slider:  Mouse Sensitivity
    private const string SensitivityKey = "MouseSensitivity";  // PlayerPrefs key to store mouse sensitivity

    public Slider volumeSlider;  // Slider: Volume
    private const string VolumeRTCP = "MasterVolume";  // Name of the RTPC in Wwise
    private const string VolumeKey = "MasterVolume";  // PlayerPrefs key to store the volume

    /// <summary>
    /// Start is called before the first frame update.
    /// Initializes the resolution dropdown menu and sets the current resolution and fullscreen toggle state.
    /// </summary>
    private void Start()
    {
        // Get for the current device all supported screen resolutions
        resolutions = Screen.resolutions;

        // Clear any existing options in the resolution dropdown
        resolutionDropdown.ClearOptions();

        // Create a list to store the resolution options as strings
        List<string> options = new List<string>();

        // Keeps track of unique resoltions with HashSet
        HashSet<string> uniqueResolutions = new HashSet<string>();

        // Filter out resolutions below the minimum allowed size
        List<Resolution> filteredResolutions = new List<Resolution>();

        // Dropdown options: Loop through each resolution
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width >= minWidth && resolutions[i].height >= minHeight)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;

                // Add resolution if its unique
                if (!uniqueResolutions.Contains(option))
                {
                    uniqueResolutions.Add(option);
                    filteredResolutions.Add(resolutions[i]);
                    options.Add(option);

                    // Check if the resolution matches the current screen resolution
                    if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                    {
                        currentResolutionIndex = filteredResolutions.Count - 1; // Store index of current resolution
                    }
                }
            }
        }

        // Replace the resolutions array with the filtered list
        resolutions = filteredResolutions.ToArray();

        // Dropdown menu: Add the resolution options
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;

        // Set the dropdown's default value to the current resolution
        resolutionDropdown.RefreshShownValue();

        // Set the fullscreen toggle's default state to match the current fullscreen mode
        fullscreenToggle.isOn = Screen.fullScreen;

        // Load and apply the saved mouse sensitivity value from PlayerPrefs
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 2.0f);  // Default to 2.0f if no value is saved
        sensitivitySlider.value = savedSensitivity;  // Sewt the slider to reflect the saved sensitivity value

        // Add listener to handle changes to the sensitivity slider's value
        sensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);  

        // Load and apply the saved volume from PlayerPrefs
        float saveVolume = PlayerPrefs.GetFloat(VolumeKey, 50.0f);
        volumeSlider.value = saveVolume;

        // Set the RTPC value in Wwise
        AkSoundEngine.SetRTPCValue(VolumeRTCP, saveVolume);

        // Add listener for slider changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    /// <summary>
    /// Sets the screen resolution based on the selected dropdown value.
    /// Ensures that the resolution does not fall below the minimum allowed size
    /// </summary>
    /// <param name="resolutionIndex">Selected resolution in the dropdown menu as an index</param>
    public void SetResolution(int resolutionIndex)
    {
        // Get the selected resolution from the array
        Resolution resolution = resolutions[resolutionIndex];

        // Ensure the resolution is not smaller than the minimum allowed size
        if (resolution.width < minWidth || resolution.height < minHeight) 
        {
            resolution.width = minWidth;
            resolution.height = minHeight;
        }

        // Maintain the current fullscreen state and Apply the selected resolution
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        // Adjust the camera's aspect ratio and field of view based on the new resolution
        AdjustCameraAspect();
    }

    /// <summary>
    /// Logs the mouse sensitivity value (placeholder function).
    /// </summary>
    /// <param name="mouseSensitivity">New mouse sensitivity value</param>
    public void SetMouseSensitivity(float mouseSensitivity)
    {
        // Save the new mouse sensitivity value in PlayerPrefs
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        PlayerPrefs.Save();  // Ensure the value is saved

        Debug.Log(mouseSensitivity);  // Log the sensitivity value (for debugging purposes)
    }

    /// <summary>
    /// Logs the volume value (placeholder function).
    /// </summary>
    /// <param name="volume">The new volume value (slider position)</param>
    public void SetVolume(float volume)
    {
        // Set RTPC value in Wwise
        AkSoundEngine.SetRTPCValue(VolumeRTCP, volume);

        // Save volume in PlayerPrefs
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();

        //Debug.Log(volume);  // Log the volume level (for debugging purposes)
    }

    /// <summary>
    /// Removes the sensitivity slider's listener to prevent errors on object destruction
    /// Removes the volume slider's listener to prevent errors on object destruction
    /// </summary>
    private void OnDestroy()
    {
        sensitivitySlider.onValueChanged.RemoveListener(SetMouseSensitivity);
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    /// <summary>
    /// Toggles fullscreen mode on or off.
    /// </summary>
    /// <param name="isFullscreen">True to enable fullscreen mode, false to disable it</param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;  // Apply the fullscreen state
    }

    /// <summary>
    /// Adjusts the camera's aspect ratio and field of view based on the current screen resolution.
    /// Ensures proper visuals even at lower resolutions.
    /// </summary>
    private void AdjustCameraAspect() 
    {
        // Check if the main camera is available
        if (Camera.main != null)
        {
            // Calculate and set the camera's aspect ratio
            Camera.main.aspect = (float)Screen.width / Screen.height;

            // Adjust the camera's field of view (FOV) for resolutions smaller than 1920 x 1080
            if (Screen.width < 1920 || Screen.height < 1080)
            {
                // Interpolate the FOV between 60 and 90 based on the width ratio
                Camera.main.fieldOfView = Mathf.Lerp(60f, 90f, (float)Screen.width / 1920f); 
            }
            else
            {
                // Set the default FOV for resolutions 1920 x 1080 or larger
                Camera.main.fieldOfView = 60f; 
            }

            Debug.Log($"Camera aspect ratio adjusted to: {Camera.main.aspect}, FOV: {Camera.main.fieldOfView}");
        }
        else
        {
            Debug.LogWarning("No Main Camera found to adjust aspect ratio.");
        }
    }
}