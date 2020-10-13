using System;
using System.Threading.Tasks;
using AutoMapper;
using Messaging.Api.Helpers;
using Messaging.Api.Models.Settings;
using Messaging.Core.Dto.Authentication;
using Messaging.Data;
using Messaging.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;

namespace Messaging.Service.Tests.Authentication
{
    public class AuthenticationServiceTests
    {
        private DbContextOptions<MessagingDbContext> ContextOptions { get; }

        public AuthenticationServiceTests()
        {
            ContextOptions = new DbContextOptionsBuilder<MessagingDbContext>()
                    .UseInMemoryDatabase("MessagingDB")
                    .Options;

            Seed();
        }

        private void Seed()
        {
            using (var context = new MessagingDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var user1 = new User
                {
                    ID = 1453,
                    Username = "fatih",
                    Password = "1234567890",
                    Email = "fatih@gmail.com"
                };

                var user2 = new User
                {
                    ID = 1789,
                    Username = "berger",
                    Password = "1234567890",
                    Email = "berger@gmail.com"
                };
                context.Users.Add(user1);
                context.Users.Add(user2);

                context.SaveChanges();
            }
        }

        #region Login

        [Fact]
        public async Task ShouldReturnSuccess_WhenUserExists_LoginAsync()
        {
            // arrange
            using (var dbcontext = new MessagingDbContext(ContextOptions))
            {
                var authenticationService = new AuthenticationService(GetSettings(), GetMapper(), dbcontext);
                var username = "fatih";
                var password = "1234567890";

                // act
                var result = await authenticationService.LoginAsync(new LoginRequestDto
                {
                    Username = username,
                    Password = password
                });

                // assert
                Assert.True(result.IsSuccess);
                Assert.NotEmpty(result.Token);
            }
        }

        [Fact]
        public async Task ShouldReturnFailure_WhenUserNotExists_LoginAsync()
        {
            // arrange
            using (var dbcontext = new MessagingDbContext(ContextOptions))
            {
                var authenticationService = new AuthenticationService(GetSettings(), GetMapper(), dbcontext);
                var username = "osman";
                var password = "1234567890";

                // act
                var result = await authenticationService.LoginAsync(new LoginRequestDto
                {
                    Username = username,
                    Password = password
                });

                // assert
                Assert.False(result.IsSuccess);
                Assert.Null(result.Token);
                Assert.NotEmpty(result.ErrorMessages);
            }
        }


        #endregion

        #region Register

        [Fact]
        public async Task ShouldReturnFailure_WhenUsernameAlreadyExists_RegisterAsync()
        {
            // arrange
            using (var dbcontext = new MessagingDbContext(ContextOptions))
            {
                var authenticationService = new AuthenticationService(GetSettings(), GetMapper(), dbcontext);
                var username = "fatih";
                var password = "1234567890";
                var email = "mytest@gmail.com";

                // act
                var result = await authenticationService.RegisterAsync(new RegisterRequestDto
                {
                    Username = username,
                    Password = password,
                    Email = email
                });

                // assert
                Assert.False(result.IsSuccess);
                Assert.NotEmpty(result.ErrorMessages);
            }
        }

        [Fact]
        public async Task ShouldReturnSuccess_WhenUsernameDoesntExist_RegisterAsync()
        {
            // arrange
            using (var dbcontext = new MessagingDbContext(ContextOptions))
            {
                var authenticationService = new AuthenticationService(GetSettings(), GetMapper(), dbcontext);
                var username = "leonardo";
                var password = "1234567890";
                var email = "leonardo@gmail.com";

                // act
                var result = await authenticationService.RegisterAsync(new RegisterRequestDto
                {
                    Username = username,
                    Password = password,
                    Email = email
                });

                // assert
                Assert.True(result.IsSuccess);
                Assert.Empty(result.ErrorMessages);
                Assert.NotEmpty(result.Token);
                Assert.NotEqual(0, result.ID);
            }
        }

        #endregion 

        #region Helpers

        private IMapper GetMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperContainer());
            });
            var mapper = mockMapper.CreateMapper();

            return mapper;
        }

        private IOptions<AuthenticationSettings> GetSettings()
        {
            IOptions<AuthenticationSettings> authenticationSettings = Options.Create<AuthenticationSettings>(
                new AuthenticationSettings
                {
                    Secret = "7AA7FA4F13D6C736759DFBA9ABE09D592391CF31777870D72FE553E8040359D9"
                }
                );

            return authenticationSettings;
        }

        #endregion
    }
}
