using System.Collections;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TMP_Text))]
public class AffichageResultat : MonoBehaviour
{
    [Header("Pop")]
    [SerializeField] private float dureePop = 0.3f;

    [Header("Suivi")]
    [Tooltip("Objet du monde que le chiffre suit (le Player). Laisse vide pour un chiffre fixe.")]
    [SerializeField] private Transform cible;

    private TMP_Text texte;
    private Vector3 echelleInitiale;
    private Vector3 decalageMonde; // écart entre la cible et le point d'apparition

    private void Awake()
    {
        texte = GetComponent<TMP_Text>();
        echelleInitiale = transform.localScale;
        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (cible == null || Camera.main == null) return;
        transform.position = Camera.main.WorldToScreenPoint(cible.position + decalageMonde);
    }

    /// Affiche la valeur à l'endroit donné (position monde), avec un pop
    public void Afficher(int valeur, Vector3 positionMonde)
    {
        StopAllCoroutines();

        texte.text = valeur.ToString();

        // Le chiffre apparaît exactement où était le dé, puis suit la cible avec le même écart
        if (cible != null) decalageMonde = positionMonde - cible.position;
        transform.position = Camera.main.WorldToScreenPoint(positionMonde);

        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);

        StartCoroutine(Pop());
    }

    /// Affiche la valeur à la même place que le dernier affichage (par rapport à la cible),
    /// avec un pop. Sert à réafficher le chiffre pour un déplacement dû à un effet de case.
    public void Reafficher(int valeur)
    {
        StopAllCoroutines();

        texte.text = valeur.ToString();

        if (cible != null && Camera.main != null)
            transform.position = Camera.main.WorldToScreenPoint(cible.position + decalageMonde);

        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);

        StartCoroutine(Pop());
    }

    /// Change le nombre affiché sans rejouer le pop
    public void MettreAJour(int valeur)
    {
        texte.text = valeur.ToString();
    }

    public void Cacher()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private IEnumerator Pop()
    {
        float temps = 0f;
        while (temps < dureePop)
        {
            temps += Time.deltaTime;
            float t = Mathf.Clamp01(temps / dureePop);
            transform.localScale = echelleInitiale * EaseOutBack(t);
            yield return null;
        }
        transform.localScale = echelleInitiale;
    }

    private static float EaseOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }
}