namespace DataAccessLayer
{
    public class DALConnexion
    {
        private static string sConnection = @"Server=DESKTOP-FK2QPRH\SQLDB2022; Database=ProjetSGDB; Trusted_Connection=True";
        public static string Connexion { get { return sConnection; } }
    }
}