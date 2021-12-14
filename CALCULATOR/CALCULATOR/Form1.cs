using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CALCULATOR
{
    public partial class Form1 : Form
    {



        // VİDEO LİNKİM = https://drive.google.com/file/d/1r-gkYByB7MJ2bhOi2-Membeufh3HQ_yT/view?usp=sharing  //






        bool FormCmplt = false;
        bool optState = true;

        bool opResult = false;
        string op = "";
        double result = 0;




        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }





                                                                    // REGISTER PANEL CODES //



        private bool FirstNum()
        {

            bool control4 = true;

            char[] Num = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            char[] NotNum = txtUsername.Text.ToCharArray();

            for (int i = 0; i < Num.Length; i++)
            {
                if (NotNum[0] == Num[i])
                {
                    MessageBox.Show("The first string of the username can not be a digit!!", "NOT DIGIT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    control4 = false;
                }
            }

            return control4;
        }


        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 32)
            {
                e.Handled = true;
            }
        }


        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 32)
            {
                e.Handled = true;
            }
        }


        private void txtUsername_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '£' || e.KeyChar == '½' ||
                e.KeyChar == '€' || e.KeyChar == '₺' ||
                e.KeyChar == '¨' || e.KeyChar == 'æ' ||
                e.KeyChar == 'ß' || e.KeyChar == '´')
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 33 && (int)e.KeyChar <= 47)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 58 && (int)e.KeyChar <= 64)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 91 && (int)e.KeyChar <= 96)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 123 && (int)e.KeyChar <= 127)
            {
                e.Handled = true;
            }
        }


        public void btnRegister_click(object sender, EventArgs e)
        {
            bool control1 = true;

            bool control2 = true;

            bool control3 = true;

            bool control4 = true;

            string hold;

            string hold2;

            string password;
            string Number = "1234567890";
            string upper = "ABCÇDEFGĞHIİJKLMNOöPRSŞTUÜVYZ";
            string lower = "abcçdefgğhıijklmnoöprsştuüvyz";

            bool thNum = false;
            bool thUpper = false;
            bool thLower = false;




            control4 = FirstNum();






            if (txtPassword.Text != txtPassword2.Text)
            {
                MessageBox.Show("PASSWORD ERROR", "Entered passwords do not match each other !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                control2 = false;
            }



            password = txtPassword2.Text;

            if (password.Length < 8)
            {
                MessageBox.Show("Password too short", "PASSWORD ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (char item in password)
                {
                    if (upper.IndexOf(item) != -1)
                    {
                        thUpper = true;
                    }
                    if (lower.IndexOf(item) != -1)
                    {
                        thLower = true;
                    }
                    if (Number.IndexOf(item) != -1)
                    {
                        thNum = true;
                    }
                }
            }



            txtPassword2.Text = password;









            string FileDirection = @"C:\CalculatorInfo\UsersInfo.txt";

            string[] Lines = System.IO.File.ReadAllLines(FileDirection);

            hold = txtUsername.Text;

            for (int i = 0; i < Lines.Length; i++) 
            {
                hold2 = Lines[i].ToLower();
                if (hold2.Contains(txtUsername.Text.ToLower()))
                {
                    MessageBox.Show("This username already used", "USERNAME ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    i = Lines.Length - 1; //error yazısı 1 kere çıkması için.
                    control3 = false;
                }
            }

            txtUsername.Text = hold;



            string File_Path = @"C:\CalculatorInfo\UsersInfo.txt";

            FileStream fs = new FileStream(File_Path, FileMode.Append, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);

            if (control1 == true && control2 == true && control3 == true && control4 == true && thLower == true && thNum == true && thUpper == true)
            {
                txtUsername.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtUsername.Text);

                sw.WriteLine(txtUsername.Text + ":" + ComputeSha256Hash(txtPassword2.Text));

                sw.Flush();
                sw.Close();
                fs.Close();

                MessageBox.Show("Username and password saved");


                isFileExist(txtUsername.Text);

                lblUsername.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtUsername.Text);


                tabControl1.SelectedTab = tabCalculator;

                FormCmplt = true;
            }
            else
            {
                MessageBox.Show("USERNAME OR PASSWORD IS INCORRECT", "USER NOT REGISTERED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sw.Close();
                fs.Close();
            }








        }








                                                                      // LOGIN PANEL CODES //






        private void txtPasswordLog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 32)
            {
                e.Handled = true;
            }
        }


        private void txtUsernameLog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 32)
            {
                e.Handled = true;
            }
        }


        private void txtUsernameLog_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '£' || e.KeyChar == '½' ||
                e.KeyChar == '€' || e.KeyChar == '₺' ||
                e.KeyChar == '¨' || e.KeyChar == 'æ' ||
                e.KeyChar == 'ß' || e.KeyChar == '´')
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 33 && (int)e.KeyChar <= 47)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 58 && (int)e.KeyChar <= 64)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 91 && (int)e.KeyChar <= 96)
            {
                e.Handled = true;
            }
            if ((int)e.KeyChar >= 123 && (int)e.KeyChar <= 127)
            {
                e.Handled = true;
            }

        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool con1 = false;
            bool con2 = false;

            string filedirection = @"C:\CalculatorInfo\UsersInfo.txt";


            FileStream fs = new FileStream(@"C:\CalculatorInfo\UsersInfo.txt", FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);

            StreamReader rd = new StreamReader(fs);

            string[] Lines = System.IO.File.ReadAllLines(filedirection); //daha önceden aynı kullanıcı adı oluşturulmuşmu kontrol.Ve key insensitive.
            
            foreach (string line in Lines)
            {
                if (line.ToLower().Contains(txtUsernameLog.Text.ToLower()))
                    con1 = true;
                if (line.Contains(ComputeSha256Hash(txtPasswordLog.Text)))
                {
                    con2 = true;
                }
            }
            

            if (con1 == false)
            {
                MessageBox.Show("Please enter correct username", "USERNAME ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (con2 == false)
            {
                MessageBox.Show("Please enter correct password", "PASSWORD ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            if (con1 == true && con2 == true)
            {
                rd.Close();
                fs.Close();

                MessageBox.Show("LOGGED");
                lblUsername.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtUsernameLog.Text);

                isFileExist(txtUsernameLog.Text);

                tabControl1.SelectedTab = tabCalculator;

                FormCmplt = true;



                label1.Text = DateTime.Now.ToLongDateString();

                label2.Text = DateTime.Now.ToLongTimeString();

            }
            else
            {
                rd.Close();
                fs.Close();
            }





        }


        public void isFileExist(string Users) 
        {
            string userDirection = @"C:\CalculatorInfo";
            string userName = Users.ToLower() + ".txt";
            string userPath = Path.Combine(userDirection, userName);

            if (File.Exists(userPath) == false)
            {
                File.Create(userPath).Close();
            }

            StreamReader reader = new StreamReader(userPath);

            string texts = reader.ReadLine();

            while (texts != null)
            {
                listBox.Items.Insert(0, texts);
                texts = reader.ReadLine();

            }
            reader.Close();
        }







                                                                // CALCULATOR PANEL CODES //








        private void NumEvent(object sender, EventArgs e)
        {
            if (txtProcess.Text == "0" || opResult)
            {
                txtProcess.Clear();


                opResult = false;
            }


            Button btn = (Button)sender;

            txtProcess.Text += btn.Text;
        }


        private void opEvent(object sender, EventArgs e)
        {
            opResult = true;

            Button btn = (Button)sender;

            string newOpt = btn.Text;

            txtAnswer.Text = txtAnswer.Text + " " + txtProcess.Text + " " + newOpt;



            switch (op)
            {
                case "+": txtProcess.Text = (result + double.Parse(txtProcess.Text)).ToString(); break; //txt result'a sonucu yazdırma. toplayarak
                case "-": txtProcess.Text = (result - double.Parse(txtProcess.Text)).ToString(); break;
                case "x": txtProcess.Text = (result * double.Parse(txtProcess.Text)).ToString(); break;
                case "÷": txtProcess.Text = (result / double.Parse(txtProcess.Text)).ToString(); break;


            }


            result = double.Parse(txtProcess.Text);
            txtProcess.Text = result.ToString();


            op = newOpt;

            Digit();
        }


        private void Digit() // girilen sayının aralarına nokta koyma
        {

            txtProcess.Text = String.Format("{0:#,0.####}", double.Parse(txtProcess.Text));


        }


        private void btnResult_Click(object sender, EventArgs e)
        {



            if (txtAnswer.Text != "")
            {

                string file_Path = @"C:\CalculatorInfo";

                string User = lblUsername.Text.ToLower() + ".txt";

                string file = Path.Combine(file_Path, User);

                string hold = txtProcess.Text; //geçmiş değeri tutuyor.


                opResult = true;

                switch (op)
                {
                    case "+": txtProcess.Text = (result + double.Parse(txtProcess.Text)).ToString(); break; //txt result'a sonucu yazdırma. toplayarak
                    case "-": txtProcess.Text = (result - double.Parse(txtProcess.Text)).ToString(); break;
                    case "x": txtProcess.Text = (result * double.Parse(txtProcess.Text)).ToString(); break;
                    case "÷": txtProcess.Text = (result / double.Parse(txtProcess.Text)).ToString(); break;


                }


                //if (op != "")
                //{
                    result = double.Parse(txtProcess.Text);

                    string writeResult = txtAnswer.Text + " " + hold + " = " + string.Format("{0:#,0.####}", double.Parse(txtProcess.Text));

                    listBox.Items.Insert(0, writeResult); //listbox'a girilen son değeri en yukarı yazdırma

                    FileStream fs = new FileStream(file, FileMode.Append, FileAccess.Write);

                    StreamWriter sw = new StreamWriter(fs);

                    sw.WriteLine(writeResult);
                    sw.Flush();
                    sw.Close();
                    fs.Close();





                    txtAnswer.Text = "";

                    txtProcess.Text = result.ToString();



                    Digit();

                    op = "";
                //}
            }
        }


        private void btnComma_Click(object sender, EventArgs e)
        {
            if (opResult)
            {
                txtProcess.Text = "0";
            }

            if (txtProcess.Text.Contains(",") == false)
            {
                txtProcess.Text += ",";
            }

            opResult = false;
        }


        private void btnDEL_Click(object sender, EventArgs e)
        {
            if (txtProcess.Text != "")
            {
                txtProcess.Text = txtProcess.Text.Substring(0, txtProcess.Text.Length - 1);

                if (txtProcess.Text == "")
                {
                    txtProcess.Text += "0";
                }
            }
        }


        private void btnC_Click(object sender, EventArgs e)
        {
            txtProcess.Text = "0";

            txtAnswer.Text = "";


            op = "";
            result = 0;
            opResult = false;
        }


        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabMove(); // sekmeler arası geçiş metodu 
        }


        private void TabMove()
        {
            if (lblUsername.Text != "")
            {
                tabControl1.SelectedTab = tabCalculator;
            }

            else
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages[2])
                {

                    tabControl1.Enabled = false;

                    if (FormCmplt)
                    {

                        tabControl1.SelectTab(2);

                    }
                    else
                    {
                        tabControl1.SelectTab(0);

                    }
                    tabControl1.Enabled = true;
                }
            }
        }


        private void btnLogOut_Click(object sender, EventArgs e)
        {

            delete();

            TabMove();

        }


        private void btnDelUser_Click(object sender, EventArgs e)
        {
            string file_path = @"C:\CalculatorInfo";
            string User = lblUsername.Text.ToLower() + ".txt";
            string UserFile = Path.Combine(file_path, User);
            string FilePath = @"C:\CalculatorInfo\UsersInfo.txt";

            DialogResult dialogResult1 = MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult1 == DialogResult.Yes)
            {
                if (File.Exists(UserFile))
                {
                    File.Delete(UserFile);
                }

                string UserName = lblUsername.Text;
                UserName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(UserName);
                File.WriteAllLines(FilePath, File.ReadAllLines(FilePath).Where(s => !s.StartsWith(UserName + ":")));

                delete();
            }

            TabMove();

        }


        private void delete()
        {
            FormCmplt = false;

            txtUsernameLog.Text = "";
            txtPasswordLog.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtPassword2.Text = "";
            lblUsername.Text = "";
            listBox.Items.Clear();
            txtAnswer.Text = "";
            txtProcess.Text = "";
        }


        private void txtProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (FormCmplt == true)
            {


                if (e.KeyCode == Keys.Oemcomma) // Virgül tuşu eğer iszero dan sonra olsaydı 0, bilmemne yapamicaktık.
                {
                    btnComma.PerformClick();
                }

                if (txtProcess.Text == "0" || opResult)
                {
                    txtProcess.Clear();


                    opResult = false;
                }


                if (e.KeyCode == Keys.Enter) // = tuş ataması
                {
                    btnResult.PerformClick();
                    opResult = false;
                }
                if (e.KeyCode == Keys.Back) // CE key
                {
                    btnDEL.PerformClick();
                }
                if (e.KeyCode == Keys.C) // C key
                {
                    btnC.PerformClick();
                }

                if (e.KeyCode == Keys.Divide) //  bölme
                {
                    btnDivide.PerformClick();
                }

                if (e.KeyCode == Keys.Subtract) // çıkartma
                {
                    btnMinus.PerformClick();
                }
                if (e.KeyCode == Keys.Multiply) // çarpma
                {
                    btnMulti.PerformClick();
                }
                if (e.KeyCode == Keys.Add)  // toplama
                {
                    btnSum.PerformClick();
                }
                if (e.KeyCode == Keys.NumPad1) // numara 1 tuş ataması.
                {
                    txtProcess.Text += button1.Text;
                }

                if (e.KeyCode == Keys.NumPad2) // numara 2 tuş ataması.
                {
                    txtProcess.Text += button2.Text;
                }

                if (e.KeyCode == Keys.NumPad3) // numara 3 tuş ataması.
                {
                    txtProcess.Text += button3.Text;
                }

                if (e.KeyCode == Keys.NumPad4) // numara 4 tuş ataması.
                {
                    txtProcess.Text += button6.Text;
                }

                if (e.KeyCode == Keys.NumPad5) // numara 5 tuş ataması.
                {

                    txtProcess.Text += button7.Text;
                }

                if (e.KeyCode == Keys.NumPad6) // numara 6 tuş ataması.
                {

                    txtProcess.Text += button8.Text;
                }

                if (e.KeyCode == Keys.NumPad7) // numara 7 tuş ataması.
                {

                    txtProcess.Text += button11.Text;
                }

                if (e.KeyCode == Keys.NumPad8) // numara 8 tuş ataması.
                {

                    txtProcess.Text += button12.Text;
                }

                if (e.KeyCode == Keys.NumPad9) // numara 9 tuş ataması.
                {

                    txtProcess.Text += button13.Text;
                }

                if (e.KeyCode == Keys.NumPad0) // numara 0 tuş ataması.
                {

                    txtProcess.Text += button17.Text;
                }


            }
        }


        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


    }
}
