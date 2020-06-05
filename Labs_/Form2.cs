using System;
using System.IO;
using System.Windows.Forms;

namespace Labs_
{
    public partial class Form2 : Form
    {
        public static string[] massline;

        public static string usernamecopy;
        public static string logintxt = @"C:\test\login.txt";
        public static string user;
        Form main;

        public Form2(Form mainu,  string username)
        {
            InitializeComponent();

            main = mainu;

            richTextBox1.Text = "";
             this.Text = "Второе окно";
            label1.Text = "Здравствуйте, " + username;
            usernamecopy = username;
            try
            {
                if (username == "admin")
                {
                    request2server.getLoginList(logintxt);
                    using (StreamReader sr = new StreamReader(logintxt))
                    {

                        while (!sr.EndOfStream)
                        {
                            while (!sr.EndOfStream)
                                richTextBox1.Text += sr.ReadLine() + "\n";
                        }
                    }

                    label4.Text += "Главный админ";
                    //label2.Text = "C:\\test\\login.txt";
                }
                else
                {
                    button2.Hide();
                    textBox1.Hide();
                    label3.Hide();
                    button4.Hide();
                    request2server.getLoginList(logintxt);
                    using (StreamReader reader = new StreamReader(logintxt))
                    {
                        string line;
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            massline = line.Split(';');
                            if (username == massline[0])
                            {
                                request2server.getUserFile(usernamecopy);
                                using (StreamReader reader2 = new StreamReader(@"C:\\test\\users\\" + usernamecopy + ".txt"))
                                {
                                    line = "";
                                    while (!reader2.EndOfStream)
                                        richTextBox1.Text += reader2.ReadLine() + "\n";
                                }
                                break;
                            }

                        }
                        label2.Text = "Рабочая область " + usernamecopy;
                    }

                    label4.Text += massline[1];

                    switch (massline[1])
                    {
                        case "user":
                            richTextBox1.MaxLength = 100;
                            label4.Text = label4.Text + " и Доступные символы 100";
                            break;
                        case "moder":
                            label3.Show();
                            textBox1.Show();
                            button2.Show();
                            richTextBox1.Enabled = false;
                            break;
                        case "adminis":
                            button4.Show();
                            label3.Show();
                            textBox1.Show();
                            button2.Show();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!: " + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (usernamecopy != "admin")
                {
                    request2server.getUserFile(usernamecopy);
                    using (StreamWriter writer = new StreamWriter(@"C:\\test\\users\\" + usernamecopy + ".txt"))
                        writer.WriteLine(richTextBox1.Text);
                    request2server.putUsersFile("C:\\test\\users\\" + usernamecopy + ".txt");
                    MessageBox.Show("Пользователь отработал");
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        request2server.getLoginList(logintxt);
                        using (StreamWriter wL = new StreamWriter(@"C:\\test\\login.txt"))
                            wL.WriteLine(richTextBox1.Text);
                        request2server.putUsersOrLogPassFile("C:\\test\\login.txt");
                    }
                    else
                    {
                        request2server.getUserFile(textBox1.Text);
                        using (StreamWriter wU = new StreamWriter(@"C:\\test\\users\\" + textBox1.Text + ".txt"))
                            wU.WriteLine(richTextBox1.Text);
                        request2server.putUsersFile("C:\\test\\users\\" + textBox1.Text + ".txt");
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!: " + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            user = textBox1.Text;
            richTextBox1.Text = "";
            try
            {
                request2server.getUserFile(user);
                using (StreamReader sr = new StreamReader(@"C:\\test\\users\\" + user + ".txt"))
                {
                    while (!sr.EndOfStream)
                        richTextBox1.Text += sr.ReadLine() + "\n";
                }
                label2.Text = "C:\\test\\users\\" + user + ".txt";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!: " + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            try
            {
                if (usernamecopy == "admin")
                {
                    request2server.getLoginList(logintxt);
                    using (StreamReader sr = new StreamReader(logintxt))
                    {
                        while (!sr.EndOfStream)
                            richTextBox1.Text += sr.ReadLine() + "\n";
                    }
                    label2.Text = usernamecopy;
                }
                else
                {
                    button2.Hide();
                    textBox1.Hide();
                    label3.Hide();
                    request2server.getLoginList(logintxt);
                    using (StreamReader reader = new StreamReader(logintxt))
                    {
                        string line;
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            massline = line.Split(';');
                            if (usernamecopy == massline[0])
                            {
                                request2server.getUserFile(usernamecopy);
                                using (StreamReader reader2 = new StreamReader(@"C:\\test\\users\\" + usernamecopy + ".txt"))
                                {
                                    line = "";
                                    while (!reader2.EndOfStream)
                                        richTextBox1.Text += reader2.ReadLine() + "\n";
                                }
                                break;
                            }

                        }
                        label2.Text =  usernamecopy;
                    }
                    if (massline[1] == "R") button1.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!: " + ex);
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form ols = new Form4();
            ols.Show();
        }
    }
}
