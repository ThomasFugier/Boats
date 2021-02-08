using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    private int meshLength = 73;

    public Transform collisionParent;
    public GameObject colliderReference;
    private List<GameObject> colliders = new List<GameObject>();

    [Header("Configuration")]
    [Space]
    [Range(0.00f, 10.00f)]
    public float waterFlowSpeed;
    [Range(0.00f, 0.7f)]
    public float tideScale;
    [Range(0.00f, 50f)]
    public float tideWaveIntensity;
    public float collisionYOffset;
    public float portStartAt;
    public float vertexShoreHeight;
    public float highSeaYOffset;
    public float highSeaZOffset;
    public float portFalloff;

    [Header("References")]
    [Space]
    public MeshRenderer waterPlane;


    private Mesh mesh;
    private float[,] noise = new float[80,80];
    private float waterActualX;

    void Start()
    {
        mesh = Instantiate(waterPlane.GetComponent<MeshFilter>().mesh);
        waterPlane.GetComponent<MeshFilter>().mesh = mesh;


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

    IEnumerator UpdateWater()
    {
        
        while(true)
        {
            yield return null;


            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;

            waterActualX += Time.deltaTime * waterFlowSpeed;

            for (var i = 0; i < vertices.Length; i++)
            {
                /*
                if(vertices[i].y < portStartAt)
                {
                    vertices[i] = new Vector3(vertices[i].x, vertices[i].y, (Mathf.PerlinNoise(((vertices[i].x + 100) * tideScale), (vertices[i].y + waterActualX) * tideScale) * tideWaveIntensity) + highSeaYOffset * Mathf.Clamp(Mathf.Abs(vertices[i].y - (portStartAt + highSeaZOffset)) * portFalloff, 0,1));
                }

                else
                {
                    vertices[i] = new Vector3(vertices[i].x, vertices[i].y, Mathf.Lerp(Mathf.PerlinNoise(((vertices[i].x + 100) * tideScale), (vertices[i].y + waterActualX) * tideScale) * tideWaveIntensity, vertexShoreHeight, Mathf.Abs(vertices[i].y - (portStartAt - highSeaZOffset)) * portFalloff));
                }
                */

                vertices[i] = new Vector3(vertices[i].x, vertices[i].y, vertexShoreHeight + (Mathf.PerlinNoise(((vertices[i].x + 100) * tideScale), (vertices[i].y + waterActualX) * tideScale) * tideWaveIntensity) + highSeaYOffset * Mathf.Clamp(Mathf.Abs(vertices[i].y - (portStartAt + highSeaZOffset)) * portFalloff, 0, 1));

                Vector3 pos = transform.TransformPoint(vertices[i]);
                Vector3 finalPos = Vector3.zero;
                finalPos.x = pos.x;
                finalPos.z = pos.y;
                finalPos.y = pos.z;

                colliders[i].transform.position = finalPos + new Vector3(0, collisionYOffset, 0);
            }

            waterPlane.GetComponent<MeshFilter>().mesh.vertices = vertices;
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
