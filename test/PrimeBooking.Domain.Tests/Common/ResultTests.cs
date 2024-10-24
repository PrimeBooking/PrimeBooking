namespace PrimeBooking.Domain.Tests.Common;

public class ResultTests
{
    [Fact]
    public void Success_WithEmptyBody_ShouldCreateSuccessResult()
    {
        var result = Result.Success();
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Error.Should().BeNull();
    }
    
    [Fact]
    public void Failure_WithError_ShouldCreateFailedResult()
    {
        Error error = ErrorFactory.BuildError(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), HttpStatusCode.BadRequest);
        
        var result = Result.Failure(error);
        
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
