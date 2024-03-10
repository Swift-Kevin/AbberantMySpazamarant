using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponBase currentWeapon;
    [Space]
    [SerializeField] private DaggerRegular dagger1;
    [SerializeField] private DaggerAbberant dagger2;
    [Space]
    [SerializeField] private GameObject dagger1Model;
    [SerializeField] private GameObject dagger2Model;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = dagger1;
        dagger1Model.SetActive(false);
        dagger2Model.SetActive(true);
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
        //currentWeapon = currentWeapon == dagger1 ? dagger2 : dagger1;
        
        if (currentWeapon == dagger1)
        {
            currentWeapon = dagger2;
            dagger1Model.SetActive(false);
            dagger2Model.SetActive(true);
        }
        else
        {
            currentWeapon = dagger1;
            dagger1Model.SetActive(true);
            dagger2Model.SetActive(false);
        }
    }

}
