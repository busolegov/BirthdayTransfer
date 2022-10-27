
using BirthdayTransfer_dll;

namespace BirthdayTransfer
{
    public partial class Form1 : Form
    {
        VkWorker vkWorker;
        public Form1()
        {
            InitializeComponent();
        }

        public void VkLoginProcess(string vkLogin, string vkPass) 
        {
            vkWorker = new VkWorker(vkLogin, vkPass);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        internal void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        internal void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "") //TODO dont work
            {
                vkWorker = new VkWorker(textBox1.Text, textBox2.Text);
                try
                {
                    vkWorker.VkAuthorize();
                    if (vkWorker.Api.IsAuthorized == true)
                    {
                        string caption = "Успех";
                        string mes = "Авторизация ВКонтакте прошла успешно";
                        MessageBox.Show(mes, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox3.ReadOnly = false;
                        textBox5.ReadOnly = false;
                        textBox6.ReadOnly = false;
                        button2.Enabled = true;
                    }

                    textBox4.Text = $"Вы авторизовались под ID = {vkWorker.Api.UserId}";

                    vkWorker.GetVkFriends();

                    foreach (var item in vkWorker.VkUsers)
                    {
                        checkedListBox1.Items.Add($"{item.FirstName} {item.LastName} - {item.BirthDate:M}");
                    }
                }
                catch (Exception)
                {
                    //if (ex is CaptchaNeededException)
                    //{
                    //    using (HttpClient client = new HttpClient())
                    //    {
                    //        var image = Bitmap.FromStream(await client.GetStreamAsync();
                    //    }
                    //}
                    //throw;
                }
            }
            else
            {
                string caption = "Ошибка!";
                string mes = "Не все поля заполнены!";
                MessageBox.Show(mes, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BirthdayWorker birthdayWorker;
            var checkedUsers = new List<VkUser>();
            foreach (var item in checkedListBox1.CheckedIndices)
            {
                var index = int.Parse(item.ToString());
                checkedUsers.Add(vkWorker.VkUsers[index]);
            }

            if (textBox5.Text != "" && textBox3.Text != "" && textBox6.Text != "")
            {
                birthdayWorker = new BirthdayWorker(checkedUsers, textBox5.Text.ToString(), textBox3.Text.ToString(), textBox6.Text.ToString());

                Task importTask = Task.Run(() => birthdayWorker.PushEventToCalendarAsync());

                importTask.GetAwaiter().OnCompleted(() =>
                {
                    MessageBox.Show("Импорт успешно завершен!","Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    foreach (TextBox tb in this.Controls.OfType<TextBox>().ToArray())
                    {
                        tb.Clear();
                    }
                    checkedListBox1.ResetText();
                });
            }
            else
            {
                string caption = "Ошибка!";
                string mes = "Не все поля заполнены!";
                MessageBox.Show(mes, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            checkedListBox1.ClearSelected();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            if (!checkBox1.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
        }
    }
}