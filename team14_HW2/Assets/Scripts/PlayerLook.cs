using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public float sensitivity;

    private Vector2 InputMouse;
    private Vector3 rotation;

    public void Look(InputAction.CallbackContext context)
    {
        InputMouse = context.ReadValue<Vector2>();
        rotation = transform.rotation.eulerAngles;
        rotation.y += InputMouse.x * sensitivity * 0.1f;
        rotation.x += -InputMouse.y * sensitivity * 0.1f;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
