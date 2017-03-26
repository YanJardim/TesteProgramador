using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickEventManager : Singleton<JoystickEventManager>
{
    string[] joysticks;

    public delegate void JoystickDelegate();

    public static event JoystickDelegate OnJoystickConnected;
    public static event JoystickDelegate OnJoystickDisconnected;



    // Use this for initialization
    void Start()
    {
        joysticks = Input.GetJoystickNames();
        if (IsJoystickConnected())
        {
            OnJoystickConnected();
        }

    }

    // Update is called once per frame
    void Update()
    {
        int previousLenght = -1;
        if (joysticks != null)
            previousLenght = joysticks.Length;

        if (previousLenght != -1)
        {
            joysticks = Input.GetJoystickNames();
            if (previousLenght > joysticks.Length)
                OnJoystickDisconnected();
            else if (previousLenght < joysticks.Length)
                OnJoystickConnected();
        }
    }

    public bool IsJoystickConnected()
    {
        if (joysticks.Length > 0)
            return true;

        return false;
    }
}
