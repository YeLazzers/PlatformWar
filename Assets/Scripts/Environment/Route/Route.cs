using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _waypoints;


    public Waypoint GetNearestWaypoint(Vector3 position)
    {
        return _waypoints.OrderBy(w => (w.transform.position - position).sqrMagnitude).FirstOrDefault();
    }

    public Waypoint GetNextWaypoint(int currentIndex) => _waypoints[++currentIndex % _waypoints.Count];

    public Waypoint GetNextWaypoint(Waypoint current)
    {
        int currentIndex = _waypoints.FindIndex(w => w == current);
        return _waypoints[++currentIndex % _waypoints.Count];
    }

    public Vector3 GetNextWaypointPosition(int currentIndex) => _waypoints[++currentIndex % _waypoints.Count].transform.position;

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _waypoints = new();

        for (int i = 0; i < pointCount; i++)
            _waypoints.Add(transform.GetChild(i).GetComponent<Waypoint>());
    }
#endif

}
