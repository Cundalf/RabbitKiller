using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        if (GameManager.SharedInstance.ActualGameState == GameManager.GameState.IN_GAME)
        {
            // Ataque del personaje (default: left mouse)
            if (Input.GetButton("Fire1")) 
            {
                player.GetComponent<WeponController>().shoot();
                return;
            }
            // Ataque secundario del personaje (default: right mouse)
            if (Input.GetButtonUp("Fire2"))
            {
                return;
            }

            // Boton de cambiar arma (default: q)
            if (Input.GetButtonUp("ChangeWeapon"))
            {
                player.GetComponent<WeponController>().quickChangeOfWeapon();
                return;
            }

            // Boton de recarga (default: r)
            if (Input.GetButtonUp("Reload"))
            {
                player.GetComponent<WeponController>().reload();
                return;
            }

        }
        
        // Menu (default: Escape)
        if (Input.GetButtonUp("Cancel"))
        {
            switch(GameManager.SharedInstance.ActualGameState)
            {
                case GameManager.GameState.IN_GAME:
                    GameManager.SharedInstance.PauseGame();
                    break;
                case GameManager.GameState.PAUSE:
                    GameManager.SharedInstance.ResumeGame();
                    break;
            }
            return;
        }
    }
}
