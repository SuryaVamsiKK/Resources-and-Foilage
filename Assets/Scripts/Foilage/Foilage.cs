using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foilage : MonoBehaviour
{
    static int numTrees = 10;
    public GameObject tree;
    //static List<GameObject> trees;

    // Start is called before the first frame update
    void Awake()
    {
        //reSpwan(); 
    }

    public void Update()
    {
        //if(trees.Count != numTrees)
        //{
        //    reSpwan();
        //}
    }

    //public void reSpwan()
    //{
    //    trees = new List<GameObject>();
    //    for (int i = 0; i < numTrees; i++)
    //    {

    //        trees.Add(treeI);
    //        treeI.transform.parent = this.transform;
    //        treeI.SetActive(false);
    //    }
    //}

    public GameObject getTree()
    {
    //    for (int i = 0; i < trees.Count; i++)
    //    {
    //        if (!trees[i].activeSelf)
    //        {
    //            return trees[i];
    //        }
    //    }
       GameObject treeI = (GameObject)Instantiate(tree, Vector3.zero, Quaternion.identity);
       return treeI;
    }


}
