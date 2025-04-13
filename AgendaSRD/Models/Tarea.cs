using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAgenda.Models;

public class Tarea
{
    public string Id { get; }
    public string Descripcion { get; }
    public string Entregable { get; }
    public bool Completada { get; private set; }
    public Participante? Asignado { get; private set; }

    public Tarea(string id, string descripcion, string entregable)
    {
        Id = id;
        Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
        Entregable = entregable ?? throw new ArgumentNullException(nameof(entregable));
    }

    public void AsignarParticipante(Participante participante)
    {
        Asignado = participante ?? throw new ArgumentNullException(nameof(participante));
    }

    public void MarcarComoCompletada()
    {
        if (Asignado == null)
            throw new InvalidOperationException("No se puede completar una tarea sin asignar.");

        Completada = true;
    }
}