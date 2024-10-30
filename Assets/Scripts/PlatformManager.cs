using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints = new Transform[3];
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool ShowGizmos = true;
    [SerializeField] private float arronwSize = 0.5f;
    [SerializeField] private float arrowAmount = 1;

    private int currentWaypoint = 0;
    private float distance = 0.1f;

 

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetPos = waypoints[currentWaypoint].position;

        transform.position = Vector3.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, TargetPos) <= distance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
    private void DrawArrow(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        float pathLength = Vector3.Distance(start, end);
        int arrowCount = Mathf.Max(1,Mathf.FloorToInt(pathLength * arrowAmount));

        for(float i = 0; i < arrowCount; i++)
        {
            float t = (i + 0.5f) / arrowCount;
            Vector3 arrowPos = Vector3.Lerp(start, end, t);

            Vector3 right = Vector3.Cross(Vector3.up, direction).normalized;
            Vector3 arrowTip = arrowPos + direction * (arronwSize * 0.5f);
            Vector3 arrowBase = arrowPos - direction * (arronwSize * 0.5f);
            Vector3 arrowRight = arrowBase + right * (arronwSize * 0.25f);
            Vector3 arrowLeft = arrowBase - right * (arronwSize * 0.25f);

            Gizmos.DrawLine(arrowBase, arrowTip);
            Gizmos.DrawLine(arrowRight, arrowTip);
            Gizmos.DrawLine(arrowLeft, arrowTip);
        }
    }
    private void OnDrawGizmos()
    {
        if(!ShowGizmos|| waypoints == null || waypoints.Length !=3)
        {
            return;
        }
        for(int i = 0; i < waypoints.Length; i++)
        {
            if(waypoints[i] != null)
            {
                Gizmos.color = (i == currentWaypoint) ? Color.green : Color.red;

                Gizmos.DrawWireSphere(waypoints[i].position, 0.25f);
#if UNITY_EDITOR
            Vector3 textPos = waypoints[i].position + Vector3.up * 0.5f;
            Handles.Label(textPos,(i+1).ToString(),new GUIStyle(){
                normal = new GUIStyleState() {textColor = (i == currentWaypoint) ? Color.green : Color.red},
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            });
#endif
                if(i < waypoints.Length - 1 && waypoints[i + 1] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                    DrawArrow(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
        if(waypoints[0] != null && waypoints[waypoints.Length - 1] != null)
        {
            Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);
            DrawArrow(waypoints[waypoints.Length - 1].position, waypoints[0].position);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

}
