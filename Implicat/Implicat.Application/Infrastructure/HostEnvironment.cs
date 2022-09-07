namespace Implicat.Application.Infrastructure
{
    public static class HostEnvironment
    {
        private const string EnvName = "ASPNETCORE_ENVIRONMENT";

        public static string CurrentEnv =>
            Environment.GetEnvironmentVariable(EnvName)
            ?? throw new InvalidOperationException($"Environment variable '${EnvName}'must be set");

        public static bool IsLocal => CurrentEnv == "Local";

        public static bool IsDevelopment => CurrentEnv == "Development";

        public static bool IsTest => CurrentEnv == "Test";

        public static bool IsProduction => CurrentEnv == "Production";

        public static bool IsNotProduction => !IsProduction;
    }
}