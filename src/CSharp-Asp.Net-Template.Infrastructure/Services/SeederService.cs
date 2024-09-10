using Bogus;
using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Infrastructure.Contexts;
using CSharp_Asp.Net_Template.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CSharp_Asp.Net_Template.Infrastructure.Services
{
    public class SeederService
    {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordService _passwordService;
        private readonly ILogger<SeederService> _logger;
        private readonly Dictionary<string, Guid> _entityIds;

        public SeederService(AppDbContext dbContext, IPasswordService passwordService, ILogger<SeederService> logger)
        {
            _dbContext = dbContext;
            _passwordService = passwordService;
            _logger = logger;
            _entityIds = [];
        }

        private void PreGenerateIds()
        {
            _logger.LogDebug("Generating Seed Ids");
            _entityIds["user1"] = Guid.NewGuid();
            _entityIds["user2"] = Guid.NewGuid();
            _logger.LogDebug("Seed Ids Generated");

        }

        public async void SeedUsers()
        {
            _logger.LogDebug("Inserting seed users");
            try
            {
                IEnumerable<User> users =
                [
                    CreateUser("user1"),
                    CreateUser("user2")
                ];


                await _dbContext.AddRangeAsync(users);

                await _dbContext.SaveChangesAsync();

                _logger.LogDebug("Seed users inserted");
            }

            catch (Exception ex)
            {
                _logger.LogError("Error inserting seed users, {ex}", ex);
                // await transaction.RollbackAsync();
            }
        }

        private User CreateUser(string userKey)
        {
            var userPassword = _passwordService.GeneratePasswordSaltAndHash("password");

            var user = new Faker<User>()
                .RuleFor(u => u.Id, _entityIds[userKey])
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName))
                .RuleFor(u => u.AvatarUrl, f => f.Internet.Avatar())
                .RuleFor(u => u.Password, userPassword.passwordHash)
                .RuleFor(u => u.PasswordSalt, userPassword.passwordSalt)
                .Generate();

            return user;


        }
    }
}
