using System.Threading.Tasks;
using AutoMapper;
using Messaging.Api.Helpers;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Constants;
using Messaging.Core.Dto.Messaging;
using Messaging.Data;
using Messaging.Data.Entities;
using Messaging.Service.Messaging;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Messaging.Service.Tests.Messaging
{
    public class MessagingServiceTests
    {
        private DbContextOptions<MessagingDbContext> ContextOptions { get; }

        public MessagingServiceTests()
        {
            ContextOptions = new DbContextOptionsBuilder<MessagingDbContext>()
                    .UseInMemoryDatabase("MessagingServiceDB")
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

                var user3 = new User
                {
                    ID = 1923,
                    Username = "ismet",
                    Password = "1234567890",
                    Email = "ismet@gmail.com"
                };

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);

                var block1 = new Block
                {
                    FKBlockerUserId = user1.ID,
                    FKBlockedUserId = user3.ID,
                    IsActive = true
                };
                var block2 = new Block
                {
                    FKBlockerUserId = user1.ID,
                    FKBlockedUserId = user2.ID,
                    IsActive = false
                };

                context.Blocks.Add(block1);
                context.Blocks.Add(block2);

                context.SaveChanges();
            }
        }

        #region SendMessage

        [Fact]
        public async Task ShoudReturnSuccess_WhenThereIsNoAnyBlock_SendMessage()
        {
            // assert
            using (var dbContext = new MessagingDbContext(ContextOptions))
            {
                var senderId = 1453;
                var receiverId = 1789;

                var mockUserService = new Mock<IUserService>();
                mockUserService.Setup(us => us.IsExists(It.IsAny<int>()))
                    .Returns(Task.FromResult(true));

                var mockBlockService = new Mock<IBlockingService>();
                mockBlockService.Setup(bs => bs.IsBlockExists(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(false));

                var mockSpamDetectionService = new Mock<ISpamDetectionService>();
                mockSpamDetectionService.Setup(sds => sds.IsSpam(It.IsAny<GetSpamProbabilityRequestDto>()))
                    .Returns(Task.FromResult(true));

                var messagingService = new MessagingService(mockUserService.Object, dbContext,
                    mockBlockService.Object, GetMapper(), mockSpamDetectionService.Object);

                // act
                var result = await messagingService.SendMessage(new SendMessageRequestDto
                {
                    ReceiverId = receiverId,
                    SenderId = senderId,
                    Text = "Hello world!"
                });

                // assert
                Assert.True(result.IsSuccess);
                Assert.Empty(result.ErrorMessages);
            }
        }

        [Fact]
        public async Task ShoudReturnErrorMessage_WhenThereBlockExists_SendMessage()
        {
            // assert
            using (var dbContext = new MessagingDbContext(ContextOptions))
            {
                var senderId = 1453;
                var receiverId = 1789;

                var mockUserService = new Mock<IUserService>();
                mockUserService.Setup(us => us.IsExists(It.IsAny<int>()))
                    .Returns(Task.FromResult(true));

                var mockBlockService = new Mock<IBlockingService>();
                mockBlockService.Setup(bs => bs.IsBlockExists(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(true));
                
                var mockSpamDetectionService = new Mock<ISpamDetectionService>();
                mockSpamDetectionService.Setup(sds => sds.IsSpam(It.IsAny<GetSpamProbabilityRequestDto>()))
                    .Returns(Task.FromResult(true));

                var messagingService = new MessagingService(mockUserService.Object, dbContext,
                    mockBlockService.Object, GetMapper(), mockSpamDetectionService.Object);

                // act
                var result = await messagingService.SendMessage(new SendMessageRequestDto
                {
                    ReceiverId = receiverId,
                    SenderId = senderId,
                    Text = "Hello world!"
                });

                // assert
                Assert.False(result.IsSuccess);
                Assert.Contains(result.ErrorMessages, errorMessage =>
                    string.Equals(errorMessage, ErrorMessages.MessageBlockedUser));
            }
        }

        [Fact]
        public async Task ShoudReturnErrorMessage_WhenReceiverNotExists_SendMessage()
        {
            // assert
            using (var dbContext = new MessagingDbContext(ContextOptions))
            {
                var senderId = 1453;
                var receiverId = 1789;

                var mockUserService = new Mock<IUserService>();
                mockUserService.Setup(us => us.IsExists(It.IsAny<int>()))
                    .Returns(Task.FromResult(false));

                var mockBlockService = new Mock<IBlockingService>();
                mockBlockService.Setup(bs => bs.IsBlockExists(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(false));

                var mockSpamDetectionService = new Mock<ISpamDetectionService>();
                mockSpamDetectionService.Setup(sds => sds.IsSpam(It.IsAny<GetSpamProbabilityRequestDto>()))
                    .Returns(Task.FromResult(true));

                var messagingService = new MessagingService(mockUserService.Object, dbContext,
                    mockBlockService.Object, GetMapper(), mockSpamDetectionService.Object);

                // act
                var result = await messagingService.SendMessage(new SendMessageRequestDto
                {
                    ReceiverId = receiverId,
                    SenderId = senderId,
                    Text = "Hello world!"
                });

                // assert
                Assert.False(result.IsSuccess);
                Assert.Contains(result.ErrorMessages, errorMessage =>
                    string.Equals(errorMessage, ErrorMessages.MessageReceiverNotFound));
            }
        }

        [Fact]
        public async Task ShoudReturnMultipleErrorMessages_WhenReceiverNotExistsAndBlockExists_SendMessage()
        {
            // assert

            using (var dbContext = new MessagingDbContext(ContextOptions))
            {
                var senderId = 1453;
                var receiverId = 1789;

                var mockUserService = new Mock<IUserService>();
                mockUserService.Setup(us => us.IsExists(It.IsAny<int>()))
                    .Returns(Task.FromResult(false));

                var mockBlockService = new Mock<IBlockingService>();
                mockBlockService.Setup(bs => bs.IsBlockExists(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(Task.FromResult(true));

                var mockSpamDetectionService = new Mock<ISpamDetectionService>();
                mockSpamDetectionService.Setup(sds => sds.IsSpam(It.IsAny<GetSpamProbabilityRequestDto>()))
                    .Returns(Task.FromResult(true));

                var messagingService = new MessagingService(mockUserService.Object, dbContext,
                    mockBlockService.Object, GetMapper(), mockSpamDetectionService.Object);

                // act
                var result = await messagingService.SendMessage(new SendMessageRequestDto
                {
                    ReceiverId = receiverId,
                    SenderId = senderId,
                    Text = "Hello world!"
                });

                // assert
                Assert.False(result.IsSuccess);
                Assert.Contains(result.ErrorMessages, errorMessage =>
                    string.Equals(errorMessage, ErrorMessages.MessageReceiverNotFound));
                Assert.Contains(result.ErrorMessages, errorMessage =>
                    string.Equals(errorMessage, ErrorMessages.MessageBlockedUser));
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

        #endregion

    }
}
