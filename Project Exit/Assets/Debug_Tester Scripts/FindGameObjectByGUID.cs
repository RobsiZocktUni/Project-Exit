using System.Collections;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
/*
public class GUIDFinder : EditorWindow
{
    private string guid; // The GUID (of prefab, material, or script)
    private bool isMaterialSearch = false; // Flag to toggle between prefab, material, and script search
    private bool isScriptSearch = false; // Flag to toggle between script search
    private List<GameObject> foundObjects = new List<GameObject>(); // Store found GameObjects

    [MenuItem("Tools/Find GameObject by GUID")]
    private static void OpenWindow()
    {
        GetWindow<GUIDFinder>("Find by GUID");
    }

    private void OnGUI()
    {
        GUILayout.Label("Find GameObject by GUID", EditorStyles.boldLabel);

        // Toggle between prefab, material, and script search
        isMaterialSearch = GUILayout.Toggle(isMaterialSearch, "Search for Material");
        isScriptSearch = GUILayout.Toggle(isScriptSearch, "Search for Script");

        // Input field for the GUID
        guid = EditorGUILayout.TextField("GUID:", guid);

        if (GUILayout.Button("Find"))
        {
            FindGameObjectsByGUID();
        }

        // Display the result
        if (foundObjects.Count > 0)
        {
            foreach (var go in foundObjects)
            {
                EditorGUILayout.LabelField("Found GameObject:", go.name);

                if (GUILayout.Button("Select in Editor"))
                {
                    Selection.activeGameObject = go;
                    EditorGUIUtility.PingObject(go);
                }
            }
        }
        else
        {
            GUILayout.Label("No GameObject found with the given GUID.");
        }
    }

    private void FindGameObjectsByGUID()
    {
        foundObjects.Clear(); // Reset the result list


        if(guid=="0000000000000000e000000000000000"){
            Debug.LogWarning("Provided string is a Mesh GUID");
            return;
        }

        if (isMaterialSearch)
        {
            FindGameObjectsByMaterialGUID();
        }
        else if (isScriptSearch)
        {
            FindGameObjectsByScriptGUID();
        }
        else
        {
            FindGameObjectsByPrefabGUID();
        }
    }

    private void FindGameObjectsByPrefabGUID()
    {
        // Use AssetDatabase to find the asset using GUID
        string assetPath = AssetDatabase.GUIDToAssetPath(guid);

        if (string.IsNullOrEmpty(assetPath))
        {
            Debug.LogWarning("No asset found with the given GUID.");
            return;
        }

        // Find all GameObjects in the scene
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        foreach (var go in allGameObjects)
        {
            // Check if the GameObject is referencing the prefab
            if (PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(go) == assetPath)
            {
                foundObjects.Add(go);
            }
        }

        if (foundObjects.Count == 0)
        {
            Debug.LogWarning("No GameObject found referencing the given prefab.");
        }
    }

    private void FindGameObjectsByMaterialGUID()
    {
        // Use AssetDatabase to find the material using GUID
        string assetPath = AssetDatabase.GUIDToAssetPath(guid);

        if (string.IsNullOrEmpty(assetPath))
        {
            Debug.LogWarning("No material found with the given GUID.");
            return;
        }

        Material material = AssetDatabase.LoadAssetAtPath<Material>(assetPath);
        if (material == null)
        {
            Debug.LogWarning("No material found at the asset path.");
            return;
        }

        // Find all GameObjects in the scene
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        foreach (var go in allGameObjects)
        {
            // Check if the GameObject uses the material in its Renderers
            if (IsGameObjectUsingMaterial(go, material))
            {
                foundObjects.Add(go);
            }
        }

        if (foundObjects.Count == 0)
        {
            Debug.LogWarning("No GameObject found using the given material.");
        }
    }

    private void FindGameObjectsByScriptGUID()
    {
        // Use AssetDatabase to find the script using GUID
        string assetPath = AssetDatabase.GUIDToAssetPath(guid);

        if (string.IsNullOrEmpty(assetPath))
        {
            Debug.LogWarning("No script found with the given GUID.");
            return;
        }

        // Load the script asset
        MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
        if (script == null)
        {
            Debug.LogWarning("No script found at the asset path.");
            return;
        }

        // Find all GameObjects in the scene
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        foreach (var go in allGameObjects)
        {
            // Check if any MonoBehaviour on this GameObject is using the script
            if (IsGameObjectUsingScript(go, script))
            {
                foundObjects.Add(go);
            }
        }

        if (foundObjects.Count == 0)
        {
            Debug.LogWarning("No GameObject found with the given script.");
        }
    }

    private bool IsGameObjectUsingMaterial(GameObject go, Material targetMaterial)
    {
        // Get all renderers on the GameObject (MeshRenderer, SkinnedMeshRenderer, etc.)
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();

        foreach (var renderer in renderers)
        {
            // Check if any material matches the target material
            foreach (var mat in renderer.sharedMaterials)
            {
                if (mat == targetMaterial)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsGameObjectUsingScript(GameObject go, MonoScript targetScript)
    {
        // Get all MonoBehaviour components on the GameObject
        MonoBehaviour[] scripts = go.GetComponentsInChildren<MonoBehaviour>();

        foreach (var script in scripts)
        {
            // Check if the MonoBehaviour is of the type of the target script
            if (script != null && MonoScript.FromMonoBehaviour(script) == targetScript)
            {
                return true;
            }
        }

        return false;
    }
}
*/
