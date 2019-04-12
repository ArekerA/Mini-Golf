using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour {
    public PhysicMaterial physicMaterial;
    private void Start()
    {
        gameObject.AddComponent<MeshCollider>();
        GetComponent<MeshCollider>().material = physicMaterial;
    }

    public void CombineMeshes()
    {
        Vector3 oldPos = transform.position;
        Quaternion oldRot = transform.rotation;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        Debug.Log("combine");
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
        Mesh final = new Mesh();
        CombineInstance[] instances = new CombineInstance[filters.Length];
        for (int i = 0; i < filters.Length; i++)
        {
            instances[i].mesh = filters[i].sharedMesh;
            instances[i].subMeshIndex = 0;
            instances[i].transform = filters[i].transform.localToWorldMatrix;
        }
        final.CombineMeshes(instances);
        GetComponent<MeshFilter>().sharedMesh = final;

        transform.position = oldPos;
        transform.rotation = oldRot;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
