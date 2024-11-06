using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableOBJ
    {
        Door,
        Button,
        Pickup
    }
    public InteractableOBJ type;

    public void Acitivate()
    {
        Debug.Log(this.name + " Was activiated");
    }

}
