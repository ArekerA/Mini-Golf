using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public void Randomize()
    {
        Vector3 v = new Vector3();
        Transform[] transforms = GetComponentsInChildren<Transform>();
        for (int i = 1; i < transforms.Length; i++)
        {
            v.x = Random.Range(1f, 1.5f);
            v.y = Random.Range(1f, 1.5f);
            v.z = Random.Range(1f, 1.5f);
            transforms[i].localScale =  v;
            transforms[i].rotation = Quaternion.Euler(-90, Random.Range(0f, 360f), 0);
        }
    }
    public void CombineMeshes()
    {
        Vector3 oldPos = transform.position;
        Quaternion oldRot = transform.rotation;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        Debug.Log("start combine");
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
        Mesh final = new Mesh();
        CombineInstance[] instances = new CombineInstance[filters.Length];
        for (int i = 0; i < filters.Length; i++)
        {
            Debug.Log("combine: "+i);
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
    public void DestroyChildren()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    public void ActivationChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public int GetChildrenCount()
    {
        return transform.childCount;
    }
}
