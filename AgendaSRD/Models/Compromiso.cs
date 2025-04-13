using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSRD.Models;

public class Compromiso
{
    public string Id { get; }
    public string Descripcion { get; }
    public DateTime FechaLimite { get; }
    public bool Cumplido { get; private set; }
    public List<Participante> Responsables { get; }

    public Compromiso(string id, string descripcion, DateTime fechaLimite)
    {
        Id = id;
        Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
        FechaLimite = fechaLimite;
        Responsables = new List<Participante>();
    }

    public void AgregarResponsable(Participante participante)
    {
        Responsables.Add(participante ??
            throw new ArgumentNullException(nameof(participante)));
    }

    public void MarcarComoCumplido()
    {
        if (!Responsables.Any())
            throw new InvalidOperationException("No hay responsables asignados.");

        Cumplido = true;
    }
}