using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    public float useStamina;

    [Header("Resouce Gathering")]
    public bool doesGatherResouces;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    public override void OnAttackInput(PlayerConditions conditions)
    {
        if(!attacking)
        {
            if(conditions.UseStamina(useStamina))
            {
                attacking = true;
                animator.SetTrigger("Attack");
                Invoke("OnCanAttack", attackRate);
            }
        }
    }
    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            if (doesGatherResouces && hit.collider.TryGetComponent(out Resource resouce))
            {
                resouce.Gather(hit.point, hit.normal);
            }

            if(doesDealDamage && hit.collider.TryGetComponent(out IDamagable damageable))
            {
                damageable.TakePhysicalDamage(damage);
            }

        }
    }
}
