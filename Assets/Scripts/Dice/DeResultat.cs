using UnityEngine;


public class DeResultat : MonoBehaviour
{
    [SerializeField] private int valeurMin = 1;
    [SerializeField] private int valeurMax = 6; // inclus

    public int DernierResultat { get; private set; }

    ///Tire un nombre entre valeurMin et valeurMax (inclus)
    public int Lancer()
    {
        DernierResultat = Random.Range(valeurMin, valeurMax + 1);
        return DernierResultat;
    }
}