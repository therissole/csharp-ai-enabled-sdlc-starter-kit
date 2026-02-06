using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using StarterKit.Api.Features.Greetings;
using StarterKit.Api.Infrastructure;

namespace StarterKit.UnitTests.Features.Greetings;

[TestFixture]
public class GreetingRepositoryTests
{
    private Mock<IDbConnectionFactory> _mockConnectionFactory;
    private Mock<ILogger<GreetingRepository>> _mockLogger;
    private GreetingRepository _repository;

    [SetUp]
    public void Setup()
    {
        _mockConnectionFactory = new Mock<IDbConnectionFactory>();
        _mockLogger = new Mock<ILogger<GreetingRepository>>();
        _repository = new GreetingRepository(_mockConnectionFactory.Object, _mockLogger.Object);
    }

    [Test]
    public void Constructor_WithNullConnectionFactory_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => 
            new GreetingRepository(null!, _mockLogger.Object));
    }

    [Test]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => 
            new GreetingRepository(_mockConnectionFactory.Object, null!));
    }
}
