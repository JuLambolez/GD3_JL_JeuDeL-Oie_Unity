using System;

/// Classe de données pure. Pour ajouter des récompenses de mini-jeux plus tard,
/// il suffira d'ajouter ici de nouveaux champs (pièces, objets...).
[Serializable]
public class DonneesJoueur
{
    public string id;
    public int caseActuelle;

    public DonneesJoueur(string id)
    {
        this.id = id;
    }
}