using FrontEnd.DAL;
using FrontEnd.Models;

namespace FrontEnd.BL
{
    public class BLPays
    {
        public static List<Pays> SelectAll()
        {
            List<Pays> list = new List<Pays>();
            DALPays oPays = new DALPays();
            list = oPays.SelectAll();

            return list;
        }
    }
}
