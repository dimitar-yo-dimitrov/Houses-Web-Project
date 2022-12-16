namespace Houses.Tests
{
    [TestFixture]
    public class AccountControllerTest
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _dbContext;
        private IApplicationDbRepository _repository;
        private AccountController _accountController;

        const string userId = "D5F34ADA-B2F3-4890-B6BA-C055EBC85703";

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Houses.bg")
                .Options;

            _dbContext = new ApplicationDbContext(contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _repository = new ApplicationDbRepository(_dbContext);
            _accountController = new AccountController(_signInManager, _userManager);
        }

        [Test]
        public async Task TestRegister()
        {
            var user = new ApplicationUser
            {
                Id = userId,
                Email = "user@gmail.com",
                FirstName = "Peter",
                LastName = "",
                CreatedOn = DateTime.UtcNow,
                PhoneNumber = "",
                EmailConfirmed = true,
                UserName = "",
                ProfilePicture = ""
            };

            Assert.IsNotNull(user);

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();


        }
    }
}
