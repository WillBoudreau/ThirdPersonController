using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public PickUp PickUp;
    public enum InteractableOBJ
    {
        Door,
        Button,
        Pickup
    }
    public InteractableOBJ type;

    public void Acitivate()
    {
        if (type == InteractableOBJ.Pickup)
        {
            PickUp.Interact();
            gameObject.SetActive(false);
        }
        else if (type == InteractableOBJ.Button)
        {
            Debug.Log("Yippeee! You pressed the button");
        }
        else if(type == InteractableOBJ.Door)
        {
            gameObject.SetActive(false);
        }
    }

}
