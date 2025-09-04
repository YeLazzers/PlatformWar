using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ATSNearestInAOE", menuName = "Abilities/Target Selectors/AOE.Nearest")]
public class AbilityTargetSelectorNearestInAOE : AbilityTargetSelectorBase
{
    public override List<Unit> TryGetTargets(AbilityContext context)
    {
        Vector3 centerPosition = (Vector3)context.Data.GetValueOrDefault(AbilityContextDataKeys.Point);
        float radius = (float)context.Data.GetValueOrDefault(AbilityContextDataKeys.Radius);
        LayerMask layerMask = (LayerMask)context.Data.GetValueOrDefault(AbilityContextDataKeys.LayerMask);

        List<Collider2D> colliders = Physics2D.OverlapCircleAll(centerPosition, radius, layerMask).ToList();

        Unit nearestUnit = colliders
            .Where(x => x.isTrigger == false && x.attachedRigidbody.GetComponent<Unit>() != null)
            .OrderBy(x => (x.transform.position - (Vector3)centerPosition).sqrMagnitude)
            .FirstOrDefault()
            ?.attachedRigidbody.GetComponent<Unit>();

        return nearestUnit != null ? new List<Unit>() { nearestUnit } : null;
    }
}