﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    private static WaterManager _instance;

    public static WaterManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private int meshLength = 73;

    public Transform collisionParent;
    public GameObject colliderReference;
    private List<GameObject> colliders = new List<GameObject>();

    [Header("Configuration")]
    [Space]
    [Range(0.00f, 10.00f)]
    public float waterFlowSpeed;
    [Range(-5f, 5f)]
    public float globalSeaLevelFactor;
    [Range(0, 10f)]
    public float houleIntensity;
    [Range(0, 10f)]
    public float houleScale;
    [Range(-1f, 1f)]
    public float collisionYOffset;
    [Range(0, 1)]
    public float tide;

    public float tideMinPositionY;
    public float tideMaxPositionY;

    public float portFalloffPosition;
    public float portFalloff;
    public float portHeight;

    public List<WaterZone> waterZones = new List<WaterZone>();

    //public float vertexShoreHeight;
    //public float highSeaYOffset;
    //public float highSeaZOffset;
    //public float portFalloff;

    [Header("References")]
    [Space]
    public MeshRenderer waterPlane;
    public GameObject waterPlaneRight;
    public GameObject waterPlaneLeft;

    private Mesh mesh;
    private float[,] noise = new float[80,80];
    private float waterActualX;
    private float waterActualY;

    private float nextHoule_X;
    private float nextHoule_Y;
    private float previousHoule_X;
    private float previousHoule_Y;

    private float houleInterpolationTimer;

    [HideInInspector]
    public Vector3[] vertices;
    Vector3[] normals;

    void Start()
    {
        mesh = Instantiate(waterPlane.GetComponent<MeshFilter>().mesh);
        waterPlane.GetComponent<MeshFilter>().mesh = mesh;
        waterPlaneRight.GetComponent<MeshFilter>().mesh = mesh;
        waterPlaneLeft.GetComponent<MeshFilter>().mesh = mesh;
        StartCoroutine(UpdateWater());

        //Instantiating water colliders
        
        for(int i = 0; i < mesh.vertices.Length; i++)
        {
            GameObject newCollider = Instantiate(colliderReference, Vector3.zero, Quaternion.identity, collisionParent.transform);
            colliders.Add(newCollider);
            newCollider.SetActive(true);
        }

        Destroy(colliderReference);
    }

    public void GenerateNextHoule()
    {
        nextHoule_X += 2;
        nextHoule_Y += 2;
    }

    IEnumerator UpdateWater()
    {
        GenerateNextHoule();

        while (true)
        {
            yield return null;

            vertices = mesh.vertices;
            normals = mesh.normals;

            //waterActualX += Time.deltaTime * waterFlowSpeed;

            if(houleInterpolationTimer < waterFlowSpeed)
            {
                houleInterpolationTimer += Time.deltaTime;
            }

            else if(houleInterpolationTimer > waterFlowSpeed)
            {
                houleInterpolationTimer = 0;

                GenerateNextHoule();
                previousHoule_X = waterActualX;
                previousHoule_Y = waterActualY;
            }

            waterActualX = Mathf.Lerp(previousHoule_X, nextHoule_X, houleInterpolationTimer / waterFlowSpeed);
            waterActualY = Mathf.Lerp(previousHoule_Y, nextHoule_Y, houleInterpolationTimer / waterFlowSpeed);

            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(vertices[i].x, vertices[i].y, (Mathf.PerlinNoise(((vertices[i].x) * houleScale), (vertices[i].y) * houleScale)) + globalSeaLevelFactor);
                vertices[i].z += (Mathf.PerlinNoise(((vertices[i].x + Mathf.Lerp(previousHoule_X, nextHoule_X, houleInterpolationTimer / waterFlowSpeed)) * houleScale), (vertices[i].y + Mathf.Lerp(previousHoule_Y, nextHoule_Y, houleInterpolationTimer / waterFlowSpeed)) * houleScale) * (houleIntensity * tide));

                Vector3 TEMP = vertices[i];
                float fallOffCalulcation = 0;

                fallOffCalulcation = Mathf.Lerp(portHeight, vertices[i].z, (portFalloffPosition - vertices[i].y) * portFalloff);

                TEMP.z = fallOffCalulcation;
                TEMP.z -= Mathf.Lerp(tideMinPositionY, tideMaxPositionY, tide);

                vertices[i] = TEMP;

                
                if(waterZones.Count > 0)
                {
                    
                    for(int j = 0; j < waterZones.Count; j++)
                    {
                        var transformMatrix = waterPlane.transform.localToWorldMatrix;
                        Vector3 testedVertice = transformMatrix.MultiplyPoint3x4(vertices[i]);
                        testedVertice = testedVertice / 2;

                        if (waterZones[j].GetComponent<Collider>().bounds.Contains(transform.TransformPoint(testedVertice)))
                        {
                            Vector3 v = vertices[i];
                            v.z = waterZones[j].transform.position.y;
                            vertices[i] = v;
                            break;
                        }
                    } 
                } 

                if (fallOffCalulcation < 0)
                {
                    fallOffCalulcation = 0;
                }

                Vector3 pos = transform.TransformPoint(vertices[i]);
                Vector3 finalPos = Vector3.zero;
                finalPos.x = pos.x;
                finalPos.z = pos.y;
                finalPos.y = pos.z;

                colliders[i].transform.position = finalPos + new Vector3(0, collisionYOffset, 0); 
            }

            waterPlane.GetComponent<MeshFilter>().mesh.vertices = vertices;
            waterPlane.GetComponent<MeshFilter>().mesh.RecalculateNormals();
            waterPlaneRight.GetComponent<MeshFilter>().mesh.vertices = vertices;
            waterPlaneRight.GetComponent<MeshFilter>().mesh.RecalculateNormals();
            waterPlaneLeft.GetComponent<MeshFilter>().mesh.vertices = vertices;
            waterPlaneLeft.GetComponent<MeshFilter>().mesh.RecalculateNormals();
        }
    }

    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}
