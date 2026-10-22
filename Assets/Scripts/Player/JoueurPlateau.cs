using System;
using System.Collections.Generic;
using UnityEngine;

public class JoueurPlateau : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Plateau plateau;
    [SerializeField] private PionDeplacement pion;

    public int CaseActuelle { get; private set; }
    public bool AGagne { get; private set; }

    /// Déclenché à chaque case franchie, avec le nombre de pas qu'il reste à faire
    public event Action<int> PasEffectue;

    /// Déclenché quand un déplacement démarre, avec son nombre de pas réel
    public event Action<int> DeplacementDemarre;

    /// Déclenché quand le pion s'est arrêté (après chaque déplacement)
    public event Action DeplacementTermine;

    /// Déclenché quand le joueur arrive pile sur la dernière case
    public event Action Victoire;

    private int caseFinale;
    private int nombrePas;

    private void OnEnable()
    {
        pion.PasTermine += OnPionPas;
        pion.DeplacementTermine += OnPionArrive;
    }

    private void OnDisable()
    {
        pion.PasTermine -= OnPionPas;
        pion.DeplacementTermine -= OnPionArrive;
    }

    private void Start()
    {
        PlacerSurCase(0);
    }

    /// Téléporte le joueur sur une case, sans animation ni événement
    public void PlacerSurCase(int index)
    {
        CaseActuelle = Mathf.Clamp(index, 0, plateau.IndexDerniereCase);
        AGagne = CaseActuelle == plateau.IndexDerniereCase;
        pion.Placer(plateau.GetPosition(CaseActuelle));
    }

    /// Avance de X cases (rebrousse chemin si on dépasse l'arrivée)
    public void Avancer(int pas)
    {
        if (AGagne || pion.EnDeplacement) return;
        Suivre(RegleDeplacement.CalculerChemin(CaseActuelle, pas, plateau.IndexDerniereCase));
    }

    /// Recule de X cases (s'arrête au départ au plus)
    public void Reculer(int pas)
    {
        if (AGagne || pion.EnDeplacement) return;
        Suivre(RegleDeplacement.CalculerCheminRecul(CaseActuelle, pas));
    }

    private void Suivre(List<int> chemin)
    {
        if (chemin.Count == 0)
        {
            caseFinale = CaseActuelle;
            OnPionArrive();
            return;
        }

        var positions = new List<Vector3>();
        foreach (int index in chemin) positions.Add(plateau.GetPosition(index));

        nombrePas = chemin.Count;
        caseFinale = chemin[chemin.Count - 1];

        DeplacementDemarre?.Invoke(nombrePas);
        pion.Suivre(positions);
    }

    private void OnPionPas(int numeroPas)
    {
        PasEffectue?.Invoke(nombrePas - numeroPas);
    }

    private void OnPionArrive()
    {
        CaseActuelle = caseFinale;
        AGagne = CaseActuelle == plateau.IndexDerniereCase;

        DeplacementTermine?.Invoke();
        if (AGagne) Victoire?.Invoke();
    }
}