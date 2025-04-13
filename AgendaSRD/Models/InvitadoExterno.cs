using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSRD.Models;

public class InvitadoExterno : Participante
{
    public string Empresa { get; }
    public string MotivoInvitacion { get; set; }

    public InvitadoExterno(string id, string nombre, string contacto, string empresa)
        : base(id, nombre, contacto)
    {
        Empresa = empresa ?? throw new ArgumentNullException(nameof(empresa));
    }

    public override void ConfirmarAsistencia()
    {
        Console.WriteLine($"[Invitado] {Nombre} ({Empresa}) confirmó asistencia");
    }

    public override void RechazarInvitacion(string razon)
    {
        if (string.IsNullOrWhiteSpace(razon))
            throw new ArgumentException("La razón es requerida.");

        Console.WriteLine($"[Invitado] {Nombre} rechazó invitación. Razón: {razon}");
    }
}
