using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stat", menuName = "Stats/New Character Stats")]
public class CharacterStats : ScriptableObject
{
    public string characterName;

    public void ShowName()
    {
        Debug.Log("Character Name: " + characterName);
    }
}
