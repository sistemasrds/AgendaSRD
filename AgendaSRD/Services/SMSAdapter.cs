using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAgenda.Services;

namespace CorporateAgenda.Services
{
    public class SMSAdapter : NotificacionService
    {
        public void EnviarNotificacion(string mensaje)
        {
            Console.WriteLine($"\n[SMS] Notificación enviada: {mensaje}");
        }
    }
}
