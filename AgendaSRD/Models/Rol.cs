using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAgenda.Models;

public class Rol
{
    public string Nombre { get; }
    public List<string> Permisos { get; }

    public Rol(string nombre)
    {
        Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
        Permisos = new List<string>();
    }

    public void AgregarPermiso(string permiso)
    {
        if (string.IsNullOrWhiteSpace(permiso))
            throw new ArgumentException("El permiso no puede estar vacío.");

        Permisos.Add(permiso);
    }
}
