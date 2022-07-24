using System;
using System.Collections.Generic;
using UI_Inputs;
using UI_InputSystem.Base;
using UnityEngine;


public class UIInputSystem : MonoBehaviour
{
    public static UIInputSystem ME { get; private set; }
    public static UIInputSystem instance;

    private Dictionary<ButtonAction, UIInputButton> uiButtonInputs;
    private Dictionary<JoyStickAction, UIInputJoystick> uiJoySticks;
        
    private void Awake()
    {
        instance = this;
        ME = this;

        CreateButtonDictionary();
        CreateJoystickDictionary();
    }

    private void CreateButtonDictionary()
    {
        if (uiButtonInputs != null)
            return;

        uiButtonInputs = UIInputsFinder.GetAvailableInputs<ButtonAction, UIInputButton, bool>();
    }

    private void CreateJoystickDictionary()
    {
        if (uiJoySticks != null)
            return;

        uiJoySticks = UIInputsFinder.GetAvailableInputs<JoyStickAction, UIInputJoystick, Vector2>();
    }

    private bool ButtonPressProcessor(ButtonAction buttonToCheckPress)
    {
        return uiButtonInputs.ContainsKey(buttonToCheckPress) && uiButtonInputs[buttonToCheckPress].InputValue;
    }

    private Vector2 JoyStickProcessor(JoyStickAction joyStickToCheckPress)
    {
        return uiJoySticks.ContainsKey(joyStickToCheckPress) ? uiJoySticks[joyStickToCheckPress].InputValue : Vector2.zero;
    }

    public Vector2 GetAxis(JoyStickAction joystickToChek) => JoyStickProcessor(joystickToChek);
    public float GetAxisHorizontal(JoyStickAction joystickToChek) => JoyStickProcessor(joystickToChek).x;
    public float GetAxisVertical(JoyStickAction joystickToChek) => JoyStickProcessor(joystickToChek).y;
    public bool GetButton(ButtonAction buttonToCheck) => ButtonPressProcessor(buttonToCheck);
        
    public void AddOnClickEvent(ButtonAction action, Action @event) => uiButtonInputs[action].OnClick += @event;
    public void AddOnTouchEvent(ButtonAction action, Action @event) => uiButtonInputs[action].OnTouch += @event;
    public void RemoveOnClickEvent(ButtonAction action, Action @event) => uiButtonInputs[action].OnClick -= @event;
    public void RemoveOnTouchEvent(ButtonAction action, Action @event) => uiButtonInputs[action].OnTouch -= @event;
}