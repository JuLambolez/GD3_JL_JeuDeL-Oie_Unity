using System;
using UnityEngine;

public abstract class EffetCase : MonoBehaviour
{
    /// Déclenché quand l'effet est fini (effets qui ne déplacent pas le joueur)
    public event Action Termine;

    public abstract void Declencher(JoueurPlateau joueur);

    protected void Terminer()
    {
        Termine?.Invoke();
    }
}