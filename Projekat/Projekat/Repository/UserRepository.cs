using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Repository
{
    public class UserRepository
    {
        private List<User> users;
        private JsonSerializer<User> userSerializer = new JsonSerializer<User>();
        private string path = @"C:\Users\dejana\Desktop\SIMS\Projekat\Projekat\Data\users.json";


        public List<User> GetAllUsers()
        {
            users = userSerializer.LoadDataFromJson(path) as List<User>;
            return users;
        }

        public User FindByJmbg(string jmbg)
        {
            users = GetAllUsers();
            foreach (User user in users)
            {
                if (user.JMBG.Equals(jmbg))
                {
                    return user;
                }
            }
            return null;
        }

        public User FindByEmail(string email)
        {
            users = GetAllUsers();
            foreach (User user in users)
            {
                if (user.Email.Equals(email))
                {
                    return user;
                }
            }
            return null;
        }

        public User GetById(string id)
        {
            users = GetAllUsers();

            foreach (User user in users)
            {
                if (user.Id.Equals(id))
                {
                    return user;
                }
            }
            return null;
        }

        public void SaveChanges(List<User> users)
        {
            userSerializer.SaveDataToJson(users, path);
        }

        public void AddNewUser(User user)
        {
            users = GetAllUsers();
            users.Add(user);
            SaveChanges(users);
        }

        public void DeleteUser(User user)
        {
            users = GetAllUsers();
            users.Remove(user);
            SaveChanges(users);

        }

        public void UpdateUser(User user)
        {
            users = GetAllUsers();

            User existingUser = users.FirstOrDefault(u => u.JMBG.Equals(user.JMBG));
            if (existingUser != null)
            {
                int index = users.IndexOf(existingUser);
                users[index] = user;
                SaveChanges(users);
            }
        }



    }
}
