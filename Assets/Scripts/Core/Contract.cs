using System;

namespace Core
{
    public static class Contract
    {
        private const string RequireFailedMessage = "Require contract failed";
        private const string EnsureFailedMessage = "Ensure contract failed";
        public static void Require(bool condition) {
            if (!condition) {
                throw new ArgumentException(RequireFailedMessage);
            }
        }

        public static void Require(bool condition, string message) {
            if (!condition) {
                throw new ArgumentException(RequireFailedMessage + ": " + message);
            }
        }

        public static void Ensure(bool condition) {
             if (!condition) {
                throw new InvalidOperationException(EnsureFailedMessage);
            }
        }

        public static void Ensure(bool condition, string message) {
             if (!condition) {
                throw new InvalidOperationException(EnsureFailedMessage + ": " + message);
            }
        }
    }
}