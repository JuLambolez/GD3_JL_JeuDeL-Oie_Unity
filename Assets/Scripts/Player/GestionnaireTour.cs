using UnityEngine;

public class GestionnaireTour : MonoBehaviour
{
    private enum Phase { Attente, DeplacementDe, Effet }

    [Header("Références")]
    [SerializeField] private LanceurDe lanceur;
    [SerializeField] private JoueurPlateau joueur;
    [SerializeField] private AffichageResultat affichage;
    [SerializeField] private Plateau plateau;

    [Header("Effets de case")]
    [Tooltip("Si activé, une case spéciale atteinte grâce à un effet déclenche aussi son effet.")]
    [SerializeField] private bool enchainerEffets = false;
    [SerializeField] private int maxEnchainements = 5; // sécurité contre les boucles infinies

    private Phase phase = Phase.Attente;
    private EffetCase effetEnCours;
    private int enchainements;

    private void OnEnable()
    {
        lanceur.ResultatObtenu += OnResultatDe;
        joueur.DeplacementDemarre += OnDeplacementDemarre;
        joueur.PasEffectue += OnPasEffectue;
        joueur.DeplacementTermine += OnDeplacementTermine;
        joueur.Victoire += OnVictoire;
    }

    private void OnDisable()
    {
        lanceur.ResultatObtenu -= OnResultatDe;
        joueur.DeplacementDemarre -= OnDeplacementDemarre;
        joueur.PasEffectue -= OnPasEffectue;
        joueur.DeplacementTermine -= OnDeplacementTermine;
        joueur.Victoire -= OnVictoire;
        DesabonnerEffet();
    }

    private void OnResultatDe(int resultat)
    {
        phase = Phase.DeplacementDe;
        enchainements = 0;
        joueur.Avancer(resultat);
    }

    private void OnDeplacementDemarre(int nombrePas)
    {
        // Pour un déplacement dû à un effet, on réaffiche le chiffre.
        // (Pour le dé, il est déjà affiché par le lanceur.)
        if (phase == Phase.Effet) affichage.Reafficher(nombrePas);
    }

    private void OnPasEffectue(int pasRestants)
    {
        // Le chiffre descend d'un cran à chaque case franchie
        affichage.MettreAJour(pasRestants);
    }

    private void OnDeplacementTermine()
    {
        affichage.Cacher();
        if (joueur.AGagne) return;

        if (phase == Phase.DeplacementDe)
        {
            // Le joueur vient de finir son déplacement de dé : y a-t-il un effet ?
            DeclencherEffetOuFinir();
        }
        else if (phase == Phase.Effet)
        {
            // Un effet de déplacement (avancer / reculer) vient de se terminer
            DesabonnerEffet();

            if (enchainerEffets && enchainements < maxEnchainements) DeclencherEffetOuFinir();
            else FinTour();
        }
    }

    private void DeclencherEffetOuFinir()
    {
        EffetCase effet = plateau.GetEffet(joueur.CaseActuelle);
        if (effet == null)
        {
            FinTour();
            return;
        }

        enchainements++;
        phase = Phase.Effet;
        effetEnCours = effet;
        effetEnCours.Termine += OnEffetTermine; // abonné AVANT de déclencher
        effetEnCours.Declencher(joueur);
    }

    private void OnEffetTermine()
    {
        // Effets qui ne déplacent pas le joueur (mini-jeu...)
        if (phase != Phase.Effet) return;
        DesabonnerEffet();
        FinTour();
    }

    private void DesabonnerEffet()
    {
        if (effetEnCours == null) return;
        effetEnCours.Termine -= OnEffetTermine;
        effetEnCours = null;
    }

    private void FinTour()
    {
        phase = Phase.Attente;
        if (!joueur.AGagne) lanceur.Debloquer();
    }

    private void OnVictoire()
    {
        Debug.Log("Victoire !");
    }
}