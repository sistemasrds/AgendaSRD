using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAgenda.Services;


   public class EmailAdapter : NotificacionService
{
    public void EnviarNotificacion(string mensaje)
    {
        Console.WriteLine($"\n[Email] Notificación enviada: {mensaje}");
    }
}

