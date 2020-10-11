using System;
namespace Messaging.Core.Constants
{
    public class ErrorCodes
    {
        public readonly static string RegisterEmailAlreadyExists = "Register.EmailExists";
        public readonly static string RegisterUsernameAlreadyExists = "Register.UsernameExists";

        public readonly static string LoginInvalidLogin = "Login.InvalidLogin";

        public readonly static string MessageReceiverNotFound = "Message.ReceiverNotFound";

        public readonly static string MessageBlockedUser = "Message.BlockedUser";

        public readonly static string BlockingBlockedUserNotFound = "Blocking.BlockedUserNotFound";
    }
}
