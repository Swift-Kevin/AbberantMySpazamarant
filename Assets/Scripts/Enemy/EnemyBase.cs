using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, IDamageable, IPushable
{
    [Header("Additional Scripts")]
    [SerializeField] private EnemyStatManager enemy_statManager;
    [SerializeField] private EnemyMeshManipulator meshScript;
    [SerializeField] private EnemyMovement enemyMovementScript;
    [SerializeField] private Rigidbody rb;

    void Start()
    {
        Revive();
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        enemy_statManager.Health.OnDepleted += Health_OnDepleted;
    }

    private void OnDisable()
    {
        enemy_statManager.Health.OnDepleted -= Health_OnDepleted;
    }

    private void Health_OnDepleted()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// The main take damage function
    /// </summary>
    /// <param name="damage">Damage to apply</param>
    public void TakeDamage(float damage)
    {
        enemy_statManager.Health.Decrease(damage);
        
        if (enemy_statManager.Health.Valid)
        {
            meshScript.FlashMesh();
        }
    }

    public void Revive()
    {
        enemy_statManager.Revive();
        gameObject.SetActive(true);
    }

    public void Push(Vector3 dirToPush) 
    {
        rb.AddForce(dirToPush);
    }
}
