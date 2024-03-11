using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerRegular : WeaponBase
{
    private float specCDMult = 1;
    private float specAtkMult = 1;

    public override void Attack()
    {
        Debug.Log("Regular Attack Called");
        if (canUseWeapon)
        {
            transform.parent.GetComponent<Animator>().Play(weapon.RandAnimName, -1, 0f);
            weaponTimer.enabled = true;
            weaponTimer.StartTimer(weapon.CD * specCDMult);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, weapon.AtkDist))
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(attack.CurrValue * specAtkMult);
                }
            }
        }
    }

    public override void SpecialAttack()
    {
        specCDMult = 2;
        specAtkMult = 2.5f; 
        
        Attack();

        specCDMult = specAtkMult = 1;
    }

    public override void FixBadValues()
    {
        weaponTimer.StopTimer();
    }

    public override void Sheathe()
    {
        transform.parent.GetComponent<Animator>().Play(sheatheAnim, -1, 0f);
        canUseWeapon = false;
    }

    public override void Unsheathe()
    {
        transform.parent.GetComponent<Animator>().Play(unsheatheAnim, -1, 0f);
        canUseWeapon = true;
    }
}
