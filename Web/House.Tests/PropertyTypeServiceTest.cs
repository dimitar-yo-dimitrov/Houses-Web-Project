namespace Houses.Tests
{
    [TestFixture]
    public class PropertyTypeServiceTest
    {
        private ApplicationDbContext _dbContext;
        private IApplicationDbRepository _repository;
        private IPropertiesTypesService _propertiesTypesService;

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
            _propertiesTypesService = new PropertyTypeService(_repository);
        }

        [Test]
        public async Task TestAllPropertyTypesAsync()
        {
            var propertyTypes = await _propertiesTypesService.AllPropertyTypesAsync();

            var propertyTypeNames = await _propertiesTypesService.AllPropertyTypeNamesAsync();

            Assert.That(propertyTypes.Count(), Is.EqualTo(expected: 6));
            Assert.That(propertyTypeNames.Count(), Is.EqualTo(expected: 6));
        }
    }
}
