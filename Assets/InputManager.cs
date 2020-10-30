using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public GameObject player;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("q"))
        {
            UnityEngine.Debug.Log("Cambio de arma");
            player.GetComponent<WeponController>().quickChangeOfWeapon();
        }

        if (Input.GetMouseButton(0)) 
        {
            player.GetComponent<WeponController>().shoot();
        }
    }
}
