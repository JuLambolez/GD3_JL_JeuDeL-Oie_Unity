using System.Collections.Generic;
using UnityEngine;

/// Les cases sont des Transforms qu'on place où on veut (forme libre)
/// Par défaut, ce sont les enfants de cet objet, dans l'ordre de la hiérarchie
public class Plateau : MonoBehaviour
{
    [SerializeField] private List<Transform> cases = new List<Transform>();

    [Header("Debug")]
    [SerializeField] private bool afficherGizmos = true;

    public int NombreCases => cases.Count;
    public int IndexDerniereCase => cases.Count - 1;

    private void Awake()
    {
        if (cases.Count == 0) RemplirDepuisEnfants();
    }

    /// Remplit la liste avec les enfants directs, dans l'ordre
    [ContextMenu("Remplir depuis les enfants")]
    public void RemplirDepuisEnfants()
    {
        cases.Clear();
        foreach (Transform enfant in transform) cases.Add(enfant);
    }

    public Vector3 GetPosition(int index)
    {
        return cases[index].position;
    }

    /// Renvoie l'effet de la case, ou null si c'est une case normale
    public EffetCase GetEffet(int index)
    {
        return cases[index].GetComponent<EffetCase>();
    }

    private void OnDrawGizmos()
    {
        if (!afficherGizmos) return;

        // En édition, on lit les enfants si la liste est vide
        IList<Transform> liste = cases;
        if (liste.Count == 0)
        {
            var temp = new List<Transform>();
            foreach (Transform enfant in transform) temp.Add(enfant);
            liste = temp;
        }

        for (int i = 0; i < liste.Count; i++)
        {
            if (liste[i] == null) continue;

            Gizmos.color = i == 0 ? Color.green : (i == liste.Count - 1 ? Color.red : Color.yellow);
            Gizmos.DrawSphere(liste[i].position, 0.15f);

            if (i < liste.Count - 1 && liste[i + 1] != null)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(liste[i].position, liste[i + 1].position);
            }
        }
    }
}