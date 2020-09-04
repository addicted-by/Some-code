using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Timers;
namespace Randomayzer
{
    public class Model
    {
        public List<string> opera = new List<string>();
        private ArrayList listeners = new ArrayList();
        public int main_number;
        public int state = 0;
        public bool flag = false;
        public Timer timer;

        public Model()
        {
            timer = new Timer();
            timer.AutoReset = true;
            timer.Enabled = false;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Interval = 1000;
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Operate();
            if (state >= opera.Count)
            {
                timer.Enabled = false;
                state = -1;
                flag = true;
            }
            //flag = false;
            state++;
        }

        public int State
        {
            get { return state; }
            set { state = value; }
        }

        public void Add(string oper) {
            opera.Add(oper);
            UpdateMassives();
        }

        public int MainNumber {
            get { return main_number; }
            set { main_number = value; }
        }

        public void Operate() {
           
            try
            {
                char operation = (this.opera[this.State - 1])[0];
                int operand = int.Parse((this.opera[this.State - 1]).Remove(0, 1));

                switch (operation)
                {
                    case '+':
                        {
                            MainNumber += operand;
                            break;
                        }
                    case '-':
                        {
                            MainNumber -= operand;
                            break;
                        }
                    case '*':
                        {
                            MainNumber *= operand;
                            break;
                        }
                    case '/':
                        {
                            MainNumber /= operand;
                            break;
                        }
                }
            }
            catch { return; }
            //this.State += 1;
            UpdateObservers();
        }

        public void Register(IObserver o)
        {
            listeners.Add(o);

        }
        public void Deregister(IObserver o)
        { listeners.Remove(o); }

        public void UpdateObservers(){
            foreach (IObserver ob in listeners) ob.UpdateState();
        }

        public void UpdateMassives() {
            foreach (IObserver ob in listeners) ob.UpdateMassiv();
        }
    }
}
