using Api.Data.Context;
using Data.Entities;
using Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Api.Data.Test.BaseTest;

namespace Api.Data.Test
{
    public class CriarUsuarioApiExterna : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public CriarUsuarioApiExterna(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;

        }

        [Fact]
        public async Task E_Possivel_Gravar_Request_Api_Externa()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserRepository _repository = new UserRepository(context);
                List<UserEntity> _entity = new List<UserEntity>() {
                    new UserEntity() {
                        createDbdAt = DateTime.Now,
                        createdAt = DateTime.UtcNow,
                        name = "Usuario de Teste",
                        avatar = "https://fakeurl/teste.jpg",
                        mail = "teste@hotmail.com",
                        id = 1
                    },
                    new UserEntity() {
                        createDbdAt = DateTime.UtcNow,
                        createdAt = DateTime.UtcNow,
                        name = "Usuario de Teste 2",
                        avatar = "https://fakeurl/teste2.jpg",
                        mail = "teste2@hotmail.com",
                        id = 2
                    }
                };

                var _registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
            }
        }
    }
}
