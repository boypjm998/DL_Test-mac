using UnityEngine;
using System.Collections;

public class MyInputModule
{
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;

    private bool curState = false;
    private bool lastState = false;


    public void Tick(bool input)
    {
        curState = input;
        IsPressing = curState;


        OnPressed = false;
        OnReleased = false;

        if (curState != lastState)
        {
            if (curState)
            {
                OnPressed = true;
            }
            else {
                OnReleased = true;
            }
        }

        lastState = curState;

    }



}
