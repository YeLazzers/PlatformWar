public class PlayerAttacker : AttackerBase
{
    public void Attack(bool isMoving)
    {
        if (IsAttackAvailable == false)
            return;

        DisallowAtack();

        if (isMoving)
            Animator.SetAttack();
        else
            Animator.SetRandomAttack();        
    }
}