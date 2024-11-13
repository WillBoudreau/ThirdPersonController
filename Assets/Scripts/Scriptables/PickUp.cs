using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Pick Up")]
public class PickUp : ScriptableObject
{
    public string Name;
    public void Interact()
    {
        Debug.Log($"You have picked up {Name}");
    }
}
