using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foilageSpwaner : MonoBehaviour
{
    List<GameObject> myTrees = new List<GameObject>();
    public bool spwanTrees;

    // Start is called before the first frame update
    void Awake()
    {
        if (spwanTrees)
        {
            for (int i = 0; i < GetComponent<MeshGenerator>().spwanAblepoints.Count; i++)
            {
                GameObject newTree = Foilage.getTree();
                if (newTree != null)
                {
                    Vector3 treePos = GetComponent<MeshGenerator>().spwanablePosition[i];
                    newTree.transform.position = treePos;
                    newTree.transform.up = GetComponent<MeshFilter>().sharedMesh.normals[GetComponent<MeshGenerator>().spwanAblepoints[i]].normalized;
                    newTree.SetActive(true);
                    myTrees.Add(newTree);
                }
            }
        }
    }
}
