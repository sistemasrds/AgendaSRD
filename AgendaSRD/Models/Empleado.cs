using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSRD.Models;

public class Empleado : Participante
{
    public string Area { get; }
    public string Subarea { get; }
    public Rol Rol { get; set; }

    public Empleado(string id, string nombre, string contacto, string area, string subarea)
        : base(id, nombre, contacto)
    {
        Area = area ?? throw new ArgumentNullException(nameof(area));
        Subarea = subarea ?? throw new ArgumentNullException(nameof(subarea));
    }

    public override void ConfirmarAsistencia()
    {
        Console.WriteLine($"[Empleado] {Nombre} confirmó asistencia (Área: {Area})");
    }

    public override void RechazarInvitacion(string razon)
    {
        if (string.IsNullOrWhiteSpace(razon))
            throw new ArgumentException("La razón es requerida.");

        Console.WriteLine($"[Empleado] {Nombre} rechazó invitación. Razón: {razon}");
    }
}