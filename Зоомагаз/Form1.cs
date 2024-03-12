namespace Зоомагаз
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Database db = new Database(); 
            InitializeComponent();

            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Controls.AddRange(db.addCateg().ToArray());

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();  //Скрывает окно при переходе
            Вход_и_Рег sigReg = new Вход_и_Рег();
            sigReg.Show();           
        }

        public void signedCl(string nameCl)
        {
            label3.Text = "Здравствуйте, " + "\n" + nameCl + "!";
        }
    }
}