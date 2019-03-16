﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MeshGenerator : MonoBehaviour
{
    #region LOD

    [HideInInspector] public int lod = 0;
    [HideInInspector] public GameObject chunk;
    [HideInInspector] public int quad_Location;
    [HideInInspector] public Vector2 planerRefrence;

    #endregion

    #region Dimensions

    [HideInInspector] public ColorGenerator colorGenerator;
    [HideInInspector] public float side;
    [HideInInspector] public Shape_Settings shapeSettings;
    [HideInInspector] public Vector3 localUp = Vector3.up;
    Vector3 axisA;
    Vector3 axisB;

    #endregion

    #region noise
    [HideInInspector] public NoiseFilter noiseFilter;

    #endregion

    #region Mesh

    Vector3[] verts;
    int[] triangles;   
    Mesh mesh;
    Vector2[] uvs;
    public Material mat;

    #endregion

    public Transform planetCore;
    public Transform mainFace;

    public List<int> spwanAblepoints = new List<int>();
    public List<Vector3> spwanablePosition = new List<Vector3>();

    private void UpdateColor()
    {
        if(lod <= 1)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void CreateShape()
    {

        if (lod > 0)
        {
            mainFace = transform.parent;            
        }

        #region Initalization

        noiseFilter = new NoiseFilter(shapeSettings);
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);

        mesh = new Mesh();
        verts = new Vector3[(shapeSettings.previewResolution + 1) * (shapeSettings.previewResolution + 1)];
        uvs = new Vector2[(shapeSettings.previewResolution + 1) * (shapeSettings.previewResolution + 1)];

        triangles = new int[(shapeSettings.previewResolution) * (shapeSettings.previewResolution) * 6]; 
        int vert = 0; 
        int triIndex = 0;

        if(planetCore == null)
        {
            if (transform.parent.GetComponent<MeshGenerator>() != null)
            {
                planetCore = transform.parent.GetComponent<MeshGenerator>().planetCore;
            }
            else
            {
                planetCore = transform.parent;
            }
        }

        #endregion

        for (int x = 0, i = 0; x <= shapeSettings.previewResolution; x++)
        {
            for (int z = 0; z <= shapeSettings.previewResolution; z++)
            {

                #region Adjustment of valuse according to the LOD to implement on vertices.

                float scale = 2f;
                float pos = -0.5f;
                scale = (2 / (Mathf.Pow(2, lod)));
                pos = Mathf.Pow(2, lod - 1) - 1;

                #endregion

                Vector2 localVert = (new Vector2(x, z) / shapeSettings.previewResolution);
                if (lod > 0)
                {
                    #region Vertices based on Quad Tree position of the plane 
                    if (quad_Location == 0)
                    {
                        planerRefrence = new Vector2(0f, 0f) + (transform.parent.GetComponent<MeshGenerator>().planerRefrence * 2);
                    }
                    if (quad_Location == 1)
                    {
                        planerRefrence = new Vector2(1f, 0f) + (transform.parent.GetComponent<MeshGenerator>().planerRefrence * 2);
                    }
                    if (quad_Location == 2)
                    {
                        planerRefrence = new Vector2(0f, 1f) + (transform.parent.GetComponent<MeshGenerator>().planerRefrence * 2);
                    }
                    if (quad_Location == 3)
                    {
                        planerRefrence = new Vector2(1f, 1f) + (transform.parent.GetComponent<MeshGenerator>().planerRefrence * 2);
                    }

                    localVert = (new Vector2(x, z) / shapeSettings.previewResolution) - planerRefrence;
                    #endregion
                }

                #region Vertices

                float elevaltion = 0;
                verts[i] = (localUp + (localVert.x + pos) * scale * axisA - (localVert.y + pos) * scale * axisB);
                verts[i] = verts[i].normalized;
                uvs[i] = new Vector2(colorGenerator.biomePercentFromPoint(verts[i]), 0);        // 1. Biome Index to UVs

                verts[i] = noiseFilter.CalculatePointOnPlanet(verts[i], this.transform.position, out elevaltion);   // 2. Vertice height to elevation


                if (lod == 0)
                {
                    #region Explanation
                    /*
                     * This for the lower LODs to use the highest lod as 
                     * the base because the lower lod might spwan on the 
                     * hill and its lowest point might be something differnt.
                     * 
                     */
                    #endregion

                    transform.parent.GetComponent<PlanetGenerator>().elevationMinMax.AddValue(elevaltion);
                }

                if (elevaltion > 1.3f * planetCore.GetComponent<PlanetGenerator>().shapeSettings.planetRadius && elevaltion < 1.4f * planetCore.GetComponent<PlanetGenerator>().shapeSettings.planetRadius)
                {
                    if (uvs[i].x < 0.679f)
                    {
                        if (lod >= planetCore.GetComponent<PlanetGenerator>().lodSettings.maxDepth)
                        {
                            spwanAblepoints.Add(i);
                            spwanablePosition.Add(verts[i]);
                        }
                    }   
                }


                /*
                 *  1.  Biome index can be extracted from that point for which type of object to spwan and change its values accordingly 
                 *  
                 *  2.  Based on the elevation and elevation minmax values,
                 *      we can extract vertice's height position from the planet for 
                 *      spwaning different objects at different heights.
                 *      
                 *  CAUTION : if you are spwaning the trees when the chunks are loading as they are getting instanciated ... 
                 *            well better store all the chosen vertice's positions into a seperate save file or somewhere and better 
                 *            read from that from next time as it shuld be the same the next time the player comes there .... 
                 *            which might just be a huge problem    ... mainly should be used for resources and structures that can be big like tress and things 
                 *            (not for the foilage like grass) which can quite be randomized.
                 *            
                 *            or 
                 *            
                 *            do not randomly choose ... right a formula which can store just the seed and randomly place the objects based on the seed which can be quite 
                 *            ... i think thats how it supposed to be done ...... like the planets as they are same everytime they are generated.
                 *  
                 */

                i++;

                #endregion

                if (x != shapeSettings.previewResolution && z != shapeSettings.previewResolution)
                {
                    #region Triagnles

                    triangles[triIndex + 0] = vert + shapeSettings.previewResolution + 2;
                    triangles[triIndex + 1] = vert + shapeSettings.previewResolution + 1;
                    triangles[triIndex + 2] = vert + 1;
                    triangles[triIndex + 3] = vert + 1;
                    triangles[triIndex + 4] = vert + shapeSettings.previewResolution + 1;
                    triangles[triIndex + 5] = vert + 0;

                    #endregion

                    vert++;
                    triIndex += 6;
                }
            }

            if (x != shapeSettings.previewResolution)
            {
                vert++;
            }
        }
    }

    void Update()
    {
        GetComponent<MeshRenderer>().sharedMaterial.SetVector("_planetCenter", planetCore.position);
        if(lod >= planetCore.GetComponent<PlanetGenerator>().lodSettings.maxDepth)
        {
            GetComponent<MeshCollider>().enabled = true;
            GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
        }
        else
        {
            GetComponent<MeshCollider>().enabled = false;
            GetComponent<MeshCollider>().sharedMesh = null;
        }
    }

    public void UpdateMesh()
    {
        CalculateCenter(verts);
        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.uv = uvs;
        GetComponent<MeshFilter>().sharedMesh = mesh;
        SetMaterial(mat);
    }

    void CalculateCenter(Vector3[] verts)
    {
        #region To center the pivot object
        //Vector3 sum = Vector3.zero;
        //for (int i = 0; i < verts.Length; i++)
        //{
        //    sum += verts[i];
        //}

        //this.transform.position = (sum/verts.Length) + transform.root.position;
        #endregion

        transform.position = verts[(verts.Length / 2)]; // To put the pivot at the exact middle vertice. For further control over LOD.
       
       for (int i = 0; i < verts.Length; i++)
       {
            verts[i] = verts[i] - this.transform.position;
       }
       
       Vector3 pose = planetCore.position + (planetCore.rotation * transform.position);
       transform.position = pose;
    }

    public void SetMaterial(Material mat)
    {
        if(mat != this.mat)
        {
            Material material = new Material(mat.shader);
            UpdateMaterial(material);
        }
        else
        {
            UpdateMaterial(mat);
        }
    }
    
    void UpdateMaterial(Material material)
    {
        material.SetVector("_elivationMinMax", mat.GetVector("_elivationMinMax"));
        material.SetTexture("_pColor", mat.GetTexture("_pColor"));
        material.SetFloat("_smoothness", mat.GetFloat("_smoothness"));
        material.SetFloat("_specular", mat.GetFloat("_specular"));
        material.SetVector("_planetCenter", planetCore.position);
        /*
         * FUCKING DUCK THE BIGGEST BUG OF LIFE ..... 
         * The shader evaluation of hight was in object space meaing local space but since 
         * the planet is being rotated the face's position is alred but still doing a stupid thing of taking the world positions like is shit face idiot ...
         * original line ||  material.SetVector("_planetCenter", this.transform.position - planetCore.position);    ||
         * changed Line  ||  material.SetVector("_planetCenter", this.transform.localPosition);                     ||
         * 
         * 
         * well GG transform.localposition ate up the god damn LOD shaders so changed the approch of the shader it self !!
        */
        GetComponent<MeshRenderer>().material = material;
    }
}
