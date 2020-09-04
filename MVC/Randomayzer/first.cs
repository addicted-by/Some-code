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
    public partial class first : Form, IObserver
    {
        Model model;
        firstController controller;

        public first(Model model)
        {
            InitializeComponent();
            this.model = model;
            this.model.Register(this);
            this.controller = new firstController(this, model);
        }

        public void UpdateState()
        {
            
                textBox2.Invoke(new Action(()=>textBox2.Text = model.MainNumber.ToString()));

                richTextBox1.Invoke(new Action(() => richTextBox1.Clear()));
                for (int i = 0; i < model.opera.Count; i++)
                    if (i == model.State-1)
                        richTextBox1.Invoke(new Action(() => richTextBox1.Text += "-> " + (i + 1).ToString() + ". " + model.opera[i].ToString() + "\n"));
                    else
                        richTextBox1.Invoke(new Action(() => richTextBox1.Text += (i + 1).ToString() + ". " + model.opera[i].ToString() + "\n"));
               // model.State = 0;
            
        }

        public void UpdateMassiv() {
            richTextBox1.Clear();
            for (int i = 0; i < model.opera.Count; i++)
                richTextBox1.Text += (i+1).ToString()+ ". "+ model.opera[i].ToString() + "\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            controller.Add(textBox1.Text);
            //model.UpdateMassives();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            controller.Add_main(int.Parse(textBox2.Text));
            controller.switchOn();

        }

    }
}
