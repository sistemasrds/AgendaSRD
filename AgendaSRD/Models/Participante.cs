using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaSRD.Models;

namespace AgendaSRD.Models;
public abstract class Participante
{
    public string Id { get; }
    public string Nombre { get; set; }
    public string Contacto { get; set; }
    public Rol Rol { get; set; }

    protected Participante(string id, string nombre, string contacto)
    {
        Id = id;
        Nombre = nombre;
        Contacto = contacto;
    }

    public abstract void ConfirmarAsistencia();
    public abstract void RechazarInvitacion(string razon);
}


