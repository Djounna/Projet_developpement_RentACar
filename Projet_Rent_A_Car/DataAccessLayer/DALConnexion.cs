namespace DataAccessLayer
{
    public class DALConnexion
    {
        private static string sConnection = @"Server=DESKTOP-FK2QPRH\SQLDB2022; Database=ProjetSGDB; User Id=sa; Password=SQLDB2022";
        public static string Connexion { get { return sConnection; } }
    }
}