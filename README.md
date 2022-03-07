# Projet_developpement_RentACar
Projet de Développement pour le cours Ephec-SGBD

Fait
- Contraintes de structure DB  : OK
- Schéma Relationnel : OK (Schéma EA à faire qd tout fini)
- Architecture du projet OK (à fignoler ok)
- structure du code : OK

Planning
10/03
Corentin
Relire le code + améliorer la partie Edit de ville ( afficher le Pays en read only + réussir à passer les parametre à la fonction uptade)
Mettre au max les fonctions de la DAL en commun.
Vérifier si on peut mettre plusieurs modéle.
Avancer sur les Use Case

Antoine
Reprendre le code de Antoine et simplifier/changer (voir Linq) + Renommer selon la Nomenclature

Planning futur
Contraintes fonctionnelles/fonctionnelles/Use Case.
Fonctionnement FluentValidation pour l'API
Fonctionnement DataAnnotation pour MVC
Règles business dans la BL
Gestion d'erreurs
Implémenter les use cases j(Check List)



Problème to resolve ou to improve:

Cacher l'ID lors d'un PUT ou empêcher la modification (ok : readonly tag)
Le type Decimal ne prend pas la , ou le . comme séparateur


Nomenclature des méthodes :

MVC : Get = Show/
      Getbyid =  ShowById/
      Post =    Create/
      Put =    Update/
      Delete =    Erase
      ex: CreateVille a definir plus tard

API/BL/DAL : SelectAll.../SelectByID/Insert.../Uptade.../Delete... => Je Propose Insert à la place de Create car ça évite de confondre avec les view "Create" du MVC et ça colle au vocabulaire SQL.
