# Projet_developpement_RentACar
Projet de Développement pour le cours Ephec-SGBD

Fait
- Contraintes de structure DB  : OK
- Schéma Relationnel : OK (Schéma EA à faire qd tout fini)
- Architecture du projet OK (à fignoler ok)
- structure du code : OK

Planning
03/03/2021
Fonctionnement Code : Post/Get/Delete/Update : exemple
Contraintes fonctionnelles/fonctionnelles/Use Case.
Fonctionnement FluentValidation pour l'API
Fonctionnement DataAnnotation pour MVC

Corentin
Reprendre le code de Antoine et simplifier/changer (voir Linq) + Renommer selon la Nomenclature



Planning futur
Règles business dans la BL
Gestion d'erreurs
Implémenter les use cases j(Check List)



Problème to resolve ou to improve:

Cacher l'ID lors d'un PUT ou empêcher la modification
Le type Decimal ne prend pas la , ou le . comme séparateur


Nomenclature des méthodes :

MVC : Get = Show/
      Getbyid =  ShowById/
      Post =    Create/
      Put =    Update/
      Delete =    Erase
            ex: CreateVille

API : Get/GetById/Post/Put/Delete

BL : Get/GetById/Post/Put/Delete
DAL : Get/...
