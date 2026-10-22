# GD3_JL_JeuDeL-Oie_Unity
Git du projet Jeu de L'Oie sur Unity

Le projet est de réaliser un jeu de type jeu de l'oie (sans boucle donc).
Le jeu se passe à la 3ème personne dans une prison, où une sorte de "circuit" (le plateau de jeu) a été crée pour rendre sa liberté à un détenu : celui qui arrivera à le terminer en vie. Cette "course" est retransmis en directe.
Evidemment, il n'y a pas qu'un seul détenu en lice mais bien 4 et tout les coups sont permis entre eux :

- acheter des outils au "marché noir" en de pièces récupérés lors de mini-jeux ou sur des cases (des objets utilitaires pour avancer de plus de cases, des pièges pour bloquer ou ralentir la progression, des outils pour attaquer un autre détenu...). Ce "marché noir" a des emplacements fixes un peu partout sur le "circuit" entre 2 cases, ce qui fait qu'on a pas besoin de s'arrêter pile dessus pour acheter quelque chose.
- S'affronter dans des mini-jeux pour gagner de l'argent (dont un cache-cache imposé) à la fin du tour de tout les prisonniers
- On peut avoir la chance de recevoir des "contrats" (des quêtes) des spectateurs ou d'autres détenus (pas ceux dans la course avec nous), ou de gardes.
Ces quêtes varient et rapporteront diverses récompenses (des objets, de l'argent...) :
- Lors du prochain mini-jeu, arriver à x position
- Lors du prochain mini-jeu, faire en sorte que x détenu finisse dernier
- Lors de votre tour, utiliser tel objet pour attaquer x détenu

Le "circuit" est composé de différentes cases ayant des effets différents :

- Les cases neutres (une fois arrivé dessus, il ne se passe rien)
- Les cases boost (une fois arrivé dessus, vous avancez de x cases)
- Les cases recule (une fois arrivé dessus, vous reculez de x cases)
- Les cases pièces (une fois arrivez dessus, vous gagnez x pièces)
- Les cases mini-jeux (elles lancent des mini-jeux)
