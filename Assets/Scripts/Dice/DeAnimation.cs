using System;
using System.Collections;
using UnityEngine;


public class DeAnimation : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private Vector3 axeRotation = new Vector3(1f, 1f, 0f);
    [SerializeField] private float vitesseRotation = 360f;

    [Header("Animation de fin")]
    [SerializeField] private float dureeRalentissement = 0.8f;
    [SerializeField] private float hauteurMontee = 0.5f;

    /// Déclenché à la fin de l'animation, avec la position monde du dé
    public event Action<Vector3> AnimationTerminee;

    private Vector3 positionInitiale;
    private bool enRotation;

    private void Awake()
    {
        positionInitiale = transform.localPosition;
        gameObject.SetActive(false); // caché au départ
    }

    private void Update()
    {
        if (!enRotation) return;
        transform.Rotate(axeRotation.normalized * vitesseRotation * Time.deltaTime, Space.Self);
    }

    /// Affiche le dé et le fait tourner
    public void DemarrerRotation()
    {
        StopAllCoroutines();
        transform.localPosition = positionInitiale;
        gameObject.SetActive(true);
        enRotation = true;
    }

    /// Ralentit le dé, le fait monter, puis le cache
    public void ArreterRotation()
    {
        if (!enRotation) return;
        enRotation = false;
        StartCoroutine(AnimationFin());
    }

    private IEnumerator AnimationFin()
    {
        Vector3 depart = positionInitiale;
        Vector3 arrivee = positionInitiale + Vector3.up * hauteurMontee;
        float temps = 0f;

        while (temps < dureeRalentissement)
        {
            temps += Time.deltaTime;
            float t = Mathf.Clamp01(temps / dureeRalentissement);
            float lisse = Mathf.SmoothStep(0f, 1f, t);

            float vitesse = vitesseRotation * (1f - lisse);
            transform.Rotate(axeRotation.normalized * vitesse * Time.deltaTime, Space.Self);
            transform.localPosition = Vector3.Lerp(depart, arrivee, lisse);

            yield return null;
        }

        Vector3 positionMonde = transform.position;
        AnimationTerminee?.Invoke(positionMonde);
        gameObject.SetActive(false);
    }
}