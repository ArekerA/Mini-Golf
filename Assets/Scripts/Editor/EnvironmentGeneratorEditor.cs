using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EnvironmentGenerator))]
public class EnvironmentGeneratorEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnvironmentGenerator eg = (EnvironmentGenerator)target;
        GUILayout.Label("Count: "+eg.GetChildrenCount());
        if (GUILayout.Button("Combine Meshes"))
        {
            eg.CombineMeshes();
        }
        if (GUILayout.Button("Randomize"))
        {
            eg.Randomize();
        }
        if (GUILayout.Button("Destroy Children"))
        {
            eg.DestroyChildren();
        }
        if (GUILayout.Button("Activation Children"))
        {
            eg.ActivationChildren();
        }
    }
}
