using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShowroom : MonoBehaviour
{
    private static PlayerShowroom _instance;

    public static PlayerShowroom Instance { get { return _instance; } }

    public bool isRecording = false;

    public float speed;

    [Header("References")]
    public Transform player1;
    public Transform player2;
    public Transform player3;
    public Transform player4;
    public Camera player1_Camera;
    public Camera player2_Camera;
    public Camera player3_Camera;
    public Camera player4_Camera;

    private Coroutine coroutine_showroom;

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

    public void StartRecording()
    {
        isRecording = true;
        player1_Camera.enabled = true;
        player2_Camera.enabled = true;
        player3_Camera.enabled = true;
        player4_Camera.enabled = true;

        coroutine_showroom = StartCoroutine(Showroom());
    }

    public void StopRecording()
    {
        isRecording = false;
        player1_Camera.enabled = false;
        player2_Camera.enabled = false;
        player3_Camera.enabled = false;
        player4_Camera.enabled = false;

        StopCoroutine(coroutine_showroom);
    }

    IEnumerator Showroom()
    {
        while(true)
        {
            yield return null;

            player1.transform.Rotate(Vector3.up * speed * Time.deltaTime);
            player2.transform.Rotate(Vector3.up * speed * Time.deltaTime);
            player3.transform.Rotate(Vector3.up * speed * Time.deltaTime);
            player4.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
