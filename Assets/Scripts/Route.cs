using System.Linq;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;

    public Waypoint GetNextWaypoint(int currentIndex) => _waypoints[++currentIndex % _waypoints.Length];

    public Vector3 GetNextWaypointPosition(int currentIndex) => _waypoints[++currentIndex % _waypoints.Length].transform.position;

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _waypoints = new Waypoint[pointCount];

        for (int i = 0; i < _waypoints.Length; i++)
            _waypoints[i] = transform.GetChild(i).GetComponent<Waypoint>();
    }
#endif

}
