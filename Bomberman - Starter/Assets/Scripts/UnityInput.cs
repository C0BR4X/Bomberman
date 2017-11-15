using UnityEngine;

public class UnityInput : IUnityInput
{
    public bool KeyPressed(KeyCode keyCode)
    {
        return (Input.GetKey(keyCode));
    }

}