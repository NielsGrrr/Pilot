using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot.Classes
{
    /// <summary>
    /// Stocke les informations de connexion :
    /// 1 booléen : si la personne est admin ou non
    /// 1 chaine de caractères : celui qui a enregistré la commande
    /// 1 DataAccess
    /// 1 chaine de caractères : Les informations de connexion à la base de données
    /// 1 NpgsqlConnection : la connexion à la base de données
    /// </summary>
    public class DataAccess
    {
        private static bool admin;
        private static string username, password;
        private static DataAccess instance;
        private readonly string connectionString = "Host=srv-peda-new;Port=5433;Username=stiefvan;Password=dGAxKU;Database=pilot_41;Options='-c search_path=sae'";
        private NpgsqlConnection connection;

        /// <summary>
        /// Instance unique de la classe, utilisée pour centraliser l'accès aux données.
        /// Initialise l'accès administrateur ou utilisateur selon le paramètre <see cref="Admin"/>.
        /// </summary>
        public static DataAccess Instance
        {
            get
            {
                if (instance != null)
                    instance.CloseConnection();
                if (Admin)
                {
                    instance = new DataAccess();
                }
                else
                {
                    instance = new DataAccess(Username, Password);
                }
                return instance;
            }
        }

        /// <summary>
        /// Indique si l'accès est en mode administrateur.
        /// </summary>
        public static bool Admin
        {
            get
            {
                return admin;
            }

            set
            {
                admin = value;
            }
        }

        /// <summary>
        /// Nom d'utilisateur utilisé pour la connexion à la base de données (hors mode admin).
        /// </summary>
        public static string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        /// <summary>
        /// Mot de passe utilisé pour la connexion à la base de données (hors mode admin).
        /// </summary>
        public static string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        /// <summary>
        /// Constructeur privé pour le mode administrateur. Initialise la connexion avec les identifiants codés en dur.
        /// </summary>
        //  Constructeur privé pour empêcher l'instanciation multiple
        private DataAccess()
        {
            
            try
            {
                connection = new NpgsqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de connexion GetConnection \n" + connectionString);
                throw;
            }
        }

        /// <summary>
        /// Constructeur privé pour le mode utilisateur. Initialise la connexion avec identifiants personnalisés.
        /// </summary>
        private DataAccess(string username, string password)
        {
            connectionString = $"Host=srv-peda-new;Port=5433;Username={username};Password={password};Database=pilot_41;Options='-c search_path=sae'";
            try
            {
                connection = new NpgsqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de connexion GetConnection \n" + connectionString);
                throw;
            }
        }

        /// <summary>
        /// Retourne une connexion ouverte à la base de données PostgreSQL.
        /// </summary>
        /// <returns>Connexion PostgreSQL ouverte</returns>
        // pour récupérer la connexion (et l'ouvrir si nécessaire)
        public NpgsqlConnection GetConnection()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, "Pb de connexion GetConnection \n" + connectionString);
                    throw;
                }
            }


            return connection;
        }

        /// <summary>
        /// Exécute une commande SELECT et retourne les résultats.
        /// </summary>
        /// <returns>Données retournées par la requête</returns>
        //  pour requêtes SELECT et retourne un DataTable ( table de données en mémoire)
        public DataTable ExecuteSelect(NpgsqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection = GetConnection();
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Erreur SQL");
                throw;
            }
            return dataTable;
        }

        /// <summary>
        /// Exécute une commande INSERT et retourne l'identifiant généré.
        /// </summary>
        /// <returns>Identifiant inséré</returns>
        //   pour requêtes INSERT et renvoie l'ID généré
        public int ExecuteInsert(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb avec une requete insert " + cmd.CommandText);
                throw;
            }
            return nb;
        }



        /// <summary>
        /// Exécute une commande SQL de type UPDATE ou DELETE et retourne le nombre de lignes affectées.
        /// </summary>
        /// <returns>Nombre de lignes modifiées</returns>
        //  pour requêtes UPDATE, DELETE
        public int ExecuteSet(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb avec une requete set " + cmd.CommandText);
                throw;
            }
            return nb;

        }

        /// <summary>
        /// Exécute une commande SQL et retourne une seule valeur (par exemple un COUNT ou un SUM).
        /// </summary>
        /// <returns>Résultat de la requête (objet unique)</returns>
        // pour requêtes avec une seule valeur retour  (ex : COUNT, SUM) 
        public object ExecuteSelectUneValeur(NpgsqlCommand cmd)
        {
            object res = null;
            try
            {
                cmd.Connection = GetConnection();
                res = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb avec une requete select " + cmd.CommandText);
                throw;
            }
            return res;

        }

        /// <summary>
        /// Ferme proprement la connexion à la base de données si elle est ouverte.
        /// </summary>
        //  Fermer la connexion 
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
