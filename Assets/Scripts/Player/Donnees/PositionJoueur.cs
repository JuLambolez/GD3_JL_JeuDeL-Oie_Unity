using UnityEngine;


[DefaultExecutionOrder(100)] // son Start s'exécute après celui de JoueurPlateau (qui place le joueur au départ)
public class PersistanceJoueur : MonoBehaviour
{
    [SerializeField] private JoueurPlateau joueur;
    [SerializeField] private string idJoueur = "Joueur1";

    private void OnEnable()
    {
        joueur.DeplacementTermine += Sauvegarder;
    }

    private void OnDisable()
    {
        joueur.DeplacementTermine -= Sauvegarder;
    }

    private void Start()
    {
        // Si une position a été retenue (ex : retour d'un mini-jeu), on y replace le joueur
        if (SessionPartie.Donnees.TryGetCase(idJoueur, out int caseSauvegardee))
        {
            joueur.PlacerSurCase(caseSauvegardee);
        }
    }

    private void Sauvegarder()
    {
        SessionPartie.Donnees.SetCase(idJoueur, joueur.CaseActuelle);
    }
}