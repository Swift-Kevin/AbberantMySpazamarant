using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeFragmentScript : MonoBehaviour
{
    [SerializeField] BoxCollider coll;
    [SerializeField] Rigidbody rb;
    [SerializeField] DaggerAbberant abbScript;

    private void Start()
    {
        coll = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        coll.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable dmg = collision.gameObject.GetComponent<IDamageable>();
        IPushable push = collision.gameObject.GetComponent<IPushable>();

        //Debug.Log(collision);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        coll.enabled = true;

        if (dmg != null)
        {
            dmg.TakeDamage(abbScript.Dmg);
        }

        if (push != null)
        {
            push.Push(/*collision.contacts[0].normal*/transform.forward * 10f);
        }
    }
}
