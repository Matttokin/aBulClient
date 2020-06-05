using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Labs_
{
    static class request2server
    {
        private static string webUrl = "http://localhost:1864/";

        public static string Host { get => webUrl; set => webUrl = value; }

        static public void getPasswordList(string path)
        {
            // Адрес ресурса, к которому выполняется запрос
            string url = webUrl + "api/getPasswordList";

            // Создаём объект WebClient
            using (var webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
                response = response.Substring(1, response.Length - 2);
                response = response.Replace("\\\"", "\"");
                response = response.Replace("\\", "");
                var responseSplit = JsonConvert.DeserializeObject<List<string>>(response);

                using (var writer = new StreamWriter(path))
                {
                    foreach (string split in responseSplit)
                    {
                        if (split.Length > 1)
                            writer.WriteLine(split);
                    }
                    writer.Close();
                }
            }
        }
        static public void getLoginList(string path)
        {
            string url = webUrl + "api/getLoginList";

            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Substring(1, response.Length - 2);
                response = response.Replace("\\\"", "\"");
                response = response.Replace("\\", "");
                var responseSplit = JsonConvert.DeserializeObject<List<string>>(response);
                using (var writer = new StreamWriter(path))
                {
                    foreach (string split in responseSplit)
                    {
                        if (split.Length > 1)
                            writer.WriteLine(split);
                    }
                    writer.Close();
                }
                using (var writer = new StreamWriter(path))
                {
                    foreach (string split in responseSplit)
                    {
                        if (split.Length > 1)
                            writer.WriteLine(split);
                    }
                    writer.Close();
                }
            }
        }
        static public void getUserFile(string user)
        {
            string url = webUrl + "api/getUserFile?user=" + user;

            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Substring(1, response.Length - 2);
                response = response.Replace("\\\"", "\"");
                response = response.Replace("\\", "");
                var responseSplit = JsonConvert.DeserializeObject<List<string>>(response);

                using (var writer = new StreamWriter("C:\\test\\users\\" + user + ".txt"))
                {
                    foreach (string split in responseSplit)
                    {
                        if (split.Length > 1)
                            writer.WriteLine(split);
                    }
                    writer.Close();
                }
            }
        }
        static public void getLogpassList(string path)
        {
            string url = webUrl + "api/getLogpassList";

            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Substring(1, response.Length - 2);
                response = response.Replace("\\\"", "\"");
                response = response.Replace("\\", "");
                var responseSplit = JsonConvert.DeserializeObject<List<string>>(response);
                using (var writer = new StreamWriter(path))
                {
                    foreach (string split in responseSplit)
                    {
                        if (split.Length > 1)
                            writer.WriteLine(split);
                    }
                    writer.Close();
                }
            }
        }
        static public void putUsersFile(string path)
        {
            string url = webUrl + "api/putUserFile";

            using (var webClient = new WebClient())
            {
                var response = webClient.UploadFile(url, path);
            }
        }
        static public void putUsersOrLogPassFile(string path)
        {
            string url = webUrl + "api/putUsersOrLogPassFile";

            using (var webClient = new WebClient())
            {
                var response = webClient.UploadFile(url, path);
            }
        }
        static public void putPasswordnFile(string path)
        {
            string url = webUrl + "api/putPasswordFile";

            using (var webClient = new WebClient())
            {
                var response = webClient.UploadFile(url, path);
            }
        }
    }
}
