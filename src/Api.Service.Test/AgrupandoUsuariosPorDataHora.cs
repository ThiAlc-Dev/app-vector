using Api.Domain.Models;
using Data.Entities;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Api.Service.Test
{
    public class AgrupandoUsuariosPorDataHora
    {

        List<Users> lstUsersMock = new List<Users>() {
                    new Users() {
                        createdAt = new DateTime(2021,05,05),
                        name = "Usuario de Teste",
                        avatar = "https://fakeurl/teste.jpg",
                        mail = "teste@hotmail.com",
                        id = "1"
                    },
                    new Users() {
                        createdAt = new DateTime(2021,05,05),
                        name = "Usuario de Teste 2",
                        avatar = "https://fakeurl/teste2.jpg",
                        mail = "teste2@hotmail.com",
                        id = "2"
                    },
                    new Users() {
                        createdAt = new DateTime(2021,05,12),
                        name = "Usuario de Teste 3",
                        avatar = "https://fakeurl/teste2.jpg",
                        mail = "teste3@hotmail.com",
                        id = "3"
                    }
                };

        [Fact]
        public void Agrupando_Usuarios_Por_Data_Hora()
        {
            var userService = new UsersService(null, null, null, null);

            var result = userService.GetQueryAgruped(lstUsersMock).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
