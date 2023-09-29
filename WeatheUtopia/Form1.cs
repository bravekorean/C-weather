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
    public partial class Form1 : Form
    {
        private string loggedInUserID;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Properties.Resources.logo;
        }

        private void Button1_Click(object sender, EventArgs e)
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


                int login_status = 0;
                // 로그인 상태 변수 선언, 비로그인 상태 0
                string loginid = Lid_textbox.Text;
                // 로그인창에서 아이디 텍스트박스 값
                string loginpwd = Lpw_textbox.Text;
                // 로그인창에서 비밀번호 텍스트박스의 값
                string selectQuery = "SELECT * FROM user_info WHERE userid = \'" + loginid + "\' ";
                // sql에 로그인 아이디가 전송되서 셀렉트문 실행 (user_info 테이블에서 아이디를 기준으로 검색하는것.)

                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
                // mysql로 명령어를 전송하기위한 클래스 mysql에 selectquery의 쿼리문을 보내고 connection 값을 보내 연결시도
                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                // 입력값을 받고, ExecuteReader()로 입력값을 받고 해당 정보를 userAccount 변수에 저장함.
                while (userAccount.Read())
                {
                    if (loginid == (string)userAccount["userid"] && loginpwd == (string)userAccount["userpw"])
                    {
                        login_status = 1;
                    }
                }
                connection.Close();

                if (login_status == 1)
                {
                    MessageBox.Show("로그인 완료");
                    this.Visible = false;
                    Form3 form3 = new Form3(loginid);
                    form3.ShowDialog();
                }
                else
                {
                    MessageBox.Show("회원 정보를 확인해 주세요.");
                }
            }
            // userAccount의 정보를 읽을 동안 로그인 아이디 패스워드가 userAccount에 있는 값과 동일하다면 로그인 상태로 전환(status 1로 변경)
            // 밑에는 로그인상태라면 메세지박스로 로그인완료 메세지 출력 아니라면 회원 정보 재검토 사용자에게 출력 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog(); 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
