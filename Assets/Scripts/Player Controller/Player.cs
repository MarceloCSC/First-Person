using UnityEngine;

namespace FirstPerson.PlayerController
{

    public static class Player 
    {

        public static void LockIntoPlace(bool locked)
        {
            PlayerSight.SightLocked = locked;
            PlayerMovement.MovementLocked = locked;
        }

        public static void LockCursor(bool locked)
        {
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        }

    }

}
