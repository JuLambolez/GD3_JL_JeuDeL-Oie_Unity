using UnityEngine;

/// Case spéciale : lance un mini-jeu.
/// Pour l'instant, simple message dans la Console.
public class EffetMiniJeu : EffetCase
{
    [SerializeField] private string nomMiniJeu = "Mini-jeu";

    public override void Declencher(JoueurPlateau joueur)
    {
        Debug.Log($"Mini-jeu lancé : {nomMiniJeu}");

        // TODO : lancer le vrai mini-jeu, puis appeler Terminer() à sa fin.
        // Pour l'instant on termine tout de suite.
        Terminer();
    }
}