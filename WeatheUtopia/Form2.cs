using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WeatheUtopia
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Rpw_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void Rname_text_TextChanged(object sender, EventArgs e)
        {

        }

        private void Registerbtn_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("Server = localhost;Database=weatherdb;Uid=root;Pwd=1234;");
                // sql 서버와 연결
                // Database = 스키마 이름
                // uid = db 로그인 아이디 이 경우 root
                // upw = db 로그인 비밀번호 
                connection.Open();
                // sql 서버 연결

                string insertQuery = "INSERT INTO user_info (userid, userpw,username) VALUES ('" + Rid_text.Text + "', '" + Rpw_text.Text + "', '" + Rname_text.Text + "');";
                MySqlCommand command = new MySqlCommand(insertQuery, connection);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show(Rname_text.Text + "님 회원가입 완료, 사용할 아이디는 " + Rid_text.Text + "입니다.");
                    connection.Close();
                    Close();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("비정상 입력 정보, 재확인 요망");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
           
        }
    }

