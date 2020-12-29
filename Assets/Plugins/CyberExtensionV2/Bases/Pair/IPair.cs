using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver
{
    public interface IPair<out T1,out T2>
    {
        T1 First { get; }
        T2 Second { get; }

    }
}
