using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    class DebugMailService : IMailService
    {
public bool SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Enviando Mail: A: {to}, Asunto: {subject},Mensaje: {body}");
return true;
        }
    }
}
