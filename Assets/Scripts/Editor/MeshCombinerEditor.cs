using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MeshCombiner))]
public class MeshCombinerEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MeshCombiner mc = target as MeshCombiner;
        if (GUILayout.Button("Combine Meshes"))
        {
            mc.CombineMeshes();
        }
    }
}
