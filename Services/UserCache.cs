using MemoGlobalTest.Modles;
using MemoGlobalTest.Data.Entities;
using MemoGlobalTest.Data.Entities.MetadataConfiguration;

namespace MemoGlobalTest.Services
{
    public class UserCache
    {
        public static void AddUser(ReqresUser? newUser, Entities db)
        {

            if (newUser == null) return;

            var userExist = db.ReqresUser.Any(u => u.id == newUser.id);

            if (!userExist)
            {
                var user = new ReqresUser()
                {
                    id = newUser.id,
                    avatar = newUser.avatar,
                    first_name = newUser.first_name,
                    last_name = newUser.last_name,
                    email = newUser.email,
                    createdAt = DateTime.UtcNow
                };

                db.ReqresUser.Add(user);
                db.SaveChanges();
            }
        }

        public static void AddUsers(List<ReqresUser> newUsers, Entities db)
        {
            foreach (var newUser in newUsers)
            {
                AddUser(newUser, db);
            }
        }

        public static ReqresUser? GetUser(int userId, Entities db)
        {
            var user = db.ReqresUser.Find(userId);
            return user;
        }

        public static void DeleteUser(int userId, Entities db)
        {
            var user = db.ReqresUser.Find(userId);

            if (user != null)
            {
                db.ReqresUser.Remove(user);
                db.SaveChanges();
            }
        }

        public static void UpdateUser(int userId, UserDetails? userDetails, Entities db)
        {
            if (userDetails == null) return;

            var user = db.ReqresUser.Find(userId);

            if (user != null)
            {
                user.email = userDetails.email;
                user.first_name = userDetails.first_name;
                user.last_name = userDetails.last_name; 
                user.avatar = userDetails.avatar;
                user.updatedAt = DateTime.UtcNow;
                db.SaveChanges();
            }
        }
    }
}
