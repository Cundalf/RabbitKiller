using UnityEngine;

public class BustController : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void applyBust(string bust)
    {
        switch (bust)
        {
            case "redBushBust":
                redBushBust();
                break;
            case "purpleBushBust":
                purpleBushBust();
                break;
            default: return;
        }
    }

    private void redBushBust()
    {
        if (playerController.health < 4)
        {
            playerController.health++;
        }
    }

    private void purpleBushBust()
    {
        playerController.GetComponent<WeaponController>().currentWeapon.ammoInCharger = 10;
    }
}
