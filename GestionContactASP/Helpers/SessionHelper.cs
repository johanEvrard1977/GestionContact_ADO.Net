using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestionContactASP.Helpers
{
    public static class SessionHelper
    {
        /// <summary>
        /// Récupérer le user en session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static T Get<T>(ISession session)
        {
            // déserialiser string -> User
            string chaine = session.GetString(typeof(T).Name);
            // return User
            if (chaine is null) return default;
            return JsonSerializer.Deserialize<T>(chaine);
        }

        /// <summary>
        /// Enregistrer l'utilisateur en session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="user"></param>
        public static void Set<T>(ISession session, T user)
        {
            // sérialiser le user -> string
            string serialized = JsonSerializer.Serialize(user);
            session.SetString(typeof(T).Name, serialized);
        }
    }
}