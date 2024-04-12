namespace DDoS_Program
{
    public partial class Form1 : Form
    {
        private readonly Logik _logik;
        public Form1(Logik logik)
        {
            InitializeComponent();
            OnStart();
            _logik = logik;

            _logik.Form = this;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
            }
            else { button1.Enabled = false; };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Clear();

            string url = textBox1.Text;
            int numberOfRequest = int.Parse(textBox2.Text);

            _logik.Execute(url, numberOfRequest);
        }

        private void OnStart()
        {
            button1.Enabled = false;
        }
        public void SuccessfulOutPut(string output)
        {
            textBox3.Text += output;
        }
        public void FailOutPut(string output)
        {
            textBox3.Text += output;
        }

    }
}
