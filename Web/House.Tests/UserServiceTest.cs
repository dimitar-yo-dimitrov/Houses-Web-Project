using Houses.Core.ViewModels.User;
using Houses.Infrastructure.Data.Identity;

namespace Houses.Tests
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserService _userService;
        private ApplicationDbContext _dbContext;
        private IApplicationDbRepository _repository;

        const string userId = "D5F34ADA-B2F3-4890-B6BA-C055EBC85703";

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Houses.bg")
                .Options;

            _dbContext = new ApplicationDbContext(contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _repository = new ApplicationDbRepository(_dbContext);
            _userService = new UserService(_repository);
        }

        [Test]
        public async Task TestUserServiceAsync()
        {
            var user = new ApplicationUser
            {
                Id = userId,
                Email = "user@gmail.com",
                FirstName = "Peter",
                LastName = "",
                CreatedOn = DateTime.UtcNow,
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            var users = await _userService.GetUsers();
            var userById = await _userService.GetUserById(userId);
            var userExist = await _userService.ExistsById(userId);
            var userGetId = await _userService.GetUserId(userId);
            var userGetForEdit = await _userService.GetUserForEdit(userId);
            var userGetForProfile = await _userService.GetUserByIdForProfile(userId);

            Assert.That(userExist, Is.EqualTo(true));
            Assert.That(users.Count(), Is.EqualTo(1));
            Assert.That(userById.FirstName, Is.EqualTo("Peter"));
            Assert.That(userGetId, Is.EqualTo(userId));
            Assert.That(userGetForEdit.Id, Is.EqualTo(userId));
            Assert.That(userGetForProfile.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task TestUpdateAsync()
        {
            var user = new ApplicationUser
            {
                Id = userId,
                Email = "user@gmail.com",
                FirstName = "Peter",
                LastName = "",
                CreatedOn = DateTime.UtcNow,
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            var model = new EditUserInputViewModel
            {
                Id = userId,
                Email = "user@gmail.com",
                FirstName = "Maria",
                LastName = "",
                PhoneNumber = "",
                ProfilePicture = ""
            };

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.ProfilePicture = model.ProfilePicture;
            user.UserName = model.FirstName;

            await _repository.SaveChangesAsync();

            var result = await _userService.UpdateUser(model);

            var userUpdate = await _userService.GetUserById(userId);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(userUpdate.FirstName, Is.EqualTo("Maria"));
        }

        [Test]
        public async Task TestDeleteUserAsync()
        {
            var user = new ApplicationUser
            {
                Id = userId,
                Email = "user@gmail.com",
                FirstName = "Peter",
                LastName = "",
                CreatedOn = DateTime.UtcNow,
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            await _userService.DeleteUserAsync(userId);
            var users = await _userService.GetUsers();

            Assert.That(users.Count(), Is.EqualTo(0));
        }
    }
}
