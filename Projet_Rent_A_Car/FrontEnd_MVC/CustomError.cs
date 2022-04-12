namespace FrontEnd_MVC
{
    public class CustomError: ApplicationException
    {
        private int _ErrorId;
        private string _ErrorMessage;
        public CustomError(int pErrorId)
        {
            string sMessage = "Defaut";

            switch (pErrorId)
            {
                case 1:  // Erreur encodage CoefficientMult;
                    _ErrorId = 1;
                    _ErrorMessage = "le prix doit être un nombre et plus grand que 0";
                    break;






                default:
                    _ErrorId = 999;
                    _ErrorMessage = sMessage;
                    break;
            }
        }

    }
}
