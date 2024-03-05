using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponBase currentWeapon;
    [SerializeField] DaggerRegular dagger1;
    [SerializeField] DaggerAbberant dagger2;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = dagger1;
    }

    void Update()
    {
        if (InputManager.Instance.SwapWeaponPressed)
        {
            SwapWeapon();
        }

        currentWeapon.Attack();
    }

    private void SwapWeapon()
    {
        currentWeapon = currentWeapon == dagger1 ? dagger2 : dagger1;
    }

}
