using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilitys : MonoBehaviour
{

    public MeshRenderer mesh;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Evaluando si esconder entidad");
        if (other.gameObject.CompareTag("LandToHide"))
        {
            this.mesh.enabled = false;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Evaluando si esconder entidad");
        if (other.gameObject.CompareTag("LandToHide"))
        {
            this.mesh.enabled = true;
        }
    }
}
