using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatheUtopia
{
    public partial class Form4 : Form
    {
        private string loguserid;
        public Form4(string userid)
        {
            InitializeComponent();
            loguserid = userid;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string connectionString = "{db adress, id, pass } ";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // user_info 테이블에서 areaX와 areaY 조회 쿼리 작성
                string selectQuery = "SELECT * FROM user_info WHERE userid = @userId";
                MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@userId", loguserid); // 사용자 ID를 해당하는 값으로 대체해야 합니다.
                MySqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    string userid = reader["userid"].ToString();
                    string userpw = reader["userpw"].ToString();
                    string username = reader["username"].ToString();
                    string areaX = reader["areaX"].ToString();
                    string areaY = reader["areaY"].ToString();
                    label1.Text = $"아이디 : {userid}";
                    label2.Text = $"비밀번호 : {userpw}";
                    label3.Text = $"닉네임 : {username}";
                   label4.Text = $"좌표X값 : {areaX}";
                   label5.Text = $"좌표Y값 : {areaY}";

                    reader.Close();

                    
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("DB에서 좌표 값을 가져오지 못했습니다.");
                }
            }
        }
    }
}
