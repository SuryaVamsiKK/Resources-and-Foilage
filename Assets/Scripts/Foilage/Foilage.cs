using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foilage : MonoBehaviour
{
    static int numTrees = 200;
    public GameObject tree;
    static GameObject[] trees;

    // Start is called before the first frame update
    void Awake()
    {
        trees = new GameObject[numTrees];
        for (int i = 0; i < numTrees; i++)
        {
            trees[i] = (GameObject)Instantiate(tree, Vector3.zero, Quaternion.identity);
            trees[i].transform.parent = this.transform;
            trees[i].SetActive(false);
        }
    }

    static public GameObject getTree()
    {
        for (int i = 0; i < numTrees; i++)
        {
            if(!trees[i].activeSelf)
            {
                return trees[i];
            }
        }
        return null;
    }


}
