using UnityEngine;

public class BustController : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    //? Podemos cambiarlo a un Enum? - Cunda
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
        playerController.heal();
    }

    private void purpleBushBust()
    {
        playerController.GetComponent<WeaponController>().currentWeapon.AmmoBonus = 10;
    }
}
