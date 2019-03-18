using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foilageSpwaner : MonoBehaviour
{
    [HideInInspector] public List<GameObject> myTrees = new List<GameObject>();
    public bool spwanTrees;

    // Start is called before the first frame update
    void Start()
    {
        if (spwanTrees)
        {
            GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().SetSeed(GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().seed * 100000 + (Mathf.RoundToInt((transform.position.x * 100) + (transform.position.y * 10) + transform.position.z)));
            for (int i = 0; i < GetComponent<MeshGenerator>().spwanAblepoints.Count; i++)
            {                
                if (GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().GenerateSingleNumber(1f, 1000f) > 995f)
                {
                    GameObject newTree = GameObject.FindObjectOfType<Foilage>().getTree();
                    if (newTree != null)
                    {
                        Vector3 treePos = GetComponent<MeshGenerator>().spwanablePosition[i];
                        newTree.transform.position = treePos;
                        newTree.transform.up = GetComponent<MeshFilter>().sharedMesh.normals[GetComponent<MeshGenerator>().spwanAblepoints[i]].normalized;
                        newTree.transform.parent = this.transform;
                        myTrees.Add(newTree);
                    }
                }
            }
            GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().SetSeed(GetComponent<MeshGenerator>().planetCore.GetComponent<Randomizer>().seed);
        }
    }

    public void Reseter()
    {
        for (int i = 0; i < myTrees.Count; i++)
        {
            if (myTrees[i] != null)
            {
                myTrees[i].transform.parent = GameObject.FindObjectOfType<Foilage>().transform;
                myTrees[i].SetActive(false);
            }
        }
        myTrees.Clear();
    }
}
