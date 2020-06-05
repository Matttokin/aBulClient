using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs_
{
    public partial class Form4 : Form
    {
        private const string path_to_list_users = @"C:\test\login.txt";
        private ArrayList список_ролей = new ArrayList() { "user", "moder", "adminis" };
        private List<users> список_пользователей = new List<users>();
        private bool доступ;

        public Form4()
        {
            InitializeComponent();

            get_data();

            comboBox1.DataSource = список_пользователей;
            comboBox1.DisplayMember = "логин";

            comboBox2.DataSource = список_ролей;
            comboBox2.SelectedIndex = список_ролей.IndexOf(список_пользователей[comboBox1.SelectedIndex].роль);
        }

        private void get_data()
        {
            request2server.getLoginList(path_to_list_users);
            using (StreamReader reader = new StreamReader(path_to_list_users))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();

                    string[] temp_data_users = line.Split(';');

                    список_пользователей.Add(new users() { логин = temp_data_users[0], роль = temp_data_users[1], путь = temp_data_users[2] });

                }
            }
        }

        public class users
        {
            public string логин { get; set; }
            public string роль { get; set; }
            public string путь { get; set; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (доступ)
            {
                comboBox2.SelectedIndex = список_ролей.IndexOf(список_пользователей[comboBox1.SelectedIndex].роль);
                доступ = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (доступ)
            {
                список_пользователей[comboBox1.SelectedIndex].роль = список_ролей[comboBox2.SelectedIndex].ToString();
                доступ = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (StreamWriter file_with_data_of_users = new StreamWriter(path_to_list_users, false, Encoding.Default))
            {
                foreach ( users и in список_пользователей)
                {
                    file_with_data_of_users.WriteLine(и.логин + ";" + и.роль + ";" + и.путь + ";");
                }
            }
            request2server.putUsersOrLogPassFile(path_to_list_users);
        }

        private void comboBoxs(object sender, EventArgs e)
        {
            доступ = true;
        }
    }
}
