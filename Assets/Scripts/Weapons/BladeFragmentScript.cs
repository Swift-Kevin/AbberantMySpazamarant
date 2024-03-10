using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeFragmentScript : MonoBehaviour
{
    [SerializeField] BoxCollider coll;
    [SerializeField] DaggerAbberant abbScript;

    private void Start()
    {
        coll = GetComponent<BoxCollider>();
        coll.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable dmg = collision.gameObject.GetComponent<IDamageable>();
        
        if (dmg != null)
        {
            dmg.TakeDamage(abbScript.Dmg);
        }
    }
}
