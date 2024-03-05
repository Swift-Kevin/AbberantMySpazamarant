using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerRegular : WeaponBase
{
    public override void Attack()
    {
        Debug.Log("Regular Attack Called");
        if (InputManager.Instance.Action.Attack.WasPressedThisFrame() && canUseWeapon)
        {
            GetComponent<Animator>().Play(weapon.AnimationName, -1, 0f);
            weaponTimer.enabled = true;
            weaponTimer.StartTimer(weapon.CD);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, weapon.AtkDist))
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(attack.CurrValue);
                }
            }
        }
    }
}
