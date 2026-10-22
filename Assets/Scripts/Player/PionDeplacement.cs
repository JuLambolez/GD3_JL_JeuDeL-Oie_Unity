using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PionDeplacement : MonoBehaviour
{
    [Header("Déplacement")]
    [SerializeField] private float dureeParCase = 0.35f;
    [SerializeField] private float hauteurSaut = 0.3f;
    [SerializeField] private Vector3 decalage = Vector3.zero; // ex : (0, 1, 0) si le pivot est aux pieds

    public bool EnDeplacement { get; private set; }

    /// Déclenché à chaque case atteinte, avec le numéro du pas (1, 2, 3...)
    public event Action<int> PasTermine;

    public event Action DeplacementTermine;

    /// Téléporte le pion instantanément
    public void Placer(Vector3 positionMonde)
    {
        transform.position = positionMonde + decalage;
    }

    /// Fait avancer le pion le long des positions, case par case
    public void Suivre(IReadOnlyList<Vector3> positions)
    {
        if (EnDeplacement) return;
        StartCoroutine(SuivreCoroutine(positions));
    }

    private IEnumerator SuivreCoroutine(IReadOnlyList<Vector3> positions)
    {
        EnDeplacement = true;

        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 depart = transform.position;
            Vector3 arrivee = positions[i] + decalage;
            float temps = 0f;

            while (temps < dureeParCase)
            {
                temps += Time.deltaTime;
                float t = Mathf.Clamp01(temps / dureeParCase);

                Vector3 position = Vector3.Lerp(depart, arrivee, t);
                position.y += Mathf.Sin(t * Mathf.PI) * hauteurSaut; // petit saut
                transform.position = position;

                yield return null;
            }

            transform.position = arrivee;
            PasTermine?.Invoke(i + 1);
        }

        EnDeplacement = false;
        DeplacementTermine?.Invoke();
    }
}