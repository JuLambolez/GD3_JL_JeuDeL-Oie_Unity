using System.Collections.Generic;

/// Classe statique pure : aucun lien avec Unity, aucune position, seulement des index.
public static class RegleDeplacement
{
    /// Renvoie la liste des index de cases traversés, dans l'ordre, jusqu'à la case finale
    /// Si le joueur dépasse la dernière case, il rebrousse chemin pour les pas restants
    /// Exemple : à 1 case de l'arrivée avec un 5 → arrivée, puis 4 cases en arrière
    public static List<int> CalculerChemin(int caseActuelle, int pas, int indexDerniereCase)
    {
        var chemin = new List<int>();
        int position = caseActuelle;
        int direction = 1;

        for (int i = 0; i < pas; i++)
        {
            // Sur la dernière case avec des pas restants → on repart en arrière
            if (position == indexDerniereCase) direction = -1;

            // Sécurité (plateau très court) : au départ on ne peut pas reculer plus
            if (position == 0 && direction == -1) direction = 1;

            position += direction;
            chemin.Add(position);
        }

        return chemin;
    }

    /// Renvoie le chemin d'un recul de X cases. Le joueur s'arrête au départ au plus
    /// (le chemin peut donc être plus court que demandé, voire vide)
    public static List<int> CalculerCheminRecul(int caseActuelle, int pas)
    {
        var chemin = new List<int>();
        int position = caseActuelle;

        for (int i = 0; i < pas; i++)
        {
            if (position == 0) break;
            position--;
            chemin.Add(position);
        }

        return chemin;
    }
}