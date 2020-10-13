using System;
namespace Messaging.Core.Constants
{
    public class ErrorMessages
    {
        public readonly static string RegisterEmailAlreadyExists = "Email is already exists. Please try a different email";
        public readonly static string RegisterUsernameAlreadyExists = "Username is already exists. Please try a different username";

        public readonly static string LoginInvalidLogin = "Invalid login";

        public readonly static string MessageReceiverNotFound = "User that you are trying to send message is not exists";
        public readonly static string MessageBlockedUser = "You are not allowed to send message to this user";

        public readonly static string BlockingBlockedUserNotFound = "User that you are trying to block is not exists";
    }
}
