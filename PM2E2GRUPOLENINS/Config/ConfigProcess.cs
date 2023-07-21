using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E2GRUPOLENINS.Config
{
    public class ConfigProcess
    {
        // Web Api http
        public static string ipaddress = "192.168.0.19:82";
        public static string webapi = "PM2E2CRUD";

        //Routing  METHOD POST
        public static string postRoute = "SitioCreate.php";
        // Routing METHOD GET
        public static string getRoute = "SitioGetList.php";

        //Format URL
        public static string formaturl = "http://{0}/{1}/{2}";

        // URl Endpoint
        public static string ApiGET = string.Format(formaturl, ipaddress, webapi, getRoute);
        public static string ApiPOST = string.Format(formaturl, ipaddress, webapi, postRoute);
    }
}
