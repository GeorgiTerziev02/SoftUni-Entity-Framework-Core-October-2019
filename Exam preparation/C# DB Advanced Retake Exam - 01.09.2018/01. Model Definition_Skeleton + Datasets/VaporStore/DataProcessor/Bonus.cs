namespace VaporStore.DataProcessor
{
    using System;
    using System.Linq;
    using Data;

    public static class Bonus
    {
        public static string UpdateEmail(VaporStoreDbContext context, string username, string newEmail)
        {
            var userExist = context
                .Users
                .Any(u => u.Username == username);

            if (userExist == false)
            {
                return $"User {username} not found";
            }

            var isEmailTaken = context
                .Users
                .Any(u => u.Email == newEmail);

            if (isEmailTaken)
            {
                return $"Email {newEmail} is already taken";
            }

            var user = context
                .Users
                .First(u => u.Username == username);

            user.Email = newEmail;
            context.SaveChanges();

            return $"Changed {username}'s email successfully";
        }
    }
}
