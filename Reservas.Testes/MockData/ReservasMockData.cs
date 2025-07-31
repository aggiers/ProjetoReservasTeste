using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservas.Api.Models;

namespace Reservas.Testes.MockData
{
    internal class ReservasMockData
    {
        public static List<Reserva> GetReservas()
        {
            return new List<Reserva>()
            {
                new Reserva(){ReservaId = 1, Nome = "Agatha", InicioLocacao = "São Paulo", FimLocacao = "Canadá"},
                new Reserva(){ReservaId = 2, Nome = "Layza", InicioLocacao = "Guarulhos", FimLocacao = "Espanha"},
                new Reserva(){ReservaId = 3, Nome = "Jean", InicioLocacao = "Rio de Janeiro", FimLocacao = "Paris"},
            };
        }
    }
}
