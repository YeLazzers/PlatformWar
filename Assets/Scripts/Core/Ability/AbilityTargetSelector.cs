using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AbilityTargetSelector
{
    private static readonly float _minRaduis = 0.05f;

    public static List<Unit> TryGetTargets(AbilityConfig config, AbilityContext context)
    {
        switch (config.TargetingPolicy)
        {
            case AbilityTargetingPolicy.Nearest:
                {
                    return GetNearestTarget(context);
                }
        }
        return null;
    }

    private static List<Unit> GetNearestTarget(AbilityContext context)
    {
        Vector3 centerPosition = (Vector3)context.Data.GetValueOrDefault(AbilityContextDataKeys.Point);
        float radius = (float)context.Data.GetValueOrDefault(AbilityContextDataKeys.Radius);
        LayerMask layerMask = (LayerMask)context.Data.GetValueOrDefault(AbilityContextDataKeys.LayerMask);

        List<Collider2D> colliders = Physics2D.OverlapCircleAll(centerPosition, Mathf.Max(_minRaduis, radius), layerMask).ToList();

        Unit nearestUnit = colliders
            .Where(CheckIsUnit)
            .OrderBy(x => (x.transform.position - (Vector3)centerPosition).sqrMagnitude)
            .FirstOrDefault()
            ?.attachedRigidbody.GetComponent<Unit>();

        return nearestUnit != null ? new List<Unit>() { nearestUnit } : null;
    }

    private static bool CheckIsUnit(Collider2D collider) =>
        collider.isTrigger == false && collider.attachedRigidbody.GetComponent<Unit>() != null;
}