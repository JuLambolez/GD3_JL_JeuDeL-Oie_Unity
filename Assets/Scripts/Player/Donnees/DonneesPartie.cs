using System;
using System.Collections.Generic;

/// Classe de données pure (sans Unity), sérialisable : elle pourra être sauvegardée sur disque plus tard.
[Serializable]
public class DonneesPartie
{
    public List<DonneesJoueur> joueurs = new List<DonneesJoueur>();

    /// Retient la case d'un joueur (le crée s'il n'existe pas encore)
    public void SetCase(string idJoueur, int caseActuelle)
    {
        DonneesJoueur joueur = Trouver(idJoueur);
        if (joueur == null)
        {
            joueur = new DonneesJoueur(idJoueur);
            joueurs.Add(joueur);
        }
        joueur.caseActuelle = caseActuelle;
    }

    /// Renvoie false si ce joueur n'a encore aucune donnée enregistrée
    public bool TryGetCase(string idJoueur, out int caseActuelle)
    {
        DonneesJoueur joueur = Trouver(idJoueur);
        caseActuelle = joueur != null ? joueur.caseActuelle : 0;
        return joueur != null;
    }

    private DonneesJoueur Trouver(string idJoueur)
    {
        foreach (DonneesJoueur joueur in joueurs)
        {
            if (joueur.id == idJoueur) return joueur;
        }
        return null;
    }
}