using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaSRD.Exceptions;

public class AgendaException : Exception
{
    public AgendaException(string message) : base(message) { }
    public AgendaException(string message, Exception inner) : base(message, inner) { }
}

public class EventoConflictException : AgendaException
{
    public EventoConflictException(string titulo)
        : base($"Conflicto con el evento '{titulo}'. Ya existe o tiene datos inválidos.") { }
}