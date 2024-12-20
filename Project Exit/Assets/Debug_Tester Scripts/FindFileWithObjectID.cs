using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
public class FindFileWithObjectID : EditorWindow
{
    private string objectID;

    [MenuItem("Tools/Find Object by YAML ID")]
    public static void ShowWindow()
    {
        GetWindow<FindFileWithObjectID>("Find Object by YAML ID");
    }

    private void OnGUI()
    {
        GUILayout.Label("Search for Object by YAML ID", EditorStyles.boldLabel);

        objectID = EditorGUILayout.TextField("Object ID", objectID);

        if (GUILayout.Button("Find"))
        {
            FindObject(objectID);
        }
    }

   private static void FindObject(string id)
{
    if (string.IsNullOrEmpty(id))
    {
        Debug.LogError("ID cannot be empty.");
        return;
    }

    string[] guids = AssetDatabase.FindAssets("t:Object");
    bool found = false;

    foreach (string guid in guids)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);

     //   Debug.Log($"Checking file: {path}");

        try
        {
            // Attempt to load the asset as text
            string assetContent = AssetDatabase.LoadAssetAtPath<TextAsset>(path)?.text;

            // Fallback to raw file read if LoadAssetAtPath fails
            if (string.IsNullOrEmpty(assetContent))
            {
                assetContent = System.IO.File.ReadAllText(path);
            }

            if (assetContent.Contains("&" + id))
            {
                Debug.Log($"Found in asset: {path}");
                found = true;
            }
        }
        // catch (UnauthorizedAccessException ex)
        // {
        //     Debug.LogError($"Access denied to file: {path}. Error: {ex.Message}");
        // }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error accessing file at {path}: {ex.Message}");
        }
    }

    if (!found)
    {
        Debug.LogWarning("ID not found in any assets.");
    }

    Debug.Log("Search complete.");
}


}
*/
