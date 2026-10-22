using UnityEngine;

/// Case spéciale : fait avancer le joueur de X cases.
public class EffetAvancer : EffetCase
{
    [SerializeField] private int nombreCases = 2;

    public override void Declencher(JoueurPlateau joueur)
    {
        Debug.Log($"Case bonus : avance de {nombreCases} cases");
        joueur.Avancer(nombreCases);
    }
}