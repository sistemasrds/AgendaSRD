using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAgenda.Models;

namespace CorporateAgenda.Services
    {
        public class BusquedaService
        {
            private readonly List<Empleado> _empleados;
            private readonly List<Evento> _eventos;

            public BusquedaService(List<Empleado> empleados, List<Evento> eventos)
            {
                _empleados = empleados ?? throw new ArgumentNullException(nameof(empleados));
                _eventos = eventos ?? throw new ArgumentNullException(nameof(eventos));
            }

        public BusquedaService(List<Empleado> empleados)
        {
            _empleados = empleados;
        }

        public List<Empleado> BuscarEmpleadosPorArea(string area)
            {
                if (string.IsNullOrWhiteSpace(area))
                    throw new ArgumentException("El área de búsqueda no puede estar vacía.");

                return _empleados
                    .Where(e => e.Area.Equals(area, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            public List<Evento> BuscarEventosPorParticipante(string participanteId)
            {
                return _eventos
                    .Where(e => e.Participantes.Any(p => p.Id == participanteId))
                    .ToList();
            }

            public List<Compromiso> BuscarCompromisosPendientes(string empleadoId)
            {
                return _eventos
                    .SelectMany(e => e.Minuta?.CompromisosDerivados ?? Enumerable.Empty<Compromiso>())
                    .Where(c => !c.Cumplido && c.Responsables.Any(r => r.Id == empleadoId))
                    .ToList();
            }
        }
    }
