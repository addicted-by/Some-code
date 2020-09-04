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
    public partial class third : Form, IObserver
    {
        Model model;
        thirdController controller;
        

        public third(Model model)
        {
            InitializeComponent();
            this.model = model;
            this.model.Register(this);
            this.controller = new thirdController(this, model);
        }

        public void UpdateState()
        {
            
                textBox2.Invoke(new Action(() => textBox2.Text = model.MainNumber.ToString()));
        }

        public void UpdateMassiv() {}

        private void button1_Click(object sender, EventArgs e)
        {
            controller.Add(textBox1.Text);
           // model.UpdateMassives();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controller.Add_main(int.Parse(textBox2.Text));
            //timer1.Start();
            controller.switchOn();
        }
    }
}
