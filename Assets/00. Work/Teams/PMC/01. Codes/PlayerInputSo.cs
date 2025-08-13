using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00._Work.Teams.PMC._01._Codes
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSo")]
    public class PlayerInputSo : ScriptableObject, Controls.IPlayerActions
    {
        private Controls controls;

        public event Action ToggleMenu;
        
        private void OnEnable()
        {
            if (controls == null)
            {
                controls = new Controls();
                controls.Player.SetCallbacks(this);
            }
            controls.Player.Enable();
        }

        private void OnDisable()
        {
            controls.Player.Disable();
        }

        public void OnESC(InputAction.CallbackContext context)
        {
            ToggleMenu?.Invoke();
        }
    }
}