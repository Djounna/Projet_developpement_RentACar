namespace FrontEnd_MVC
{
    public class CustomError : ApplicationException
    {
        private int _ErrorId;
        private string _ErrorMessage;
        public CustomError(int pErrorId)
        {
            string sMessage = "Defaut";

            switch (pErrorId)
            {
                case 1:  // Erreur encodage CoefficientMult;
                    ErrorId = 1;
                    ErrorMessage = " Le coefficient multiplicateur doit être un décimal, supérieur à 0 et inférieur à 5";
                    break;

                case 2:  // Erreur si ModelState invalide;
                    ErrorId = 2;
                    ErrorMessage = "(2) Un problème s'est produit avec l'envoi des données";
                    break;

                case 3:  // Erreur BadRequest;
                    ErrorId = 3;
                    ErrorMessage = "(3) Un problème s'est produit avec l'enregistrement des données";
                    break;

                case 4:  // Erreur Get;
                    ErrorId = 4;
                    ErrorMessage = "(4) Un problème s'est produit lors de la récupération des données dans la base de données";
                    break;

                case 5:  // Erreur Get id null;
                    ErrorId = 5;
                    ErrorMessage = "(5) Forçage id";
                    break;

                case 6:  // Erreur dupplicate;
                    ErrorId = 6;
                    ErrorMessage = "(6) Il semblerait que vous essayer de créer un doublon";
                    break;

                case 7:  // Erreur client inexistant;
                    ErrorId = 7;
                    ErrorMessage = "(7) Oups, adresse mail incorrecte";
                    break;

                case 8:  // Erreur Date de départ;
                    ErrorId = 8;
                    ErrorMessage = "(8) La date de départ votre réservation ne peut pas être dans le passé, nous vous prions de recommencer votre réservation";
                    break;

                case 9:  // Erreur Kilometrage retour;
                    ErrorId = 9;
                    ErrorMessage = "(9) Le kilométrage au retour doit être supérieur au kilométrage au départ";
                    break;

                case 10:  // Erreur Date de Retour;
                    ErrorId = 10;
                    ErrorMessage = "(10) La date de retour de votre réservation doit être supérieur à la date de départ, nous vous prions de recommencer";
                    break;

                case 11:  // Autres Erreurs au niveau des données;
                    ErrorId = 11;
                    ErrorMessage = "(11) les données mentionnées ne sont pas valides, nous vous prions de recommencer l'opération";
                    break;




                default:
                    ErrorId = 999;
                    ErrorMessage = sMessage;
                    break;
            }
        }

        public CustomError(string message)
        {
            ErrorMessage = "le problème suivant s'est produit : " + message;
        }

        public string ErrorMessage { get => _ErrorMessage; set => _ErrorMessage = value; }
        public int ErrorId { get => _ErrorId; set => _ErrorId = value; }
    }
}
