using Models;

namespace DataAccessLayer
{
    public class GenerateData
    {
        DalCommun dal = new DalCommun();
        string[] tabPays = new string[] { "Vatican", "Ukraine", "Suisse", "Suède", "Slovénie ", "Slovaquie", "Saint-Marin", "Serbie", "Russie", "Royaume-Uni", "Roumanie", "République ", "Portugal", "Pologne", "Pays-Bas", "Norvège", "Monténégro", "Monaco", "Moldavie", "Malte", "Macédoine", "Luxembourg", "Lituanie", "Liechtenstein", "Lettonie ", "Kasakhstan", "Italie", "Islande", "Irlande", "Hongrie", "Grèce", "Géorgie", "France", "Finlande", "Estonie", "Espagne", "Danemark", "Croatie ", "Chypre ", "Bulgarie ", "Bosnie–Herzégovine ", "Biélorussie ", "Belgique ", "Arménie", "Azerbaïdjan", "Autriche", "Andorre ", "Allemagne", "Albanie" };
        string[] tabVilleBE = new string[] { "Louvain", "Namur", "Bruges", "Anderlecht", "Schaerbeek", "Ville de Bruxelles", "Liège", "Charleroi", "Gand" };
        string[] tabVilleFR = new string[] { "Lille", "Bordeaux", "Strasbourg", "Montpellier", "Nantes", "Nice", "Toulouse", "Lyon", "Marseille", "Paris" };
        string[] tabVilleIT = new string[] { "Catane", "Bari", "Florence", "Bologne", "Gênes", "Palerme", "Turin", "Naples", "Milan", "Rome" };
        string[] tabVilleAL = new string[] { "Leipzig", "Essen", "Dortmund", "Düsseldorf", "Stuttgart", "Francfort-sur-le-Main", "Cologne", "Munich", "Hambourg", "Berlin" };
        string[] tabVilleES = new string[] { "Palma de Majorque", "Murcie", "Las Palmas de Gran Canaria", "Saragosse", "Malaga", "Bilbao", "Valence", "Séville", "Barcelone", "Madrid" };
        string[] tabNot = new string[] { "Luxe", "Poubelle", "SUV", "Familiale", "Citadine", "Campagnarde", "Roule-a-peine", "Sport", "Tank", "Fullspeed" };
        List<string> listdep = new List<string>();
        string[] tabMarque = new string[] { "Peugeot", "Audi", "BMW", "Volvo", "Ford", "Volkswagen", "Renault", "MINI", "Skoda", "Fiat", "Tesla" };

        public void generatefirstpart()
        {



            listdep.AddRange(tabVilleBE);
            listdep.AddRange(tabVilleFR);
            listdep.AddRange(tabVilleIT);
            listdep.AddRange(tabVilleAL);
            listdep.AddRange(tabVilleES);

            #region Pays
            foreach (string pays in tabPays)
            {
                Pays p = new Pays();
                p.Nom = pays;
                dal.InsertOrUpdate(p);

            }
            #endregion

            #region Ville
            // Data pour ville

            foreach (string ville in tabVilleBE)
            {
                Ville v = new Ville();
                v.Idpays = dal.dbcontext.Pays.Where(r => r.Nom == "Belgique").Select(s => s.Idpays).FirstOrDefault();
                v.Nom = ville;
                dal.InsertOrUpdate(v);
            }


            foreach (string ville in tabVilleFR)
            {
                Ville v = new Ville();
                v.Idpays = dal.dbcontext.Pays.Where(r => r.Nom == "France").Select(s => s.Idpays).FirstOrDefault();
                v.Nom = ville;
                dal.InsertOrUpdate(v);
            }


            foreach (string ville in tabVilleIT)
            {
                Ville v = new Ville();
                v.Idpays = dal.dbcontext.Pays.Where(r => r.Nom == "Italie").Select(s => s.Idpays).FirstOrDefault();
                v.Nom = ville;
                dal.InsertOrUpdate(v);
            }


            foreach (string ville in tabVilleAL)
            {
                Ville v = new Ville();
                v.Idpays = dal.dbcontext.Pays.Where(r => r.Nom == "Allemagne").Select(s => s.Idpays).FirstOrDefault();
                v.Nom = ville;
                dal.InsertOrUpdate(v);
            }


            foreach (string ville in tabVilleES)
            {
                Ville v = new Ville();
                v.Idpays = dal.dbcontext.Pays.Where(r => r.Nom == "Espagne").Select(s => s.Idpays).FirstOrDefault();
                v.Nom = ville;
                dal.InsertOrUpdate(v);
            }

            #endregion

            #region Notoriété

            double nb1 = 0;
            int nb2 = 0;
            List<double> lstcoef = new();
            for (int j = 0; j < 60; j++)
            {
                Random rnd = new Random();
                nb1 = rnd.NextDouble();
                nb2 = rnd.Next(1, 3);
                lstcoef.Add(Math.Round(nb2 + nb1, 2));
            }

            foreach (string not in tabNot)
            {
                Random rnd = new Random();
                Notoriete n = new();
                n.Libelle = not;
                nb2 = rnd.Next(0, 60);
                n.CoefficientMultiplicateur = (decimal)lstcoef[nb2];
                dal.InsertOrUpdate(n);
            }
            #endregion

            #region Prix
            List<double> lstPrixkm = new();
            for (int j = 0; j < 60; j++)
            {
                Random rnd = new Random();
                nb1 = rnd.NextDouble();
                nb2 = rnd.Next(1, 5);
                lstPrixkm.Add(Math.Round(nb2 + nb1, 2));
            }

            foreach (string pays in tabPays)
            {
                Random rnd = new Random();
                Prix prix = new Prix();
                prix.Idpays = dal.dbcontext.Pays.Where(r => r.Nom == pays).Select(s => s.Idpays).FirstOrDefault();
                prix.DateDebut = DateTime.Now;
                nb2 = rnd.Next(0, 60);
                prix.PrixKm = (decimal)lstPrixkm[nb2];
                dal.InsertOrUpdate(prix);
            }
            #endregion

            #region Depot
            foreach (string dep in listdep)
            {
                Depot d = new();
                d.Idville = dal.dbcontext.Ville.Where(r => r.Nom == dep).Select(s => s.Idville).FirstOrDefault();
                dal.InsertOrUpdate(d);
            }

            #endregion

            #region Voiture

            for (int j = 0; j < 200; j++)
            {
                int id = 0;
                int nb3 = 0;
                int nb4 = 0;
                int nb5 = 0;
                int nb6 = 0;
                int nb7 = 0;
                Random rnd = new Random();
                Voiture voiture = new Voiture();
                nb2 = rnd.Next(0, 10);
                voiture.Marque = tabMarque[nb2];
                nb2 = rnd.Next(0, 49);

                id = dal.dbcontext.Ville.Where(r => r.Nom == listdep[nb2]).Select(s => s.Idville).FirstOrDefault();
                voiture.Iddepot = dal.dbcontext.Depot.Where(r => r.Idville == id).Select(s => s.Iddepot).FirstOrDefault();
                nb2 = rnd.Next(0, 10);
                voiture.Idnotoriete = dal.dbcontext.Notoriete.Where(r => r.Libelle == tabNot[nb2]).Select(s => s.Idnotoriete).FirstOrDefault();
                nb3 = rnd.Next(65, 91);
                nb4 = rnd.Next(65, 91);
                nb5 = rnd.Next(65, 91);
                nb6 = rnd.Next(1, 3);
                nb7 = rnd.Next(100, 1000);

                char a = (char)nb3;
                char b = (char)nb4;
                char c = (char)nb5;
                voiture.Immatriculation = nb6 + "-" + a + b + c + "-" + nb7;
                dal.InsertOrUpdate(voiture);

            }
            #endregion


        }

        public void generateSecondpart()
        {
            #region Client

            string[] lstendmail = new string[] { "@gmail.com", "@yahoo.fr", "@outlook.com", "@hotmail.com", "@students.ephec.be" };
            string[] lstprenom = new string[] { "Nora", "Soline", "Albane", "Céleste", "Emmy", "Salomé", "Noémie", "Lisa", "Elsa", "Amelia", "Ella", "Marie", "Célia", "Maëlle", "Elise", "Constance", "Maria", "Lilou", "Alma", "Joy", "Ava", "Héloïse", "Assia", "Maëlys", "Lila", "Thaïs", "Apolline", "Anaïs", "Valentine", "Livia", "Mathilde", "Maya", "Léana", "Lily", "Lise", "Zelie", "Roxane", "Alicia", "Alya", "Gabrielle", "Yasmine", "Lyna", "Aya", "Lana", "Clara", "Emy", "Alba", "Elena", "Théa", "Clémence", " Marin", "Youssef", "Wassim", "Adem", "Ayoub", "Auguste", "Timothée", "William", "Alessio", "Mathys", "Logan", "Anas", "Ali", "Ilyes", "Milan", "Basile", "Charly", "Charlie", "Livio", "Noam", "Diego", "Malone", "Noa", "Elio", "Oscar", "Joseph", "Thomas", "Ilyan", "Naïm", "Léandre", "Pablo", "Lenny", "Ismaël", "Owen", "Alexandre", "Camille", "Maxime", "Evan", "Soan", "Côme", "Kaïs", "Imran", "Maxence", "Baptiste", "Simon", "Mathéo", "Clément", "Sohan", "Gaspard", "Ibrahim", "Capucine", "Lyana", "Nour", "Margot", "Lya", "Giulia", "Sarah", "Charlotte", "Sofia", "Mya", "Louna", "Alix", "Victoria", "Olivia", "Margaux", "Lucie", "Romane", "Camille", "Luna", "Manon", "Victoire", "Adèle", "Lola", "Charlie", "Eva", "Iris", "Jeanne", "Léonie", "Zoé", "Nina", "Inaya", "Juliette", "Agathe", "Léna", "Inès", "Lou", "Romy", "Julia", "Mila", "Anna", "Léa", "ia", "hloé", "ose", "ina", "mbre", "lice", "mma", "ouise", "ade", "Yanis", "Rayan", "Lyam", "Amir", "Augustin", "Samuel", "Antoine", "Milo", "Malo", "Nolan", "Eliott", "Nino", "Valentin", "Marceau", "Enzo", "Timéo", "Robin", "Axel", "Mathis", "Naël", "Martin", "Ayden", "Victor", "Marius", "Isaac", "Théo", "Tiago", "Noé", "Léon", "Eden", "Tom", "Ethan", "Mohammed", "Aaron", "Nathan", "Paul", "Gabriel", "Sacha", "Gabin", "Liam", "Noah", "Hugo", "Lucas", "Maël", "Adam", "Louis", "Arthur", "Raphaël", "Gabriel", "Léo" };
            string[] lstnom = new string[] { "LECOMTE", "GUILLAUME", "LECLERCQ", "ROLLAND", "PAYET", "LECLERC", "REY", "BENOIT", "PIERRE", "BOURGEOIS", "PHILIPPE", "OLIVIER", "LACROIX", "DUMAS", "RENAUD", "LEMOINE", "AUBERT", "FABRE", "ROGER", "PICARD", "CARON", "VIDAL", "COLIN", "LEROUX", "ROY", "SCHMITT", "RENARD", "ROCHE", "GERARD", "MARTINEZ", "ARNAUD", "BARBIER", "GAILLARD", "BRUNET", "LUCAS", "RIVIERE", "JOLY", "GIRAUD", "BLANCHARD", "BRUN", "MEUNIER", "DUFOUR", "MEYER", "NOEL", "LEMAIRE", "MARIE", "DUMONT", "DENIS", "DUVAL", "MARCHAND", "MASSON", "GAUTIER", "MATHIEU", "ROUSSEL", "HENRY", "NICOLAS", "MORIN", "CLEMENT", "ROBIN", "PERRIN", "GARCIA", "GAUTHIER", "LEGRAND", "FRANCOIS", "CHEVALIER", "GARNIER", "BOYER", "GUERIN", "BLANC", "MERCIER", "ANDRE", "FAURE", "LEFEVRE", "MULLER", "VINCENT", "ROUSSEAU", "FONTAINE", "LAMBERT", "DUPONT", "BONNET", "GIRARD", "FOURNIER", "MOREL", "BERTRAND", "DAVID", "ROUX", "LEROY", "LEFEBVRE", "MICHEL", "SIMON", "LAURENT", "MOREAU", "DUBOIS", "DURAND", "RICHARD", "ROBERT", "PETIT", "THOMAS", "BERNARD", "MARTIN" };


            for (int i = 0; i < 100; i++)
            {
                int nb1 = 0;
                int nb2 = 0;
                int nb3 = 0;
                Client c = new();

                Random rnd = new Random();
                nb1 = rnd.Next(0, 5);
                nb2 = rnd.Next(0, 200);
                nb3 = rnd.Next(0, 100);

                c.Prenom = lstprenom[nb2];
                c.Nom = lstnom[nb3];
                c.Mail = c.Prenom + "." + c.Nom + lstendmail[nb1];

                dal.InsertOrUpdate(c);


            }


            #endregion

            #region forfait


            List<int> listIdDep = dal.dbcontext.Depot.Select(s => s.Iddepot).ToList();
            for (int i = 0; i < 50; i++)
            {
                int nb1 = 0;
                int nb2 = 0;
                double prix = 0;
                int annee = 0;
                int mois = 0;
                int jour = 0;
                Forfait f = new();

                Random rnd = new Random();
                nb1 = rnd.Next(0, listIdDep.Count() - 1);
                nb2 = rnd.Next(0, listIdDep.Count() - 1);
                prix = rnd.Next(100, 1000);
                f.Iddepot1 = listIdDep[nb1];
                f.Iddepot2 = listIdDep[nb2];
                annee = rnd.Next(2010, 2023);
                mois = rnd.Next(1, 13);
                jour = rnd.Next(1, 30);

                if (mois == 2 && jour > 28)
                    jour = 27;

                string date = jour.ToString() + "/" + mois.ToString() + "/" + annee.ToString();
                f.DateDebut = Convert.ToDateTime(date);
                f.Prix = (decimal)Math.Round(prix);

                dal.InsertOrUpdate(f);


            }


            #endregion
        }

        public void generatethirdpart()
        {
            #region reservation


            List<int> listIdClient = dal.dbcontext.Client.Select(s => s.Idclient).ToList();
            List<int> listIdvoit = dal.dbcontext.Voiture.Select(s => s.Idvoiture).ToList();
            List<int> listIdDepdeb = dal.dbcontext.Depot.Select(s => s.Iddepot).ToList();
            List<int> listIDForfait = dal.dbcontext.Forfait.Select(s => s.Idforfait).ToList();


            for (int i = 0; i < 100; i++)
            {
                Reservation reservation = new Reservation();
                int forfait = 0;
                int idforfait = 0;
                Random rnd = new Random();
                forfait = rnd.Next(0, 2);
                reservation.Idclient = listIdClient[rnd.Next(0, listIdClient.Count())];
                reservation.Idvoiture = listIdvoit[rnd.Next(0, listIdvoit.Count())];
                if (forfait == 1)
                {
                    idforfait = listIDForfait[rnd.Next(0, listIDForfait.Count())];
                    reservation.Idforfait = idforfait;
                    int iddepot1 = dal.dbcontext.Forfait.Where(r => r.Idforfait == idforfait).Select(s => s.Iddepot1).SingleOrDefault();
                    int iddepot2 = dal.dbcontext.Forfait.Where(r => r.Idforfait == idforfait).Select(s => s.Iddepot2).SingleOrDefault();
                    reservation.IddepotDepart = iddepot1;
                    reservation.IddepotRetour = iddepot2;

                }
                else
                {

                    reservation.IddepotDepart = listIdDepdeb[rnd.Next(0, listIdDepdeb.Count())];
                    if (reservation.IddepotDepart % 5 != 0)
                        reservation.IddepotRetour = listIdDepdeb[rnd.Next(0, listIdDepdeb.Count())];
                    else
                        reservation.IddepotRetour = null;
                }
                int annee = rnd.Next(2010, 2023);
                int mois = rnd.Next(1, 13);
                int jour = rnd.Next(1, 30);

                if (mois == 2 && jour > 28)
                    jour = 27;

                string date = jour.ToString() + "/" + mois.ToString() + "/" + annee.ToString();
                reservation.DateDepart = Convert.ToDateTime(date);
                if (mois - 1 != 0 && mois - 1 != 2)
                {
                    string date2 = jour.ToString() + "/" + (mois - 1).ToString() + "/" + annee.ToString();
                    reservation.DateReservation = Convert.ToDateTime(date2);
                }
                else if (jour - 1 != 0)
                {
                    string date2 = (jour - 1).ToString() + "/" + (mois).ToString() + "/" + annee.ToString();
                    reservation.DateReservation = Convert.ToDateTime(date2);
                }
                else
                {
                    string date2 = (jour).ToString() + "/" + (mois).ToString() + "/" + (annee - 1).ToString();
                    reservation.DateReservation = Convert.ToDateTime(date2);
                }

                if (mois + 1 != 13 && mois + 1 != 2)
                {
                    string date3 = (jour).ToString() + "/" + (mois + 1).ToString() + "/" + (annee).ToString();
                    reservation.DateRetour = Convert.ToDateTime(date3);
                }
                else if (jour + 1 != 32 && mois != 2)
                {
                    string date3 = (jour + 1).ToString() + "/" + (mois).ToString() + "/" + (annee).ToString();
                    reservation.DateRetour = Convert.ToDateTime(date3);
                }
                else
                {
                    string date3 = (jour).ToString() + "/" + (mois).ToString() + "/" + (annee + 1).ToString();
                    reservation.DateRetour = Convert.ToDateTime(date3);
                }
                int kmdep = 0;
                int kmret = 0;

                kmdep = rnd.Next(2534, 65001);
                reservation.KilometrageDepart = kmdep;
                if (kmdep % 5 != 0)
                {
                    kmret = rnd.Next(5, 2000) + kmdep;
                    reservation.KilometrageRetour = kmret;
                }

                int idNot = dal.dbcontext.Voiture.Where(r => r.Idvoiture == reservation.Idvoiture).Select(s => s.Idnotoriete).SingleOrDefault();
                decimal coef = dal.dbcontext.Notoriete.Where(r => r.Idnotoriete == idNot).Select(s => s.CoefficientMultiplicateur).SingleOrDefault();
                reservation.CoefficientMultiplicateur = coef;

                if (reservation.KilometrageRetour % 10 == 0 && reservation.Idforfait != null)
                {
                    reservation.Penalite = true;
                }

                dal.InsertOrUpdate(reservation);
            }
            #endregion
        }
    }
}
