using ConcordiaCurriculumManager.Services;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class InputHashServiceTests
{
    private static IInputHasherService _inputHasherService = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        _inputHasherService = new InputHasherService();
    }

    [TestMethod]
    public void Hash_ValidInput_ReturnsNonEmptyString()
    {
        string input = "password";

        string hash = _inputHasherService.Hash(input);

        Assert.IsFalse(string.IsNullOrEmpty(hash));
    }

    [TestMethod]
    public void Verify_CorrectInputAndHash_ReturnsTrue()
    {
        string input = "password";
        string hash = _inputHasherService.Hash(input);

        bool result = _inputHasherService.Verify(input, hash);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Verify_IncorrectInputAndHash_ReturnsFalse()
    {
        string correctInput = "password";
        string incorrectInput = "wrongpassword";
        string hash = _inputHasherService.Hash(correctInput);

        bool result = _inputHasherService.Verify(incorrectInput, hash);

        Assert.IsFalse(result);
    }
}
