using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(FallStoneReceiver))]
public class FallStoneReceiverEditor : CustomEditor<FallStoneReceiver>
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //EditorGUILayout

        if (Button("Trigger Snare", Color.green, GUILayout.Width(100f)))
        {
            Target.OnSnareTrigger();
        }
    }
}