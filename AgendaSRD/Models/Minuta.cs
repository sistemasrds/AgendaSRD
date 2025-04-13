using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateAgenda.Models;

namespace CorporateAgenda.Models;

public class Minuta
{
    public string Id { get; }
    public List<string> HechosRelevantes { get; }
    public List<Compromiso> CompromisosDerivados { get; }

    public Minuta(string id)
    {
        Id = id;
        HechosRelevantes = new List<string>();
        CompromisosDerivados = new List<Compromiso>();
    }

    public void AgregarHechoRelevante(string hecho)
    {
        if (string.IsNullOrWhiteSpace(hecho))
            throw new ArgumentException("El hecho relevante no puede estar vacío.");

        HechosRelevantes.Add(hecho);
    }

    public void AgregarCompromiso(Compromiso compromiso)
    {
        CompromisosDerivados.Add(compromiso ??
            throw new ArgumentNullException(nameof(compromiso)));
    }
}