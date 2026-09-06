using System;
using JAGD.Kit.Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAGD.Kit.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }

        public bool JumpPressed { get; private set; }
        public bool DashPressed { get; private set; }

        public event Action OnJumpPressed;
        public event Action OnJumpCanceled;

        public event Action OnDashPressed;
        public event Action OnDashCanceled;

        public event Action OnInteractPressed;
        public event Action OnAttackPressed;
        public event Action OnPausePressed;

        // ---

        public void OnMove(InputAction.CallbackContext ctx)
        {
            MoveInput = ctx.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext ctx)
        {
            LookInput = ctx.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                JumpPressed = true;
                OnJumpPressed?.Invoke();
            }
            else if (ctx.canceled)
            {
                JumpPressed = false;
                OnJumpCanceled?.Invoke();
            }
        }

        public void OnDash(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                DashPressed = true;
                OnDashPressed?.Invoke();
            }
            else if (ctx.canceled)
            {
                DashPressed = false;
                OnDashCanceled?.Invoke();
            }
        }

        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                OnInteractPressed?.Invoke();
            }
        }

        public void OnAttack(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                OnAttackPressed?.Invoke();
            }
        }

        public void OnPause(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                OnPausePressed?.Invoke();
            }
        }
    }
}
