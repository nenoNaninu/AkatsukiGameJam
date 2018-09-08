using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Neno.Scripts
{
    interface IEnemy
    {
        void Explode();

        int Id { get; set; }
    }
}
