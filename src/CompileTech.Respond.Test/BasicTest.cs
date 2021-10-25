using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CompileTech.Respond.Test
{
    public class BasicTest
    {
        private readonly ITestOutputHelper _output;

        public BasicTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void OnSuccessGetsFired()
        {
            var response = Response.Void();

            var resolvedSuccess = false;

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
            Response response = ValidationErrorResult.Make("ERROR");

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

        [Fact]
        public void OnSuccessGetsFiredGeneric()
        {
            Response<string> response = "SUCCESS";

            var resolvedSuccess = false;

            var result = response.Resolve((r) =>
            {
                resolvedSuccess = true;
                return r;
            }, _ => throw new XunitException("onError was called."));

            _output.WriteLine(result);

            Assert.True(resolvedSuccess);
        }

        [Fact]
        public void OnErrorGetsFiredGeneric()
        {
            Response<string> response = ValidationErrorResult.Make("ERROR");

            var resolvedError = false;

            var result = response.Resolve(
                _ => throw new XunitException("onSuccess was called."),
                (e) =>
                {
                    resolvedError = true;
                    return e.Message;
                });

            _output.WriteLine(result);

            Assert.True(resolvedError);
        }
    }
}