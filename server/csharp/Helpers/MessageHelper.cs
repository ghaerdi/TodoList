namespace todolist.Helpers;

class MessageHelper
{
    public static class Success
    {
        public static string Welcome = "Welcome to Tasker";
        public static string Created = "created successfully";
        public static string Deleted = "deleted successfully";
        public static string Edited = "edited successfully";
        public static string Found = "found";
    }

    public static class Error
    {
        public static string AlreadyExist = "already exists";
        public static string Unknown = "unknown error";
        public static string NotFound = "not found";
        public static string Required = "is required";
        public static string Invalid = "is invalid";
        public static string InvalidUserRelation = "does not correspond to a valid user";
    }
}
