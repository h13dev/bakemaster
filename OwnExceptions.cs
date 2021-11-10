using System;
using System.Collections.Generic;
using System.Text;

namespace Tritronix.BakingOven
{
    class OwnExceptions : Exception
    {
           public OwnExceptions() 
           { 
           }

           public OwnExceptions(string message) : base(message) 
           { 
           } 
    }
}
