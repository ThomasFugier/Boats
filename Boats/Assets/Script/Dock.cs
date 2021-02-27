using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    public enum DockType {Fish};

    public DockType dockType = DockType.Fish;
    public Collider dockCollider;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent.transform.parent.gameObject.GetComponent<PlayerManager>().SetDockState(State.Docked, this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent.transform.parent.gameObject.GetComponent<PlayerManager>().SetDockState(State.InSea, null);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0f, 1, 0.8f);
        Gizmos.DrawCube(dockCollider.transform.position, dockCollider.transform.localScale);
    }
}
