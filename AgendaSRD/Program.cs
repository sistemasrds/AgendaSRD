using AgendaSRD.Models;
using AgendaSRD.Services;
using System.ComponentModel.DataAnnotations;

class Program
{
    static List<Evento> eventos = new List<Evento>();
    static List<Empleado> empleados = new List<Empleado>();

    static void Main()
    {
        try
        {
            InitializeData();
            RunMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError crítico: {ex.Message}");
            Console.WriteLine("Detalle (para depuración):");
            Console.WriteLine(ex.StackTrace);
        }
        finally
        {
            Console.WriteLine("\nLa aplicación ha finalizado.");
        }
    }

    static void InitializeData()
    {
        try
        {
            empleados.AddRange(new[]
            {
                new Empleado("E-001", "Juan Pérez", "juan@empresa.com", "TI", "Desarrollo"),
                new Empleado("E-002", "Ana Gómez", "ana@empresa.com", "RH", "Reclutamiento")
            });
        }
        catch (Exception ex)
        {
            throw new Exception("Error al inicializar datos: " + ex.Message, ex);
        }
    }

    static void RunMenu()
    {
        var notificacionService = new EmailAdapter(); // Default
        var busquedaService = new BusquedaService(empleados);

        while (true)
        {
            try
            {
                Console.WriteLine("\n--- Agenda Corporativa ---");
                Console.WriteLine("1. Crear Evento");
                Console.WriteLine("2. Listar Eventos");
                Console.WriteLine("3. Buscar Empleados por Área");
                Console.WriteLine("4. Cambiar Método de Notificación (Email/SMS)");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                var opcion = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(opcion))
                    throw new ValidationException("Opción no puede estar vacía.");

                switch (opcion)
                {
                    case "1":
                        CrearEvento(notificacionService);
                        break;
                    case "2":
                        ListarEventos();
                        break;
                    case "3":
                        BuscarEmpleadosPorArea(busquedaService);
                        break;
                    case "4":
                        notificacionService = (EmailAdapter)CambiarMetodoNotificacion();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Intente nuevamente.");
                        break;
                }
            }
            catch (ValidationException vex)
            {
                Console.WriteLine($"\nError de validación: {vex.Message}");
            }
            catch (InvalidOperationException ioex)
            {
                Console.WriteLine($"\nError de operación: {ioex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError inesperado: {ex.Message}");
            }
        }
    }

    static NotificacionService CambiarMetodoNotificacion()
    {
        Console.Write("¿Usar Email (1) o SMS (2)?: ");
        var opcion = Console.ReadLine();
        return opcion switch
        {
            "1" => new EmailAdapter(),
            "2" => new SMSAdapter(),
            _ => throw new ValidationException("Opción no válida. Se usará Email por defecto.")
        };
    }

    static void CrearEvento(NotificacionService notificacionService)
    {
        try
        {
            Console.Write("Título del Evento: ");
            var titulo = Console.ReadLine() ?? throw new ValidationException("Título es requerido.");

            Console.Write("Días a futuro para el evento (ej: 1): ");
            if (!int.TryParse(Console.ReadLine(), out int dias) || dias < 0)
                throw new ValidationException("Días debe ser un número positivo.");

            var evento = new Evento(
                Guid.NewGuid().ToString(),
                titulo,
                DateTime.Now.AddDays(dias),
                60,
                "Sala Reuniones"
            );

            if (eventos.Any(e => e.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Ya existe un evento con ese título.");

            // Simular invitación a empleados
            foreach (var emp in empleados)
            {
                evento.AgregarParticipante(emp);
                notificacionService.EnviarNotificacion($"Invitación a: {titulo}. Fecha: {evento.FechaHora}");
            }

            evento.Minuta = new Minuta(Guid.NewGuid().ToString());
            evento.Minuta.AgregarHechoRelevante("Reunión inicial de planificación.");
            evento.Compromisos.Add(new Compromiso("C-" + DateTime.Now.Ticks, "Entregar informe", DateTime.Now.AddDays(7)));

            eventos.Add(evento);
            Console.WriteLine($"Evento '{titulo}' creado exitosamente.");
        }
        catch (Exception ex) when (ex is ValidationException || ex is InvalidOperationException)
        {
            throw; // Relanza excepciones conocidas
        }
        catch (Exception ex)
        {
            throw new Exception("Error al crear evento: " + ex.Message, ex);
        }
    }

    static void ListarEventos()
    {
        if (!eventos.Any())
            throw new InvalidOperationException("No hay eventos registrados.");

        Console.WriteLine("\n--- Eventos Registrados ---");
        foreach (var ev in eventos)
        {
            Console.WriteLine($"\nEvento: {ev.Titulo}");
            Console.WriteLine($"Fecha: {ev.FechaHora}, Duración: {ev.Duracion} mins");
            Console.WriteLine($"Participantes: {ev.Participantes?.Count ?? 0}");
            Console.WriteLine($"Compromisos: {ev.Compromisos?.Count ?? 0}");
        }
    }

    static void BuscarEmpleadosPorArea(BusquedaService busquedaService)
    {
        Console.Write("Ingrese el área (ej: TI, RH): ");
        var area = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(area))
            throw new ValidationException("Área no puede estar vacía.");

        var resultados = busquedaService.BuscarEmpleadosPorArea(area);
        if (!resultados.Any())
            throw new InvalidOperationException($"No se encontraron empleados en el área '{area}'.");

        Console.WriteLine($"\nEmpleados en {area}:");
        foreach (var emp in resultados) Console.WriteLine($"- {emp.Nombre} ({emp.Area})");
    }
}