using Domain.Primitives;

namespace Domain.UnitTests.Primitives;

public class ErrorTests
{
    [Fact]
    public void ErrorWithEmptyCodeAndNonEmptyDescriptionIsCreatedCorrectly()
    {
        // Arrange
        string code = string.Empty;
        const string description = "Test Description";

        // Act
        ErrorType error = new(code, description);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
    }

    [Fact]
    public void ErrorWithEmptyCodeAndNullDescriptionIsCreatedCorrectly()
    {
        // Arrange
        string code = string.Empty;
        string? description = null;

        // Act
        ErrorType error = new(code, description);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Null(error.Description);
    }

    [Fact]
    public void ErrorWithNonEmptyCodeAndNonEmptyDescriptionIsCreatedCorrectly()
    {
        // Arrange
        const string code = "Test Code";
        const string description = "Test Description";

        // Act
        ErrorType error = new(code, description);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
    }

    [Fact]
    public void ErrorWithNonEmptyCodeAndNullDescriptionIsCreatedCorrectly()
    {
        // Arrange
        const string code = "Test Code";
        string? description = null;

        // Act
        ErrorType error = new(code, description);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Null(error.Description);
    }

    [Fact]
    public void ErrorWithNoneHasEmptyCodeAndDescription()
    {
        // Arrange
        string expectedCode = string.Empty;
        string expectedDescription = string.Empty;

        // Act
        ErrorType error = ErrorType.None;

        // Assert
        Assert.Equal(expectedCode, error.Code);
        Assert.Equal(expectedDescription, error.Description);
    }
}
