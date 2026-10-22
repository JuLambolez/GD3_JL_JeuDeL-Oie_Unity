using UnityEngine;

/// Case spéciale : fait reculer le joueur de X cases (il s'arrête au départ au plus).
public class EffetReculer : EffetCase
{
    [SerializeField] private int nombreCases = 2;

    public override void Declencher(JoueurPlateau joueur)
    {
        Debug.Log($"Case malus : recule de {nombreCases} cases");
        joueur.Reculer(nombreCases);
    }
}