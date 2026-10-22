using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LanceurDe : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private GameObject de;
    [SerializeField] private TMP_Text texteResultat;

    [Header("Rotation")]
    [SerializeField] private Key toucheLancer = Key.Space;
    [SerializeField] private Vector3 axeRotation = new Vector3(1f, 1f, 0f);
    [SerializeField] private float vitesseRotation = 360f;

    [Header("Animation de fin")]
    [SerializeField] private float dureeRalentissement = 0.8f;
    [SerializeField] private float hauteurMontee = 0.5f;

    [Header("Pop du texte")]
    [SerializeField] private float dureePop = 0.3f;

    [Header("Affichage")]
    [SerializeField] private bool texteFaceCamera = true;

    private Vector3 positionInitialeDe;
    private Vector3 echelleInitialeTexte;
    private bool enMaintien;
    private bool enAnimation;

    private void Start()
    {
        positionInitialeDe = de.transform.localPosition;
        echelleInitialeTexte = texteResultat.transform.localScale;

        de.SetActive(false);
        texteResultat.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (enAnimation) return; // on ignore la touche pendant l'animation

        var touche = Keyboard.current[toucheLancer];

        if (touche.wasPressedThisFrame)
        {
            enMaintien = true;
            texteResultat.gameObject.SetActive(false);
            de.transform.localPosition = positionInitialeDe;
            de.SetActive(true);
        }

        if (enMaintien && touche.isPressed)
        {
            de.transform.Rotate(axeRotation.normalized * vitesseRotation * Time.deltaTime, Space.Self);
        }

        if (enMaintien && touche.wasReleasedThisFrame)
        {
            enMaintien = false;
            StartCoroutine(AnimationFin());
        }
    }

    private IEnumerator AnimationFin()
    {
        enAnimation = true;

        // 1. Le dé ralentit et monte
        Vector3 depart = positionInitialeDe;
        Vector3 arrivee = positionInitialeDe + Vector3.up * hauteurMontee;
        float temps = 0f;

        while (temps < dureeRalentissement)
        {
            temps += Time.deltaTime;
            float t = Mathf.Clamp01(temps / dureeRalentissement);
            float lisse = Mathf.SmoothStep(0f, 1f, t);

            // la vitesse diminue progressivement jusqu'à 0
            float vitesse = vitesseRotation * (1f - lisse);
            de.transform.Rotate(axeRotation.normalized * vitesse * Time.deltaTime, Space.Self);

            // le dé monte en douceur
            de.transform.localPosition = Vector3.Lerp(depart, arrivee, lisse);

            yield return null;
        }

        // 2. Le dé disparaît, le résultat apparaît à sa place
        de.SetActive(false);

        int resultat = Random.Range(1, 7); // 1 à 10 inclus
        texteResultat.text = resultat.ToString();
        texteResultat.transform.position = Camera.main.WorldToScreenPoint(de.transform.position);
        texteResultat.transform.localScale = Vector3.zero;
        texteResultat.gameObject.SetActive(true);

        // 3. Petit "pop" du texte
        temps = 0f;
        while (temps < dureePop)
        {
            temps += Time.deltaTime;
            float t = Mathf.Clamp01(temps / dureePop);
            texteResultat.transform.localScale = echelleInitialeTexte * EaseOutBack(t);
            yield return null;
        }

        texteResultat.transform.localScale = echelleInitialeTexte;
        enAnimation = false;
    }

    // Courbe qui dépasse légèrement 1 avant de revenir à 1 (effet rebond)
    private float EaseOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }

    private void LateUpdate()
    {
        if (texteResultat.gameObject.activeSelf && Camera.main != null)
        {
            Vector3 positionMonde = transform.position + positionInitialeDe + Vector3.up * hauteurMontee;
            texteResultat.transform.position = Camera.main.WorldToScreenPoint(positionMonde);
        }
    }
}