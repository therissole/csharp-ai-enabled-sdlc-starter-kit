using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using StarterKit.Api.Features.Languages;
using StarterKit.Api.Infrastructure;

namespace StarterKit.UnitTests.Features.Languages;

[TestFixture]
public class LanguageRepositoryTests
{
    private Mock<IDbConnectionFactory> _mockConnectionFactory;
    private Mock<ILogger<LanguageRepository>> _mockLogger;
    private LanguageRepository _repository;

    [SetUp]
    public void Setup()
    {
        _mockConnectionFactory = new Mock<IDbConnectionFactory>();
        _mockLogger = new Mock<ILogger<LanguageRepository>>();
        _repository = new LanguageRepository(_mockConnectionFactory.Object, _mockLogger.Object);
    }

    [Test]
    public void Constructor_WithNullConnectionFactory_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => 
            new LanguageRepository(null!, _mockLogger.Object));
    }

    [Test]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => 
            new LanguageRepository(_mockConnectionFactory.Object, null!));
    }
}
