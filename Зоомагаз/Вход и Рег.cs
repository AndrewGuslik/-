using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Зоомагаз
{
    public partial class Вход_и_Рег : Form
    {
        string phone, nameRg, pass, nameCl, m;
        Database db = new Database();
        Form1 catalog = new Form1();

        public Вход_и_Рег()
        {
            InitializeComponent();
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            phone = txBxPhoneSg.Text;
            pass = txBxPassSg.Text;

            if (phone.Length == 11)
            {
                if (pass.Length >= 5)
                {
                    db.openConnection();

                    NpgsqlCommand checkCl = new NpgsqlCommand($"SELECT name_surname FROM clients WHERE phone = '{phone}' AND password = '{pass}'", db.getConnection());

                    NpgsqlDataReader reader = checkCl.ExecuteReader();
                    //if (checkCl != null)
                    //{
                    //    nameCl = Convert.ToString(checkCl);
                    //    label1.Text = nameCl;
                    //}
                    if (reader.Read())
                    {
                        nameCl = reader.GetString(0);
                        this.Hide();
                        catalog.signedCl(nameCl);
                        catalog.Show();
                    }
                    else { MessageBox.Show("Такого пользователя нет"); }

                    db.closeConnection();
                }
                else { MessageBox.Show("Введите пароль больше 5 символов"); }
            }
            else { MessageBox.Show("Введите номер равный 11 символов"); }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            nameRg = txBxNameRg.Text;
            phone = txBxPhoneRg.Text;
            pass = txBxPassRg.Text;
            string res = checkCl(phone);
            if (nameRg != null)
            {
                if (phone.Length == 11)
                {
                    if (pass.Length >= 5)
                    {
                        if (res == "Нету")
                        {
                            db.openConnection();
                            NpgsqlCommand addCl = new NpgsqlCommand($"INSERT INTO clients (name_surname, phone, password) VALUES ('{nameRg}', '{phone}', '{pass}')", db.getConnection());
                            addCl.ExecuteNonQuery();
                            db.closeConnection();

                            this.Hide();
                            catalog.signedCl(nameRg);
                            catalog.Show();
                        }
                        else if (res == "Есть")
                        {
                            db.closeConnection();
                            MessageBox.Show("Такой пользователь уже зарегистрирован");
                        }                      
                    }
                    else { MessageBox.Show("Введите пароль больше 5 символов"); }
                }
                else { MessageBox.Show("Введите номер равный 11 символов"); }
            }
            else { MessageBox.Show("Введите ваше имя"); }
        }

        public string checkCl(string phone)
        {
            db.openConnection();

            NpgsqlCommand checkCl = new NpgsqlCommand($"SELECT name_surname FROM clients WHERE phone = '{phone}'", db.getConnection());

            NpgsqlDataReader reader = checkCl.ExecuteReader();

            if (reader.Read())
            {
                db.closeConnection();
                return m = "Есть";
            }
            else {
                db.closeConnection();
                return m = "Нету";               
            }
        }
    }
}
