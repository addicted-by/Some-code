using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Randomayzer
{
    public partial class second : Form, IObserver
    {
        Model model;
        secondController controller;

        public List<Button> buttons = new List<Button>();

        public second(Model model)
        {
            InitializeComponent();
            this.model = model;
            this.model.Register(this);
            this.controller = new secondController(this, model);
        }

        public void UpdateState()
        {
            if (model.flag)
            {
                for (int i = 0; i < buttons.Count(); i++)
                {
                    buttons[i].BackColor = Color.White;
                }
                model.flag = false;
            }
            textBox2.Invoke(new Action(() => textBox2.Text = model.MainNumber.ToString()));

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Name == (model.State - 1).ToString())
                    {
                        buttons[i].BackColor = Color.Green;
                    }
                    //else buttons[i].BackColor = Color.White;
                }
        }

        public void UpdateMassiv() {
            for (int i = 0; i < model.opera.Count; i++)
            {
                Button b = new Button();
                b.Size = new Size(75, 25);
                b.BackColor = Color.White;
                b.Name = i.ToString();
                b.Text = model.opera[i].ToString();
                b.Location = new Point(20 + (i / 14) * 100, 20 + (i % 14) * 30);
                buttons.Add(b);
                Controls.Add(b);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            controller.Add(textBox1.Text);
           // model.UpdateMassives();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controller.Add_main(int.Parse(textBox2.Text));
            try
            {
                for (int i = 0; i < buttons.Count(); i++)
                {
                    buttons[i].BackColor = Color.White;
                }
            }
            catch { return; }
            controller.switchOn();
            ///timer1.Start();
        }

    }
}
