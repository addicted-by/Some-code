using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomayzer
{
    public interface IObserver
    {
        void UpdateState();
        void UpdateMassiv();
    }
}
