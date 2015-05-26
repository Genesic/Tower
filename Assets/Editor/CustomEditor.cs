using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomEditor<T> : Editor where T : Object
{
    public T Target { get { return target as T; } }

    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    public bool Button(string btnName, Color color, params GUILayoutOption[] option)
    {
        Color origin = GUI.color;

        GUI.color = color;

        bool submit = false;

        if (GUILayout.Button(btnName, option)) submit = true;
        
        GUI.color = origin;

        return submit;
    }
}
