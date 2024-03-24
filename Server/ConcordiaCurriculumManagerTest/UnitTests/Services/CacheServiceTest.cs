using ConcordiaCurriculumManager.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class CacheServiceTests
{
    private CacheService<string> _cacheService = null!;
    private Mock<IMemoryCache> _memoryCacheMock = null!;
    private Mock<ILogger<CacheService<string>>> _loggerMock = null!;

    [TestInitialize]
    public void Initialize()
    {
        _memoryCacheMock = new Mock<IMemoryCache>();
        _loggerMock = new Mock<ILogger<CacheService<string>>>();

        _cacheService = new CacheService<string>(_memoryCacheMock.Object, _loggerMock.Object);
    }

    [TestMethod]
    public void Get_ExistingKey_ReturnsCachedValue()
    {
        var key = "existingKey";
        object? cachedValue = "cachedValue";
        _memoryCacheMock.Setup(m => m.TryGetValue(key, out cachedValue)).Returns(true);

        var result = _cacheService.Get(key);

        Assert.AreEqual(cachedValue, result);
    }

    [TestMethod]
    public void GetOrCreate_ExistingKey_ReturnsCachedValue()
    {
        var key = "existingKey";
        object? cachedValue = "cachedValue";
        _memoryCacheMock.Setup(m => m.TryGetValue(key, out cachedValue)).Returns(true);

        var result = _cacheService.GetOrCreate(key, () => throw new InvalidOperationException("Should not be called"));

        Assert.AreEqual(cachedValue, result);
    }

    [TestMethod]
    public void GetOrCreate_NonExistingKey_ReturnsCreatedValue()
    {
        string key = "nonExistingKey";
        object? createdValue = "createdValue";
        var expiryDate = TimeSpan.FromMinutes(30);
        var cachEntry = new Mock<ICacheEntry>();

        _memoryCacheMock.Setup(m => m.TryGetValue(key, out createdValue)).Returns(false);
        _memoryCacheMock.Setup(m => m.CreateEntry(It.IsAny<object>())).Returns(cachEntry.Object);

        var result = _cacheService.GetOrCreate(key, () => ((string)createdValue, expiryDate, false));

        Assert.AreEqual(createdValue, result);
    }

    [TestMethod]
    public void Exists_ExistingKey_ReturnsTrue()
    {
        var key = "existingKey";
        object? cachedValue = "cachedValue";
        _memoryCacheMock.Setup(m => m.TryGetValue(key, out cachedValue)).Returns(true);

        var result = _cacheService.Exists(key);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Exists_NonExistingKey_ReturnsFalse()
    {
        var key = "nonExistingKey";
        object? cachedValue = "cachedValue";
        _memoryCacheMock.Setup(m => m.TryGetValue(key, out cachedValue)).Returns(false);

        var result = _cacheService.Exists(key);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Delete_CallsRemove()
    {
        var key = "existingKey";
        object? cachedValue = "cachedValue";

        _cacheService.Delete(key);

        _memoryCacheMock.Verify(m => m.Remove(key), Times.Once);
    }
}
