using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Randomayzer
{
    static class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model();

            first view1 = new first(model);
            second view2 = new second(model);
            third view3 = new third(model);

            view2.Show();
            view3.Show();

            Application.Run(view1);
        }
    }
}
