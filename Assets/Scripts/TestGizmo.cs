using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 2));
    }
}

