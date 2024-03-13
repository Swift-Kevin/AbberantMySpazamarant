using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DaggerAbberant : WeaponBase
{
    [Space]
    [Header("Abberant Varient Variables")]
    [SerializeField] private Transform hilt;
    [SerializeField] private Transform attackPosParent;

    [SerializeField] private List<Transform> origBladePieces;

    List<Vector3> storedLocalPos = new List<Vector3>();
    List<Quaternion> storedQuatRot = new List<Quaternion>();
    private int sentBladeIdx = 0;

    [SerializeField] private float daggerForceMultiplier = 50;
    [SerializeField] private float recallMultiplier;

    public float Dmg => attack.CurrValue;

    bool recall;

    public override void Start()
    {
        base.Start();

        foreach (Transform t in origBladePieces)
        {
            if (t != null)
            {
                storedLocalPos.Add(t.localPosition);
                storedQuatRot.Add(t.localRotation);
            }
        }
    }

    public override void Attack()
    {
        //Debug.Log("Abberation [ATTACK] Called");
        if (canUseWeapon && !recall)
        {
            weaponTimer.enabled = true;
            weaponTimer.StartTimer(weapon.CD);

            if (sentBladeIdx <= 4)
            {
                animator.SetTrigger("Attack01");
                origBladePieces[sentBladeIdx].parent = attackPosParent;

                Rigidbody rbDag = origBladePieces[sentBladeIdx].GetComponent<Rigidbody>();
                rbDag.isKinematic = false;
                rbDag.useGravity = true;
                origBladePieces[sentBladeIdx].GetComponent<Rigidbody>().AddForce(PlayerBase.Instance.CamFWD * daggerForceMultiplier, ForceMode.Impulse);
                
                origBladePieces[sentBladeIdx].GetComponent<BoxCollider>().enabled = true;

                sentBladeIdx++;
            }
            else // hilt attack
            {
                animator.SetTrigger("Attack02");
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, weapon.SecAtkDist))
                {
                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                    damageable?.TakeDamage(attack.CurrValue);
                }
            }
        }
    }

    public override void SpecialAttack()
    {
        //Debug.Log("Aberration [SPECIAL ATTACK] Called");
        sentBladeIdx = 0;

        foreach (var bladePiece in origBladePieces)
        {
            bladePiece.parent = hilt;

            Rigidbody rbOnBlade = bladePiece.GetComponent<Rigidbody>();

            rbOnBlade.isKinematic = false;
            rbOnBlade.velocity = Vector3.zero;
            rbOnBlade.angularVelocity = Vector3.zero;
            rbOnBlade.useGravity = false;

            bladePiece.GetComponent<BoxCollider>().enabled = false;
        }
        recall = true;
    }

    private void Update()
    {
        if (recall)
        {
            int areAllInPlace = 0;

            for (int i = 0; i < origBladePieces.Count; i++)
            {
                origBladePieces[i].localPosition = Vector3.Lerp(origBladePieces[i].localPosition, storedLocalPos[i], Time.deltaTime * recallMultiplier);
                origBladePieces[i].localRotation = Quaternion.Lerp(origBladePieces[i].localRotation, storedQuatRot[i], Time.deltaTime * recallMultiplier);

                if (Vector3.Distance(origBladePieces[i].localPosition, storedLocalPos[i]) < 0.1f)
                {
                    areAllInPlace++;
                }
                if (areAllInPlace == 5)
                {
                    //Debug.Log("Stopping Recall");
                    recall = false;
                }
            }
        }
    }

    public override void FixBadValues()
    {
        if (recall)
        {
            for (int i = 0; i < origBladePieces.Count; i++)
            {
                origBladePieces[i].localPosition = storedLocalPos[i];
                origBladePieces[i].localRotation = storedQuatRot[i];
            }

            recall = false;
        }

        weaponTimer.StopTimer();
    }
    
    public override void Sheathe()
    {
        animator.SetTrigger("Sheathe");
        canUseWeapon = false;
    }

    public override void Unsheathe()
    {
        animator.SetTrigger("Unsheathe");
        //Debug.Log("Unsheathe");
        canUseWeapon = true;
    }
}
