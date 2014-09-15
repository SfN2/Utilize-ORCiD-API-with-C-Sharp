 public class ORCiDInterface
    {
        public List<string> GetAccessToken(string code, string redirectUrl, string clientSecret, string clientId)
        {
            string token = string.Empty;
            string tokenEnd = string.Empty;
            List<string> result = new List<string>();

            try
            {
                tokenEnd = "http://pub.orcid.org/oauth/token?code=" + code + "&redirect_uri=" + redirectUrl + "&client_id=" + clientId + "&scope=&client_secret" + clientSecret +"&grant_type=authorization_code";

                string method = "POST";

                Uri uri = new Uri(tokenEnd);
                var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
                request.Method = method;
                request.ContentType = "application/x-www-form-urlencoded";
                var response = (System.Net.HttpWebResponse)request.GetResponse();
                string strResponse = string.Empty;

                Stream receiveStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

                StreamReader readStream = new StreamReader(receiveStream, encode);
                    strResponse = readStream.ReadToEnd();

                result = ParseToken(strResponse);
            }
            catch
            {
            }
            finally
            {
            }

            return result;
        }

        protected List<string> ParseToken(string result)
        {
            /*
             * result = "{\"access_token\":\"7619fe14-9a13-45c2-bce2-b3603f7ff76a\",\"token_type\":\"bearer\",\"refresh_token\":\"55a24d04-ddf4-4465-a786-d4db8e0f742a\",\"expires_in\":630460063,\"scope\":\"/orcid-bio/read-limited\",\"orcid\":\"0000-0002-8313-763X\"}"
            */

            var parts = (List<string>)(Regex.Replace(result, "[^a-zA-Z0-9_,-]+", "")).Split(',').ToList<string>();
            List<string> idToken = new List<string>();

            idToken.Add(parts[0].ToString().Replace("access_token", ""));
            idToken.Add(parts[parts.Count - 1].ToString().Replace("orcid", ""));

            return idToken;
        }
	}