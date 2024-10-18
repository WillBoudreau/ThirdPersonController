using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class WeaponsManager : MonoBehaviour
{
    public CameraManager camMan;
    public Camera playercam;
    public LayerMask cubefilter;
    public LayerMask Ground;
    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

    void Start()
    {
        playercam = camMan.playerCamera;
    }

    void FixedUpdate()
    {
        float dist = 10;
        if (Physics.Raycast(playercam.transform.position, playercam.transform.forward, out RaycastHit hit, 20, Ground.value))
        {
            dist = hit.distance;
            Debug.Log($"Distance is {dist}");
        }

        Vector3 CamPos = playercam.transform.position;
        Debug.DrawLine(CamPos, playercam.transform.forward * 20);

        RaycastHit[] hits = Physics.RaycastAll(playercam.transform.position, playercam.transform.forward, dist, cubefilter.value);
        HashSet<Renderer> hitRenderers = new HashSet<Renderer>();

        foreach (RaycastHit hit2 in hits)
        {
            if (hit2.collider.TryGetComponent(out Renderer renderer))
            {
                if (!originalColors.ContainsKey(renderer))
                {
                    originalColors[renderer] = renderer.material.color;
                }
                renderer.material.color = Color.red;
                hitRenderers.Add(renderer);
                Debug.Log($"Hit object: {hit2.collider.gameObject.name}");
            }
        }

        // Reset colors of renderers that are no longer being hit
        foreach (var original in originalColors)
        {
            if (!hitRenderers.Contains(original.Key))
            {
                original.Key.material.color = original.Value;
            }
        }

        Debug.Log($"Hit {hits.Length} cubes");
    }
}