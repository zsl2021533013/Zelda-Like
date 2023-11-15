using System;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.View_Controller.Input_System
{
    public interface IInputProperty<T>
    {
        T Value { get; }
        public void Reset();
    }

    public class InputProperty<T> : IInputProperty<T>
    {
        public T Value { get; private set; }

        /// <summary>
        /// Create a InputProperty and add it to a InputAction
        /// </summary>
        /// <param name="action">The InputAction link to</param>
        /// <param name="startedSetter">The Value equals startedSetter when action.started</param>
        /// <param name="performedSetter">The Value equals performedSetter when action.performed</param>
        /// <param name="canceledSetter">The Value equals canceledSetter when action.canceled</param>
        public InputProperty(
            InputAction action,
            Func<InputAction.CallbackContext, T> startedSetter = null, 
            Func<InputAction.CallbackContext, T> performedSetter = null, 
            Func<InputAction.CallbackContext, T> canceledSetter = null)
        {
            Value = default;
            action.started += context => { if (startedSetter != null) { Value = startedSetter(context); } };
            action.performed += context => { if (performedSetter != null) { Value = performedSetter(context); } };
            action.canceled += context => { if (canceledSetter != null) { Value = canceledSetter(context); } };
        }

        public void Reset()
        {
            Value = default;
        }

        public static implicit operator T(InputProperty<T> inputProperty) => inputProperty.Value;
    }

    public class InputKit : Singleton<InputKit>
    {
        private InputControls mControls;

        public InputProperty<Vector2> move;
        public InputProperty<Vector2> rotate;
        public InputProperty<bool> run;
        public InputProperty<bool> jump;
        public InputProperty<bool> attack;
        public InputProperty<bool> dodge;
        public InputProperty<bool> block;
        
        private InputKit() {}
        
        public override void OnSingletonInit()
        {
            base.OnSingletonInit();

            mControls = new InputControls();
            mControls.Enable();

            move = new InputProperty<Vector2>(
                mControls.Player.Move,
                performedSetter: context => context.ReadValue<Vector2>(),
                canceledSetter: context => Vector2.zero);
            
            rotate = new InputProperty<Vector2>(
                mControls.Player.Rotate,
                performedSetter: context => context.ReadValue<Vector2>(),
                canceledSetter: context => Vector2.zero);
            
            run = new InputProperty<bool>(
                mControls.Player.Run,
                performedSetter: context => true,
                canceledSetter: context => false);
            
            jump = new InputProperty<bool>(
                mControls.Player.Jump,
                performedSetter: context => true,
                canceledSetter: context => false);
            
            attack = new InputProperty<bool>(
                mControls.Player.Attack,
                performedSetter: context => true);
            
            block = new InputProperty<bool>(
                mControls.Player.Block,
                performedSetter: context => true,
                canceledSetter: context => false);
            
            dodge = new InputProperty<bool>(
                mControls.Player.Dodge,
                performedSetter: context => true,
                canceledSetter: context => false);
        }
    }
}