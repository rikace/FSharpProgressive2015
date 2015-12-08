using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSharpWorldInterfaces
{
    public interface ICalculator : Orleans.IGrainWithIntegerKey
    {
        Task<int> Add(int value);
        Task<int> Subtract(int value);
        Task<int> Multiply(int value);
    }
}
