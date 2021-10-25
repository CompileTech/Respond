using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CompileTech.Respond.Test
{
    public class BasicValidationTest
    {
        private readonly ITestOutputHelper _output;

        public BasicValidationTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void OnSuccessGetsFired()
        {
            Response response = ServiceOrControllerStub.Validation(true);

            bool resolvedSuccess = false;

            var result = response.Resolve(() =>
            {
                resolvedSuccess = true;
                return "SUCCESS";
            }, _ => throw new XunitException("onError was called."));

            _output.WriteLine(result);

            Assert.True(resolvedSuccess);
        }


        [Fact]
        public void OnErrorGetsFired()
        {
            Response response = ServiceOrControllerStub.Validation(false);

            var resolvedError = false;

            var result = response.Resolve(
                () => throw new XunitException("onSuccess was called."),
                (e) =>
                {
                    resolvedError = true;
                    return e.Message;
                });

            _output.WriteLine(result);

            Assert.True(resolvedError);
        }
    }

    public static class ServiceOrControllerStub
    {
        public static Response Validation(bool passed)
        {
            var validation = passed ? ValidationStub.Passed() : ValidationStub.Failed();

            return validation ?? Response.Void();
        }
    }

    public static class ValidationStub
    {
        public static ErrorResult Passed()
        {
            return null;
        }

        public static ErrorResult Failed()
        {
            return ValidationErrorResult.Make("VALIDATION FAILED");
        }
    }
}