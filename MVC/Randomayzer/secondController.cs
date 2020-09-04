using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomayzer
{
    class secondController
    {
        private Model model;
        private second view;

        public secondController(second view, Model model)
        {
            this.model = model;
            this.view = view;
        }

        public void Add(string oper)
        {
            model.Add(oper);
        }

        public void Add_main(int num)
        {
            model.MainNumber = num;
        }

        public void switchOn()
        {
            model.timer.Enabled = true;
        }
    }

  
}
