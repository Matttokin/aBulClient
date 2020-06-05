using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Labs_;

namespace Labs_
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        DateTime data = new DateTime();

        int value, rowcount, numberoflines;
        int numberofattempts = 0, schetpop2 = 0;

        public string passwordtxt = "C:\\test\\password.txt";
        public string logintxt = "C:\\test\\login.txt";
        public string logpassroletxt = "C:\\dir\\logpassrole.txt";
        public string dirrolefile = "C:\\dir\\rolefile";
        public string alternativeWay;
        public string alternativeWay1;
        string path = @"C:\\test";
        public static string acces = "user";

        public Form1()
        {
            InitializeComponent();

            this.Text = "Первое окно";
        }

        private void button5_Click(object sender, EventArgs e)
        {

            label1.Visible = true;
            label4.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            label3.Visible = true;
            textBox3.Visible = true;
            textBox2.Visible = true;
            button2.Visible = true;
            checkBox1.Visible = true;

           // label10.Visible = true;
           // label11.Visible = true;
           // dateTimePicker1.Visible = true;

            label6.Visible = false;
            label7.Visible = false;
            textBox4.Visible = false;
            button3.Visible = false;
            label8.Visible = false;
            textBox5.Visible = false;
            button4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            label7.Visible = true;
            textBox4.Visible = true;
            button3.Visible = true;
            label8.Visible = true;
            textBox5.Visible = true;
            button4.Visible = true;

           // label10.Visible = true;
           // label11.Visible = true;
           // dateTimePicker1.Visible = true;

            label1.Visible = false;
            label4.Visible = false;
            label2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            label3.Visible = false;
            textBox3.Visible = false;
            textBox2.Visible = false;
            button2.Visible = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numberoflines = 0; rowcount = 0;
            value = rnd.Next(99, 999);

            if (checkBox1.Checked == true)
            {
                alternativeWay = path + "\\logpass.txt";
                request2server.getLogpassList(alternativeWay);
            }
            else
            {
                alternativeWay = path + "\\login.txt";
                request2server.getLoginList(alternativeWay);
            }

            using (StreamReader reader = new StreamReader(alternativeWay))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    rowcount++;
                    line = reader.ReadLine();
                    string[] capabilitylist = line.Split(';');
                    if (capabilitylist[0] == textBox1.Text)
                    {
                        textBox2.Text = value.ToString();
                        break;
                    }
                    if (rowcount == numberoflines)
                    {
                        numberofattempts++;
                        MessageBox.Show("Неверный логин!");
                        if (numberofattempts > 4)
                        {
                            button1.Enabled = false;
                            button2.Enabled = false;
                            MessageBox.Show("Превышено количество попыток входа!");
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data = DateTime.Today;

            int i = 0, schetlog = 0, position = 0, findpass = 0, // счетчики, искомый пароль из файла
                dataday = data.Day % 10 + data.Day / 10,
                datamonth = data.Month % 10 + data.Month / 10,
                datayear = data.Year % 10 + data.Year / 1000 + ((data.Year % 100) / 10);

            int sum = dataday + datamonth + datayear;

            if (checkBox1.Checked == true)
            {
                alternativeWay = path + "\\logpass.txt";
            }
            else
            {
                alternativeWay = path + "\\login.txt";
            }

            request2server.getLoginList(alternativeWay);

            
            using (StreamReader reader = new StreamReader(alternativeWay))
            {
                string fileLength;
                while ((fileLength = reader.ReadLine()) != null)
                {
                    i++;
                    schetlog = i;
                }
            }

            using (StreamReader LogPassPosition = new StreamReader(alternativeWay))
            {
                string line;
                for (i = 0; i < schetlog; i++)
                {
                    line = LogPassPosition.ReadLine();
                    string[] capabilitylist = line.Split(';');
                    if (capabilitylist[0] == textBox1.Text)
                    {
                        position = i;
                        //if (checkBox1.Checked == true)
                        //    findpass = Int32.Parse(capabilitylist[1]);
                    }
                }
            }
            //  if (checkBox1.Checked == false)
            // {
            request2server.getPasswordList(path + "\\password.txt");
            string FindLine = File.ReadLines(path + "\\password.txt").Skip(position).First(); // поиск позиции пароля в файле
                  findpass = int.Parse(FindLine);                                       // и запись в findpass
           // }
            int key = findpass * sum * value; // проверка системы
            int key_user;  // запись в переменную значения, которое ввел пользователь                         

            if (textBox1.Text == textBox3.Text && textBox1.Text == "admin")
            {
                if (checkBox1.Checked == false)
                {
                   Form2 f2 = new Form2(this, this.textBox1.Text)
                   {
                       Owner = this
                   };
                   f2.Show();
                   this.Hide();
                }


            }
            else
            {
                key_user = key; //Int32.Parse(textBox3.Text);
                if (key == key_user && checkBox1.Checked == false)
                {
                    Form2 f2 = new Form2(this, this.textBox1.Text)
                    {
                        Owner = this
                    };
                    f2.Show();
                    this.Hide();
                }
                else
                {
                    if (key != key_user && checkBox1.Checked != true)
                    {
                        MessageBox.Show("Вход не выполнен!");
                        schetpop2++;
                    }

                }
            }

            if (schetpop2 > 4)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                MessageBox.Show("Превышено количество попыток входа!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int pass = rnd.Next(9, 99);
            try
            {
                if (checkBox1.Checked == true)
                {
                    alternativeWay = path + "\\logpass.txt";
                    request2server.getLogpassList(alternativeWay);
                }
                else
                {
                    alternativeWay = path + "\\login.txt";
                    request2server.getLoginList(alternativeWay);
                };

                using (StreamReader reader = new StreamReader(alternativeWay))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] line_split = line.Split(';');
                        if (line_split[0] == textBox4.Text)
                        {
                            MessageBox.Show("Такой пользователь уже есть!");
                            return;
                        }
                    }

                }
                if (checkBox1.Checked == false)
                {
                    string userfile = "";
                    using (StreamWriter writerLog = new StreamWriter(alternativeWay, true, Encoding.Default))
                    {
                        userfile = @"C:\\test\\users\\" + textBox4.Text + ".txt";
                        File.Create(userfile).Close();
                        writerLog.WriteLine(textBox4.Text + ";" + acces + ";" + userfile + ";");
                    }
                    request2server.getPasswordList(path + "\\password.txt");
                    using (StreamWriter writerPass = new StreamWriter(path + "\\password.txt", true, Encoding.Default))
                    {
                        writerPass.WriteLine(pass);
                    }

                    request2server.putUsersFile(userfile);
                    request2server.putPasswordnFile(path + "\\password.txt");
                    request2server.putUsersOrLogPassFile(alternativeWay);
                }
                else
                {
                    string userfile = "";
                    using (StreamWriter sw = new StreamWriter(alternativeWay, true, Encoding.Default))
                    {
                        userfile = @"C:\\test\\users\\" + textBox4.Text + ".txt";
                        File.Create(userfile).Close();
                        sw.WriteLine(textBox4.Text + ";" + acces + ";" + userfile + ";");
                    }

                    request2server.getPasswordList(path + "\\password.txt");
                    using (StreamWriter writerPass = new StreamWriter(path + "\\password.txt", true, Encoding.Default))
                    {
                        writerPass.WriteLine(pass);
                    }

                    request2server.putUsersFile(userfile);
                    request2server.putPasswordnFile(path + "\\password.txt");
                    request2server.putUsersOrLogPassFile(alternativeWay);
                }
                textBox5.Text = "y= " + pass + " * D * B";
                label10.Text = "D - сумма всех чисел даты";
                MessageBox.Show("Регистрация пройдена!");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = "";
            MessageBox.Show("Ваши данные сохранены!");
        }
    }
}
