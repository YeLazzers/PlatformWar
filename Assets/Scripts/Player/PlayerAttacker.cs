public class PlayerAttacker : CharacterAttackerBase
{
    public void Attack(bool isMoving)
    {
        if (!_isAttackAvailable)
            return;

        DisallowAtack();

        if (isMoving)
            _characterAnimator.SetAttack();
        else
            _characterAnimator.SetRandomAttack();        
    }
}