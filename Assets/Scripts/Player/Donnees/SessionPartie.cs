using UnityEngine;

/// Classe statique : elle n'est pas détruite quand on charge une autre scène (par exemple : plateau → mini-jeu → plateau).
/// Les données sont perdues à la fermeture du jeu.
public static class SessionPartie
{
    public static DonneesPartie Donnees { get; private set; } = new DonneesPartie();

    /// Efface tout. À appeler au début d'une nouvelle partie
    public static void Reinitialiser()
    {
        Donnees = new DonneesPartie();
    }

    // Repart de zéro à chaque lancement du jeu (utile si le "Domain Reload" est désactivé dans l'éditeur)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ReinitialiserAuLancement()
    {
        Reinitialiser();
    }
}