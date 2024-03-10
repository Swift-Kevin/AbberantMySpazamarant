using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerAbberant : WeaponBase
{
    [Space]
    [Header("Abberant Varient Variables")]
    [SerializeField] private Transform hilt;
    [SerializeField] private Transform attackPosParent;

    [SerializeField] private List<Transform> origBladePieces;
    [SerializeField] private List<Transform> sentBladePieces;
    private int sentBladeIdx = 0;

    [SerializeField] private ParticleSystem attackParticleSystem;

    public override void Attack()
    {
        Debug.Log("Abberation [ATTACK] Called");
        if (InputManager.Instance.Action.Attack.WasPressedThisFrame() && canUseWeapon)
        {
            weaponTimer.enabled = true;
            weaponTimer.StartTimer(weapon.CD);

            if (sentBladeIdx <= 4)
            {
                transform.parent.GetComponent<Animator>().Play(weapon.RandAnimName, -1, 0f);

                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, weapon.AtkDist))
                {
                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();

                    if (damageable != null)
                    {
                        damageable.TakeDamage(attack.CurrValue);
                    }

                    if (origBladePieces.Count > 0)
                    {
                        sentBladePieces.Add(Instantiate(origBladePieces[sentBladeIdx], hit.point, Quaternion.identity, attackPosParent));
                        origBladePieces[sentBladeIdx].gameObject.SetActive(false);
                        sentBladeIdx++;
                        attackParticleSystem.Play();
                    }
                }
            }
            else // hilt attack
            {
                transform.parent.GetComponent<Animator>().Play(weapon.RandSecAnimName, -1, 0f);
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, weapon.SecAtkDist))
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

    public override void SpecialAttack()
    {
        // Old version threw errors abt indexing and destroying bad data somehow
        //Debug.Log("Abberation [SPECIAL ATTACK] Called");
        //if (sentBladePieces.Count > 0)
        //{
        //    for (int i = 0; i < sentBladePieces.Count; i++)
        //    {
        //        Destroy(sentBladePieces[i]?.gameObject);
        //        origBladePieces[i].gameObject.SetActive(true);
        //    }
        //    sentBladePieces.Clear();
        //    sentBladeIdx = 0;
        //}

        Debug.Log("Aberration [SPECIAL ATTACK] Called");

        foreach (Transform bladePiece in sentBladePieces)
        {
            if (bladePiece != null)
                Destroy(bladePiece.gameObject);
        }

        sentBladePieces.Clear();
        sentBladeIdx = 0;

        // Set each orig piece back to being turned on
        origBladePieces.ForEach(bladePiece => bladePiece.gameObject.SetActive(true));

    }
}
