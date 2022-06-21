using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallGuyIClown_InterfacePractice
{
    internal interface IClown
    {
        string FunnyThingIHave { get; }

        void Honk() { /* to be overwritten */ }
    }
}
