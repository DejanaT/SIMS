using Project.Model;
using Project.Model.Enums;
using Projekat.DTOs;
using Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat.Service
{
    public class UserService
    {
        private UserRepository userRepository = new UserRepository();

        public List<User> GetUsers()
        {
            return userRepository.GetAllUsers();
        }

        public User FindByJmbg(string jmbg)
        {
           return userRepository.FindByJmbg(jmbg);
        }

        public User FindByEmail(string email)
        {
            return userRepository.FindByEmail(email);
        }

        public void SaveChanges(List<User> users)
        {
            userRepository.SaveChanges(users);
        }

        public void Create(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            bool isUserExist = userRepository.GetAllUsers().Any(u => u.Id == user.Id || u.JMBG == user.JMBG || u.Email == user.Email);

            if (!isUserExist)
            {
                userRepository.AddNewUser(user);
                MessageBox.Show("User registration successful!");
            }
            else
            {
                MessageBox.Show("User with the same JMBG or Email already exists!");
            }
        }

        public void DeleteUser(User user)
        {
            userRepository.DeleteUser(user);
        }

        public string GetRole(User user)
        {
            User user1 = userRepository.GetById(user.Id);

            if (user1 != null)
            {
                switch (user1.UserType)
                {
                    case UserType.Administrator:
                        return "Administrator";
                    case UserType.Host:
                        return "Host";
                    case UserType.Guest:
                        return "Guest";
                    default:
                        return null;
                }
            }

            return null;
        }

        public bool IsUserExist(LoginDTO loginDTO)
        {
            List<User> users = GetUsers();

            return users.Any(user => loginDTO.Email.Equals(user.Email) && loginDTO.Password.Equals(user.Password));
        }
        public void BlockUser(User user)
        {
            user.Blocked = true;
            userRepository.UpdateUser(user);
        }

        public void UnblockUser(User user)
        {
            user.Blocked = false;
            userRepository.UpdateUser(user);
        }

        public List<string> GetHostJmbgs()
        {
            List<User> hostUsers = userRepository.GetUsersByType(UserType.Host);
            List<string> hostsJmbgs = hostUsers.Select(user => user.JMBG).ToList();
            return hostsJmbgs;
        }


    }
}
