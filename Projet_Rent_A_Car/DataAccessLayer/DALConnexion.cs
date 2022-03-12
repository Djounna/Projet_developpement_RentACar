namespace DataAccessLayer
{
    public class DALConnexion
    {
        private static string sConnection = @"Server=LAPTOP-R3GDQJIT; Database=ProjetSGDB; Trusted_Connection=True";
        public static string Connexion { get { return sConnection; } }
    }
}