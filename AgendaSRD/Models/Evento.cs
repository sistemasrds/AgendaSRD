using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgendaSRD.Models;
public class Evento
{
    public string Id { get; }
    public string Titulo { get; set; }
    public DateTime FechaHora { get; set; }
    public int Duracion { get; set; }
    public string Ubicacion { get; set; }
    public Minuta Minuta { get; set; }
    public List<Compromiso> Compromisos { get; } = new List<Compromiso>();
    public List<Tarea> Tareas { get; } = new List<Tarea>();
    public List<Participante> Participantes { get; } = new List<Participante>();

    public Evento(string id, string titulo, DateTime fechaHora, int duracion, string ubicacion)
    {
        Id = id;
        Titulo = titulo;
        FechaHora = fechaHora;
        Duracion = duracion;
        Ubicacion = ubicacion;
    }

    public void AgregarParticipante(Participante participante)
    {
        Participantes.Add(participante);
    }
}
