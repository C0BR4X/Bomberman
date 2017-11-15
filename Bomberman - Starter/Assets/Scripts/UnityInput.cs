using UnityEngine;

public class UnityInput : IUnityInput
{
    public bool KeyPressed(KeyCode keyCode)
    {
        return (Input.GetKey(keyCode));
    }

    public bool KeyDown(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }
}