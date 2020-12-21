using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace test
{
    public class Check
    {
        private string Link = "http://am.deu.ac.kr";
        private readonly string Scouter = "z2v2a3hkji286e";
        private readonly string agent = "U-CheckPlus/5 CFNetwork/976 Darwin/18.2.0";
        private string Token = "";

        public string saveToken = "token.txt";

        public bool ReadToken()
        {
            if (File.Exists(saveToken))
            {
                StreamReader sr = new StreamReader(saveToken);
                Token = sr.ReadToEnd();
                sr.Close();
                return true;
            }
            return false;
        }

        public void SaveToken(string token)
        {
            StreamWriter sw = new StreamWriter(saveToken, false, Encoding.UTF8);
            sw.Write(token);
            sw.Close();
        }

        public string Login()
        {
            if (ReadToken())
            {
                return Token;
            }

            const string token = "apptype=as2&id=---&locale=ko&maddr=B336EEEF-F716-4643-BE3D-9197D3277358&mos=iPhoneOS12.1.4;iPhone X;5&pwd=---";
            string link = Link + "/libekasac/sacApp2/getToken.do?" + token;

            HttpWebRequest request = WebRequest.CreateHttp(link);
            request.Host = "am.deu.ac.kr";
            request.Method = "POST";

            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie
            {
                Name = "SCOUTER",
                Value = Scouter,
                Domain = "am.deu.ac.kr"
            });
            request.UserAgent = agent;

            request.GetRequestStream().Write(new byte[] { }, 0, 0);

            string result;

            using (var respond = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                result = respond.ReadToEnd();
            }

            foreach (var value in JObject.Parse(result).Last.Children().Children())
            {
                var pro = value.ToObject<JProperty>();
                if (pro.Name == "token")
                {
                    Token = pro.Value.ToString();
                    SaveToken(Token);
                    Console.WriteLine($"성공적 토큰 값 : {Token}");
                    return pro.Value.ToString();
                }
            }

            Console.WriteLine($"실패적 토큰 값 : {Token}");
            return null;
        }

        public string RefreshToken()
        {
            string token = "locale=ko&token=" + Token;
            string link = Link + "/libekasac/sacApp2/refreshToken.do?" + token;
            HttpWebRequest request = WebRequest.CreateHttp(link);

            request.Host = "am.deu.ac.kr";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie
            {
                Name = "SCOUTER",
                Value = Scouter,
                Domain = "am.deu.ac.kr"
            });
            request.Method = "POST";
            request.UserAgent = agent;
            request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

            request.Accept = "*/*";

            request.GetRequestStream().Write(new byte[] { }, 0, 0);

            //request.GetRequestStream().Write(e, 0, e.Length);

            using (var respond = request.GetResponse().GetResponseStream())
            {
                using (var reader = new StreamReader(respond))
                {
                    var pro = JObject.Parse(reader.ReadToEnd());
                    var result = pro.First.Next.First["token"].ToString();

                    Token = result;

                    SaveToken(Token);
                    Console.WriteLine($"성공적 토큰 갱신 값 : {Token}");
                    return Token;
                }
            }
        }
        
        public string GetLecture()
        {
            string token = "/libekasac/sacApp2/json/hakbun_S_B336EEEF-F716-4643-BE3D-9197D3277358__iPhoneOS12.1.4_aVBob25lIFg=_5_is2_20190312132530_N_ko@getInfo.json";
            string link = Link + token;

            HttpWebRequest request = WebRequest.CreateHttp(link);
            request.Host = "am.deu.ac.kr";
            request.Accept = "*/*";
            request.Method = "POST";
            request.CookieContainer = new CookieContainer(2);
            request.CookieContainer.Add(new Cookie
            {
                Name = "JSESSIONID",
                Value = "70CE44D4FB6C8F0077A6A6BCBE0826D9",
                Domain = "am.deu.ac.kr"
            });
            request.CookieContainer.Add(new Cookie
            {
                Name = "SCOUTER",
                Value = Scouter,
                Domain = "am.deu.ac.kr"
            });
            request.UserAgent = "U-CheckPlus/5 CFNetwork/976 Darwin/18.2.0";

            request.GetRequestStream().Write(new byte[] { }, 0, 0);

            using (var respond = request.GetResponse().GetResponseStream())
            {
                using (var reader = new StreamReader(respond))
                {
                    Console.WriteLine($"성공적 강의 목록 가져오기 : {Token}");
                    return reader.ReadToEnd();
                }
            }

        }

        public bool AttendingCheck(int type, int classNo, string ios_uuid, int lectureType, int lectureNo, string localName, string roomCd)
        {
            string link = Link + "/libekasac/sacApp2/attendCheck.do";
            HttpWebRequest request = WebRequest.CreateHttp(link);

            request.Host = "am.deu.ac.kr";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie
            {
                Name = "SCOUTER",
                Value = Scouter,
                Domain = "am.deu.ac.kr"
            });
            request.Method = "POST";
            request.UserAgent = agent;
            request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

            request.Accept = "*/*";

            string data = $"attend_request_type={type}&" +
            $"cno={classNo}&" +
            $"ios_dynamic_uuid={ios_uuid}&" +
            $"lecture_type={lectureType}&" +
            $"lecture_week=2&" +
            $"lno={lectureNo}&" +
            $"local_name={localName}&" +
            "locale=ko&" +
            "memo=F3D0BF690B997562CAC6B1D8845D11C2F3570564749C05C6C3BB1F90CEBD3A98B2569AA86AA38C83CCA89921078A1CE9D467225A7926B406541D567BB2041EC8&" +
            $"room_cd={roomCd}&" +
            $"token={Token}";

            var convByte = Encoding.UTF8.GetBytes(data);

            request.GetRequestStream().Write(convByte, 0, convByte.Length);

            string result;
            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                result = reader.ReadToEnd();
                Console.WriteLine($"출석체크 결과 : {result}");
            }

            return true;
        }

    }
}
