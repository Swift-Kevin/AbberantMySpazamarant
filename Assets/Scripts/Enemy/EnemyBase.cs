using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyStatManager enemy_statManager;
    [SerializeField] private MeshRenderer mesh;

    Color origColor;

    // Start is called before the first frame update
    void Start()
    {
        if (mesh == null)
            mesh = GetComponent<MeshRenderer>();

        if (mesh != null)
            origColor = mesh.material.color;

        enemy_statManager.Health.OnDepleted += Health_OnDepleted;
    }

    private void Health_OnDepleted()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        enemy_statManager.Health.UseResource(damage);

        if (enemy_statManager.Health.Valid)
        {
            StartCoroutine(FlashDamage());
        }
    }

    private IEnumerator FlashDamage()
    {
        mesh.material.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        mesh.material.color = origColor;

    }
}
