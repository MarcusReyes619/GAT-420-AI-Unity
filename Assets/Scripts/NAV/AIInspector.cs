using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIInspector : EditorWindow
{
    [MenuItem("AI/Inspector")]

    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AIInspector));
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Actions", EditorStyles.boldLabel);
        if(GUILayout.Button("View Agent"))
        {
            Camera cam = Camera.main;
            GameObject go = Selection.activeGameObject;
            if(go.TryGetComponent<AINavAgent>(out AINavAgent agent))
            {
                cam.transform.parent = agent.transform;
                cam.transform.localPosition = Vector3.back * 5 + Vector3.up * 2;
                cam.transform.localRotation = Quaternion.identity;
            }
        }
        GUILayout.EndHorizontal();
    }
}
