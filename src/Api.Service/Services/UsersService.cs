using Api.Domain.Models;
using Domain.Interfaces;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using System;
using Domain.DTOs;
using Domain.Interfaces.Repository;
using Data.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Service.Services
{
    public class UsersService : IUserService
    {
        private IRepository<UserEntity> _repository { get; set; }

        private readonly IHttpClientFactory _clientFactory;

        private readonly IMapper _mapper;
        private readonly ILogger<UsersService> _logger;
        private string restUrl = "https://6064ac2bf09197001778660d.mockapi.io/api/test-api";

        public UsersService(IHttpClientFactory clientFactory,
            IMapper mapper, IRepository<UserEntity> repository, ILogger<UsersService> logger)
        {
            _clientFactory = clientFactory;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<UserGroupDto>> Get()
        {
            var users = new List<Users>();
            var usersGroup = new List<UserGroupDto>();
            var dateStrNow = DateTime.Now.ToShortDateString();

            var resultDb = await _repository.SelectAllAsync();

            if (resultDb.Count() == 0)
            {
                users = await GetUsersAsync(users);
            }
            else
            {
                if (resultDb.Where(a => a.createDbdAt.ToShortDateString().Equals(dateStrNow)).Count() > 0)
                    users = _mapper.Map<IEnumerable<Users>>(resultDb).ToList();
                else
                    users = await GetUsersAsync(users);
            }

            var query = from grp in users
                        group grp by grp.createdAt.ToShortDateString() into nGroup
                        orderby nGroup.Key descending
                        select nGroup;


            foreach (var item in query)
            {
                var lstNames = new List<string>();
                usersGroup.Add(new UserGroupDto { data = item.Key });

                foreach (var user in users)
                {
                    if (item.Key.Equals(user.createdAt.ToShortDateString()))
                    {
                        lstNames.Add(user.name);
                    }
                }

                usersGroup.Find(a => a.data.Equals(item.Key)).nomes = lstNames;
            }


            return usersGroup;
        }

        protected async Task<List<Users>> GetUsersAsync(List<Users> users)
        {
            users.AddRange(await GetHttpAsync());

            var model = _mapper.Map<List<UserEntity>>(users);
            var resultEntity = await _repository.InsertAsync(model);

            return _mapper.Map<List<Users>>(resultEntity);
        }

        public async Task<List<Users>> GetHttpAsync()
        {;
            var request = new HttpRequestMessage(HttpMethod.Get, restUrl);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<List<Users>>(responseStream);
                return result;
            }
            else
            {
                _logger.LogWarning($"A chamada retornou uma resposta inesperada. StatusCode: {response.IsSuccessStatusCode}");
                return new List<Users>();
            }
        }
    }
}
