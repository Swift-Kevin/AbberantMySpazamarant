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

    [SerializeField] private ParticleSystem attackParticleSystem;
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
        Debug.Log("Abberation [ATTACK] Called");
        if (InputManager.Instance.Action.Attack.WasPressedThisFrame() && canUseWeapon)
        {
            weaponTimer.enabled = true;
            weaponTimer.StartTimer(weapon.CD);

            if (sentBladeIdx <= 4)
            {
                transform.parent.GetComponent<Animator>().Play(weapon.RandAnimName, -1, 0f);

                //RaycastHit hit;
                //if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, weapon.AtkDist))
                //{
                //    IDamageable damageable = hit.collider.GetComponent<IDamageable>();

                //    if (damageable != null)
                //    {
                //        damageable.TakeDamage(attack.CurrValue);
                //    }

                //sentBladePieces.Add(Instantiate(origBladePieces[sentBladeIdx], hit.point, Quaternion.identity, attackPosParent));
                //origBladePieces[sentBladeIdx].gameObject.SetActive(false);

                origBladePieces[sentBladeIdx].parent = attackPosParent;
                origBladePieces[sentBladeIdx].GetComponent<Rigidbody>().isKinematic = false;
                origBladePieces[sentBladeIdx].GetComponent<BoxCollider>().enabled = true;
                origBladePieces[sentBladeIdx].GetComponent<Rigidbody>().useGravity = true;
                origBladePieces[sentBladeIdx].GetComponent<Rigidbody>().AddForce(PlayerBase.Instance.CamFWD * daggerForceMultiplier, ForceMode.Impulse);

                sentBladeIdx++;
                attackParticleSystem.Play();
                //}
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

        //foreach (Transform bladePiece in sentBladePieces)
        //{
        //    if (bladePiece != null)
        //        Destroy(bladePiece.gameObject);
        //}

        //sentBladePieces.Clear();
        sentBladeIdx = 0;

        // Set each orig piece back to being turned on
        ////origBladePieces.ForEach(bladePiece => bladePiece.gameObject.SetActive(true));
        //origBladePieces.ForEach(bladePiece => bladePiece.parent = hilt);
        //origBladePieces.ForEach(bladePiece => bladePiece.localPosition = Vector3.zero);

        foreach (var bladePiece in origBladePieces)
        {
            bladePiece.parent = hilt;
            
            bladePiece.GetComponent<Rigidbody>().isKinematic = true;
            bladePiece.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bladePiece.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            bladePiece.GetComponent<Rigidbody>().useGravity = false;
            
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
                    Debug.Log("Stopping Recall");
                    recall = false;
                }
            }
        }
    }
}
