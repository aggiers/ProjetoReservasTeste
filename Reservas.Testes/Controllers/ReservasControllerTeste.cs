using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Reservas.Api.Controllers;
using Reservas.Api.Interfaces;
using Reservas.Api.Models;
using Reservas.Testes.MockData;

namespace Reservas.Testes.Controllers
{
    public class ReservasControllerTeste
    {
        [Fact]
        public void GetTodasReservas_DeveRetornar200Status()
        {
            // Arrange - Organizar
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.Reservas).Returns(ReservasMockData.GetReservas());
            var sut = new ReservasController(reservaService.Object);

            // Act - Agir
            var result = (OkObjectResult)sut.Get();

            // Assert - Afirmar
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetReservaPorId_DeveRetornar200_QuandoEncontrar()
        {
            // arrange - organizar
            var reservaMock = ReservasMockData.GetReservas()[0];
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[reservaMock.ReservaId]).Returns(reservaMock);
            var sut = new ReservasController(reservaService.Object);

            // act - agir
            var result = sut.Get(reservaMock.ReservaId);

            // assert - afirmar
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetReservaPorId_DeveRetornar400_QuandoNaoEncontrar()
        {
            // arrange
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[999]).Returns((Reserva)null);
            var sut = new ReservasController(reservaService.Object);

            // act
            var result = sut.Get(999);

            // assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void PostReserva_DeveRetornar201Status()
        {
            // arrange
            var novaReserva = new Reserva { Nome = "Teste" };
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.AddReserva(It.IsAny<Reserva>())).Returns(novaReserva);
            var sut = new ReservasController (reservaService.Object);

            // act
            var result = sut.Post(novaReserva);

            // assert
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(201);
        }

        [Fact]
        public void PutReserva_DeveRetornar200_QuandoAtualizar()
        {
            // arrange
            var reservaAtualizada = new Reserva { ReservaId = 1, Nome = "Atualizada" };
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.UpdateReserva(It.IsAny<Reserva>())).Returns(reservaAtualizada);
            var sut = new ReservasController(reservaService.Object);

            // act
            var result = sut.Put(reservaAtualizada);

            // assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
        }

        // pacth = como se fosse um Put, mas ele modifica informações específicas
        [Fact]
        public void PatchReserva_DeveRetornar200_QuandoEcontrar()
        {
            // arrange
            var reservaOriginal = new Reserva { ReservaId = 1, Nome = "Original" };
            var patch = new JsonPatchDocument<Reserva>();
            patch.Replace(r => r.Nome, "Atualizado");
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[1]).Returns(reservaOriginal);
            var sut = new ReservasController(reservaService.Object);

            // act
            var result = sut.Patch(1, patch);

            // assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void PatchReserva_DeveRetornar404_QuandoNaoEncontrar()
        {
            // arrange
            var patch = new JsonPatchDocument<Reserva>();
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[999]).Returns((Reserva)null);
            var sut = new ReservasController (reservaService.Object);

            // act
            var result = sut.Patch(999, patch);

            // assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteReserva_DevveRetornar204()
        {
            // arrange 
            var reservaService = new Mock<IReservaRepository>();
            var sut = new ReservasController(reservaService.Object);

            // act
            var result = sut.Delete(1);

            // assert
            result.Should().BeOfType<NoContentResult>();
        }

    }
}
