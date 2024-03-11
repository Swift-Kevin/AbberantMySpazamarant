using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerCounter))]
public class EnemyMeshManipulator : MonoBehaviour
{
    [Header("Misc")]
    [SerializeField] private TimerCounter flashDamageTimer;
    [SerializeField] private MeshRenderer mesh;

    [Header("Materials")]
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material hitMaterial;

    private void Start()
    {
        if (mesh == null)
        {
            mesh = GetComponent<MeshRenderer>();
            // get a color, if there isnt a mesh then add one and get its colors
            baseMaterial = (mesh != null) ? mesh.material : gameObject.AddComponent<MeshRenderer>().material;
        }

        if (flashDamageTimer == null)
        {
            flashDamageTimer = GetComponent<TimerCounter>();
        }

        mesh.material = baseMaterial;
    }

    public void FlashMesh()
    {
        flashDamageTimer.StartTimer();
    }

    private void OnEnable()
    {
        flashDamageTimer.OnEnded += FlashTimer_OnEnded;
        flashDamageTimer.OnStarted += FlashTimer_OnStart;
        flashDamageTimer.OnRestarted += FlashTimer_OnStart;
    }

    private void OnDisable()
    {
        flashDamageTimer.OnEnded -= FlashTimer_OnEnded;
        flashDamageTimer.OnStarted -= FlashTimer_OnStart;
        flashDamageTimer.OnRestarted -= FlashTimer_OnStart;
    }

    private void FlashTimer_OnStart()
    {
        mesh.material = hitMaterial;
    }

    private void FlashTimer_OnEnded()
    {
        mesh.material = baseMaterial;
    }
}
