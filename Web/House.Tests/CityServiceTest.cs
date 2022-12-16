namespace Houses.Tests
{
    [TestFixture]
    public class CityServiceTest
    {
        private ApplicationDbContext _dbContext;
        private IApplicationDbRepository _repository;
        private CityService _cityService;

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
            _cityService = new CityService(_repository);
        }

        [Test]
        public async Task TestGetCitiesAsync()
        {
            var cities = await _cityService.GetAllCitiesAsync();

            var getByNameCities = await _cityService.AllCityNamesAsync();

            Assert.That(cities.Count(), Is.EqualTo(expected: 18));
            Assert.That(getByNameCities.Count(), Is.EqualTo(expected: 18));
        }
    }
}
