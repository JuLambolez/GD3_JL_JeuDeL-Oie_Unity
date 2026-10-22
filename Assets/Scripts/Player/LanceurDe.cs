using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanceurDe : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private Key toucheLancer = Key.Space;

    [Header("Références")]
    [SerializeField] private DeAnimation deAnimation;
    [SerializeField] private DeResultat deResultat;
    [SerializeField] private AffichageResultat affichageResultat;

    /// Déclenché quand le dé a fini son animation et que le nombre est affiché
    public event Action<int> ResultatObtenu;

    private bool enMaintien;
    private bool bloque;

    private void OnEnable()
    {
        deAnimation.AnimationTerminee += OnAnimationTerminee;
    }

    private void OnDisable()
    {
        deAnimation.AnimationTerminee -= OnAnimationTerminee;
    }

    private void Update()
    {
        if (bloque) return;

        var touche = Keyboard.current[toucheLancer];

        if (touche.wasPressedThisFrame)
        {
            enMaintien = true;
            affichageResultat.Cacher();
            deAnimation.DemarrerRotation();
        }

        if (enMaintien && touche.wasReleasedThisFrame)
        {
            enMaintien = false;
            bloque = true;
            deAnimation.ArreterRotation();
        }
    }

    /// Autorise un nouveau lancer (appelé à la fin du tour)
    public void Debloquer()
    {
        bloque = false;
    }

    private void OnAnimationTerminee(Vector3 positionDe)
    {
        int resultat = deResultat.Lancer();
        affichageResultat.Afficher(resultat, positionDe);

        // On reste bloqué : c'est le gestionnaire de tour qui rendra la main
        ResultatObtenu?.Invoke(resultat);
    }
}