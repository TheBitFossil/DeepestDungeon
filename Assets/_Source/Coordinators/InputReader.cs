using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Source.Coordinators
{
    public class InputReader : MonoBehaviour, PlayerInputActions.IPlayerActions
    {
        private Game game;

        private static bool canAttack;

        public enum MovementVectors
        {
            StandingStill = 0,
            MovingX = 1,
            MovingY = 2,
            Moving = MovingX | MovingY //BitWise OR
        }
    
        public MovementVectors MovementState
        {
            get
            {
                MovementVectors retVal = MovementVectors.StandingStill;
            
                if (Mathf.Abs(MovementValue.x) <= Mathf.Epsilon)
                {
                    retVal |= MovementVectors.MovingX;
                }            
            
                if (Mathf.Abs(MovementValue.y) <= Mathf.Epsilon)
                {
                    retVal |= MovementVectors.MovingY;
                }

                return retVal;
            }
        }
        
        private Vector3 _inputDirection = Vector3.zero;
        private PlayerInputActions playerInputActions;
        private PlayerInputActions uiInputActions;
        private static bool stopForwardMovement = false;

        #region Properties

        public static bool StopForwardMovement
        {
            get => stopForwardMovement;
            set => stopForwardMovement = value;
        }
        
        public static Vector2 MovementValue { get; private set; }
    
        public static bool IsStandingStill => MovementValue == Vector2.zero;
        
        public static bool CanAttack
        {
            get => canAttack;
            set => canAttack = value;
        }

        #endregion
        
        #region Events
        public static event Action BaseAttackEvent;
        public static event Action HeavyAttackEvent;
        public static event Action DodgeEvent;
        public static event Action ToggleInventoryEvent;
        public static event Action TogglePauseMenuEvent;
        public static event Action AnyKeyEvent;
        #endregion

        #region Unity Engine

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.SetCallbacks(this);
            playerInputActions.Enable();

            game = FindObjectOfType<Game>();
        }

        private void OnDestroy()
        {
            playerInputActions.Disable();
        }
    
        public void OnMovement(InputAction.CallbackContext context)
        {
            if (stopForwardMovement)
            {
                var rotOnly = new Vector2(0f, context.ReadValue<Vector2>().x);
                MovementValue = rotOnly;
            }
            
            MovementValue = context.ReadValue<Vector2>();
        }
        
        public void OnCameraRotation(InputAction.CallbackContext context)
        {
            //...
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            ToggleInventoryEvent?.Invoke();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            TogglePauseMenuEvent?.Invoke();
        }

        public void OnAnyKey(InputAction.CallbackContext context)
        {
            AnyKeyEvent?.Invoke();
        }

        public void OnBaseAttack(InputAction.CallbackContext context)
        {
            if (false == canAttack) { return;}
            BaseAttackEvent?.Invoke();
        }

        public void OnHeavyAttack(InputAction.CallbackContext context)
        {
            if (false == canAttack) { return;}
            HeavyAttackEvent?.Invoke();
        }

        public void OnDodge(InputAction.CallbackContext context)
        { 
            DodgeEvent?.Invoke();
        }
        
        #endregion

    }
}