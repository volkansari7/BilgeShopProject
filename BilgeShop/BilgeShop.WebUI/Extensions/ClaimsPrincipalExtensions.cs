using System.Security.Claims;

namespace BilgeShop.WebUI.Extensions
{

    // Cookie'de tutalan verilenklerin kontrollerini yapmak için yazılacak metotları bu class içerisinde topluyorum.
    public static class ClaimsPrincipalExtensions
    {
        // this -> Bu sayede artık metot sondan çağrılır.
        // User.IsLogged() tarzında 
        public static bool IsLogged(this ClaimsPrincipal user)
        {
            if (user.Claims.FirstOrDefault(x => x.Type == "id") != null)
                return true;
            else
                return false;
        }

        public static string GetUserFirstName(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "firstname")?.Value;
        }

        public static string GetUserLastName(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "lastname")?.Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            if (user.Claims.FirstOrDefault(x => x.Type == "usertype")?.Value == "Admin")
                return true;
            else
                return false;

            //return user.Claims.FirstOrDefault(x => x.Type == "usertype")?.Value;
        }
    }
}
