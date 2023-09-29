using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WeatheUtopia
{
    public partial class Form3 : Form
    {
        DateTime nowDate = DateTime.Now;
        private string loguserid;
        private double x;
        private double y;
        private string add1;
        


        public struct lamc_parameter
        {
            public double Re;      /* 사용할 지구반경 [ km ] */
            public double grid;    /* 격자간격 [ km ] */
            public double slat1;   /* 표준위도 [degree] */
            public double slat2;   /* 표준위도 [degree] */
            public double olon;    /* 기준점의 경도 [degree] */
            public double olat;    /* 기준점의 위도 [degree] */
            public double xo;      /* 기준점의 X좌표 [격자거리] */
            public double yo;      /* 기준점의 Y좌표 [격자거리] */
        }

        public class WgsToBaseStationCoord
        {
            lamc_parameter map;

            public WgsToBaseStationCoord()
            {
                map.Re = 6371.00877;     // 지도반경
                map.grid = 5.0;          // 격자간격 (km)
                map.slat1 = 30.0;        // 표준위도 1
                map.slat2 = 60.0;        // 표준위도 2
                map.olon = 126.0;        // 기준점 경도
                map.olat = 38.0;         // 기준점 위도
                map.xo = 43;             // 기준점 X좌표
                map.yo = 136;            // 기준점 Y좌표
            }

            public LatXLonY dfs_xy_conv(double _dLat, double _dLon)
            {
                double DEGARD = Math.PI / 180.0;

                double re = map.Re / map.grid;
                double slat1 = map.slat1 * DEGARD;
                double slat2 = map.slat2 * DEGARD;
                double olon = map.olon * DEGARD;
                double olat = map.olat * DEGARD;

                double sn = Math.Tan(Math.PI * 0.25 + slat2 * 0.5) / Math.Tan(Math.PI * 0.25 + slat1 * 0.5);
                sn = Math.Log(Math.Cos(slat1) / Math.Cos(slat2)) / Math.Log(sn);
                double sf = Math.Tan(Math.PI * 0.25 + slat1 * 0.5);
                sf = Math.Pow(sf, sn) * Math.Cos(slat1) / sn;
                double ro = Math.Tan(Math.PI * 0.25 + olat * 0.5);
                ro = re * sf / Math.Pow(ro, sn);

                LatXLonY rs = new LatXLonY();
                rs.lat = _dLat;
                rs.lon = _dLon;

                double ra = Math.Tan(Math.PI * 0.25 + _dLat * DEGARD * 0.5);
                ra = re * sf / Math.Pow(ra, sn);
                double theta = _dLon * DEGARD - olon;
                if (theta > Math.PI) theta -= 2.0 * Math.PI;
                if (theta < -Math.PI) theta += 2.0 * Math.PI;
                theta *= sn;
                rs.x = Math.Floor(ra * Math.Sin(theta) + map.xo + 0.5);
                rs.y = Math.Floor(ro - ra * Math.Cos(theta) + map.yo + 0.5);

                return rs;


            }
        }

        public class LatXLonY
        {
            public double lat;
            public double lon;

            public double x;
            public double y;
        }

        public Form3(string userid)
        {
            InitializeComponent();
            loguserid = userid;
        }

        private async void Form3_Load(object sender, EventArgs e)
        {

            string restApiKey = "{service.key}"; // 카카오 API 키
            string address = "서울특별시 중구 필동"; // 기본 주소값

            label7.Text = address;
            add1 = address;

            // 주소 검색 및 좌표 변환
            string[] coordinates = await SearchAddress(restApiKey, address);
            Debug.WriteLine($"x: {coordinates[0]}, y: {coordinates[1]}");

            if (coordinates != null)
            {

                double lat = Convert.ToDouble(coordinates[1]);
                double lon = Convert.ToDouble(coordinates[0]);

                // 그리드 좌표 변환
                WgsToBaseStationCoord converter = new WgsToBaseStationCoord();
                LatXLonY gridCoords = converter.dfs_xy_conv(lat, lon);
                Debug.WriteLine($"x: {gridCoords.x}, y: {gridCoords.y}");

                // 기상청 API 호출
                await CallWeatherAPI(gridCoords.x.ToString(), gridCoords.y.ToString());
            }


        }

        private async Task CallWeatherAPI2(string x, string y)
        {
            DateTime prevDate = DateTime.Today.AddDays(-1); //  아침 8시에 프로그램실행했는데 당일 오후 17시 예보를 받아올 순 없으니깐 하루 전 꺼 들고옴.
            DateTime nowDate = DateTime.Today;
            string today = nowDate.ToString("yyyyMMdd");
            DateTime nextDate = DateTime.Today.AddDays(+1);
            string nextday = nextDate.ToString("yyyyMMdd");
            DateTime twoDayslater = DateTime.Today.AddDays(2);
            string twoday = twoDayslater.ToString("yyyyMMdd");
            DateTime now = DateTime.Now;
            Debug.WriteLine(nowDate.ToString("yyyyMMdd"));
     
            string url = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getVilageFcst"; // URL
            url += "?ServiceKey=" + "{service.key]"; // Service Key
            url += "&pageNo=2";
            url += "&numOfRows=1000";
            url += "&dataType=JSON";
            url += "&base_date=" + prevDate.ToString("yyyyMMdd");
            url += "&base_time=1700";
            url += $"&nx={x}";
            url += $"&ny={y}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                //List<weatherData> weatherDataList = new List<weatherData>();
                string popValue = "";
                string tmnValue = "";
                string tmxValue = "";
                string popValue1 = "";
                string tmnValue1 = "";
                string tmxValue1 = "";
                string popValue2 = "";
                string tmnValue2 = "";
                string tmxValue2 = "";

                if (!string.IsNullOrEmpty(responseContent))
                {
                    
                    // 응답 데이터를 파싱하여 필요한 정보 추출
                    dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    foreach (JToken item in responseData.response.body.items.item)
                    {
                        string fcstDate = item["fcstDate"]?.ToString();
                        string fcstTime = item["fcstTime"]?.ToString();
                        string category = item["category"].ToString();

                        if(fcstDate == today && fcstTime == "0600")
                        {
                            if (category == "POP")
                            {
                                popValue = item["fcstValue"]?.ToString();
                            }
                            if (category == "TMN")
                            {
                                tmnValue = item["fcstValue"]?.ToString();
                            }
                            
                     

                        }
                        if(fcstDate == today && fcstTime == "1500")
                        {
                            if (category == "TMX")
                            {
                                tmxValue = item["fcstValue"]?.ToString();
                            }

                        }
                        if (fcstDate == nextday && fcstTime == "1500")
                        {
                            if (category == "TMX")
                            {
                                tmxValue1 = item["fcstValue"]?.ToString();
                            }

                        }
                        if (fcstDate == twoday && fcstTime == "1500")
                        {
                            if (category == "TMX")
                            {
                                tmxValue2 = item["fcstValue"]?.ToString();
                            }

                        }

                        if (fcstDate == nextday && fcstTime == "0600" )
                         {
                            if (category == "POP")
                            {
                                popValue1 = item["fcstValue"]?.ToString();
                            }
                            if (category == "TMN")
                            {
                                tmnValue1 = item["fcstValue"]?.ToString();
                            }
                            if (category == "TMX")
                            {
                                tmxValue1 = item["fcstValue"]?.ToString();
                            }
                        }
                        
                        if (fcstDate == twoday && fcstTime == "0600")
                        {
                            if (category == "POP")
                            {
                                popValue2 = item["fcstValue"]?.ToString();
                            }
                            if (category == "TMN")
                            {
                                tmnValue2 = item["fcstValue"]?.ToString();
                            }
                            if (category == "TMX")
                            {
                                tmxValue2 = item["fcstValue"]?.ToString();
                            }
                        }

                        // UI 업데이트를 위해 Invoke 사용
                            label8.Text = today;
                            label9.Text = nextday;
                            label10.Text = twoday;
                            label11.Text = $"강수확률:{popValue}%";
                            label12.Text = $"강수확률:{popValue1}%";
                            label13.Text = $"강수확률:{popValue2}%";
                            label14.Text = $"최저 : {tmnValue}°C";
                            label15.Text = $"최저 : {tmnValue1}°C";
                            label16.Text = $"최저 : {tmnValue2}°C";
                            label17.Text = $"최고 : {tmxValue}°C";
                            label18.Text = $"최고 : {tmxValue1}°C";
                            label19.Text = $"최고 : {tmxValue2}°C";
                       
                    }
                }
                
                
            }
        }
       
        private async Task CallWeatherAPI(string x, string y)
        {
            DateTime nowDate = DateTime.Today;
            DateTime now = DateTime.Now;
            DateTime baseTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            baseTime = baseTime.AddHours(-1); // 1시간을 뺍니다. 빼는 이유는 매 시간 30분마다 갱신되는데 
            string baseTimeString = baseTime.ToString("HHmm");
            // 매 시간 30분이 되기전에 받아오기전엔 요청을 해도 값이 없기때문에 로직상 이유로 현재 시간에서 1시간 전 정보를 들고옵니다.
            Debug.WriteLine(nowDate.ToString("yyyyMMdd"));
            Debug.WriteLine(baseTimeString);
            string url = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getUltraSrtNcst"; // URL
            url += "?ServiceKey=" + "{service.key}"; // Service Key
            url += "&pageNo=1";
            url += "&numOfRows=1000";
            url += "&dataType=JSON";
            url += "&base_date=" + nowDate.ToString("yyyyMMdd");
            url += "&base_time=" + baseTimeString;
            url += $"&nx={x}";
            url += $"&ny={y}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                //List<weatherData> weatherDataList = new List<weatherData>();
                string ptyValue = null; // PTY 값 저장 변수
                string rn1Value = null; // PCP 값 저장 변수
                string rehValue = null; // SKY 값 저장 변수
                string t1hValue = null; // TMP 값 저장 변수

                if (!string.IsNullOrEmpty(responseContent))
                {
                    // 응답 데이터를 파싱하여 필요한 정보 추출
                    dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    foreach (JToken item in responseData.response.body.items.item)
                    {
                        string category = item["category"].ToString();

                        if (category == "PTY")
                        {
                            ptyValue = item["obsrValue"]?.ToString();
                        }
                        else if (category == "RN1")
                        {
                            rn1Value = item["obsrValue"]?.ToString();
                        }
                        else if (category == "REH")
                        {
                            rehValue = item["obsrValue"]?.ToString();
                        }
                        else if (category == "T1H")
                        {
                            t1hValue = item["obsrValue"]?.ToString();
                        }

                        if (ptyValue != null && rn1Value != null && rehValue != null && t1hValue != null)
                        {
                            break; // PTY, PCP, SKY, TMP 값이 모두 수집되면 반복문 종료
                        }
                       
                    }
                }
                await CallWeatherAPI2(x, y);
                UpdateLabels(ptyValue, rn1Value, rehValue, t1hValue);
                UpdateRealtimeLabel();
            }
        }
        private void UpdateLabels(string ptyValue, string rn1Value, string rehValue, string t1hValue)
        {
            if (ptyValue == "0")
            {
                ptyValue = "비 안옴";
                pictureBox1.Image = Properties.Resources.sun;
            } else if (ptyValue == "1") 
            {
                ptyValue = "비";
                pictureBox1.Image = Properties.Resources.rain;
            } else if ( ptyValue == "2") 
            {
                ptyValue = "비/눈";
                pictureBox1.Image = Properties.Resources.snowrain;
            } else if (ptyValue == "3")
            {
                ptyValue = "눈";
                pictureBox1.Image = Properties.Resources.snow;
            } else if (ptyValue == "4")
            {
                ptyValue = "소나기";
                pictureBox1.Image = Properties.Resources.rain;
            } else if (ptyValue =="5")
            {
                ptyValue = "빗방울";
                pictureBox1.Image = Properties.Resources.rain;
            } else if (ptyValue == "6")
            {
                ptyValue = "빗방울눈날림";
                pictureBox1.Image = Properties.Resources.snowrain;
            } else if (ptyValue == "7")
            {
                ptyValue = "눈날림";
                pictureBox1.Image = Properties.Resources.snow;
            }
             
           

            // UI 업데이트를 위해 Invoke 사용
            Invoke(new Action(() =>
            {
                label1.Text = $"강수형태: {ptyValue}";
                label2.Text = $"1시간 강수량: {rn1Value}mm";
                label3.Text = $"습도: {rehValue}%";
                label4.Text = $"기온: {t1hValue}°C";
            }));
        }
        private void UpdateRealtimeLabel()
        {
            // UI 업데이트를 위해 Invoke 사용
            Invoke(new Action(() =>
            {
                label5.Text = $"{DateTime.Now}";
            }));
        }

        


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async Task<string[]> SearchAddress(string apiKey, string address)
        {
            string apiUrl = "https://dapi.kakao.com/v2/local/search/address.json";
            string query = Uri.EscapeDataString(address);
            string requestUrl = $"{apiUrl}?query={query}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"KakaoAK {apiKey}");

                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();

                // 응답 데이터 파싱
                JObject responseData = JObject.Parse(responseContent);
                JArray addresses = responseData["documents"] as JArray;

                if (addresses != null && addresses.Count > 0)
                {
                    // 첫 번째 결과의 "x"와 "y" 값을 추출하여 배열로 반환
                    string x = addresses[0]["x"]?.ToString();
                    string y = addresses[0]["y"]?.ToString();

                    return new string[] { x, y };
                }
            }

            return null;
        }
        class weatherData
        {
            public string BaseDate { get; set; }
            public string ForecastDate { get; set; }
            public string ForecastTime { get; set; }
            public string Category { get; set; }
            public string ForecastValue { get; set; }
            public string LocationX { get; set; }
            public string LocationY { get; set; }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            string restApiKey = "{api.key}";
            string address = textBox1.Text;
            label7.Text = address;
            add1 = address;


            // 주소 검색 및 좌표 변환
            string[] coordinates = await SearchAddress(restApiKey, address);

            if (coordinates != null)
            {
                double lat = Convert.ToDouble(coordinates[1]);
                double lon = Convert.ToDouble(coordinates[0]);

                // 그리드 좌표 변환
                WgsToBaseStationCoord converter = new WgsToBaseStationCoord();
                LatXLonY gridCoords = converter.dfs_xy_conv(lat, lon);
                Debug.WriteLine($"x: {gridCoords.x}, y: {gridCoords.y}");
                // 그리드 좌표 저장 
                x = gridCoords.x;
                y = gridCoords.y;


                // 기상청 API 호출
                await CallWeatherAPI(gridCoords.x.ToString(), gridCoords.y.ToString());
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string address = label7.Text;
            string connectionString = "{db address, id,pass}";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // user_info 테이블 업데이트 쿼리 작성
                string updateQuery = "UPDATE user_info SET areaX = @x, areaY = @y, areaName = @address WHERE userid = @userId";
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);

                updateCommand.Parameters.AddWithValue("@address", address);
                updateCommand.Parameters.AddWithValue("@x", x.ToString());
                updateCommand.Parameters.AddWithValue("@y", y.ToString());
                updateCommand.Parameters.AddWithValue("@userId", loguserid);
                int result = updateCommand.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("저장되었습니다.");
                }
                else
                {
                    MessageBox.Show("값이 null이거나, 오류입니다.");
                }

            }


        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "{db adress, id , pass }";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // user_info 테이블에서 areaX와 areaY, areaName 조회 쿼리 작성
                string selectQuery = "SELECT areaX, areaY, areaName FROM user_info WHERE userid = @userId";
                MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@userId", loguserid); // 사용자 ID를 해당하는 값으로 대체해야 합니다.
                MySqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {

                    double x = Convert.ToDouble(reader["areaX"]);
                    double y = Convert.ToDouble(reader["areaY"]);
                    string address = reader["areaName"].ToString();
                    reader.Close();

                    label7.Text = address;
                    await CallWeatherAPI(x.ToString(), y.ToString());
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("DB에서 좌표 값을 가져오지 못했습니다.");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void userinfo_Click(object sender, EventArgs e)
        {
            
            Form4 form4 = new Form4(loguserid);
            form4.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}