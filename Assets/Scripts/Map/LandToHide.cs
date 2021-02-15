using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandToHide : MonoBehaviour
{
    private string VALID_TYPE = "Enemy";

    public void OnCollisionEnter(Collision other)
    {
        if (isAValidGameObject(other.gameObject))
        {
            other.gameObject.SetActive(false);
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (isAValidGameObject(other.gameObject))
        {
            other.gameObject.SetActive(true);
        }
    }

    private bool isAValidGameObject(GameObject gObject) 
    {
        return gObject.CompareTag(VALID_TYPE);
    }

}
