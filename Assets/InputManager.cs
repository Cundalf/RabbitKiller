using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.SharedInstance.actualGameState == GameManager.GameState.IN_GAME)
        {
            // Ataque del personaje (default: left mouse)
            if (Input.GetButtonUp("Fire1"))
            {
                return;
            }

            // Ataque secundario del personaje (default: right mouse)
            if (Input.GetButtonUp("Fire2"))
            {
                return;
            }
        }

        // Menu (default: Escape)
        if (Input.GetButtonUp("Cancel"))
        {
            switch(GameManager.SharedInstance.actualGameState)
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
