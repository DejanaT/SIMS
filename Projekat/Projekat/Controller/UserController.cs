using Project.Model;
using Projekat.DTOs;
using Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Controller
{
    public class UserController
    {
        private UserService userService = new UserService();

        public List<User> GetUsers()
        {
            return userService.GetUsers();
        }
        public User FindByJmbg(string jmbg)
        {
            return userService.FindByJmbg(jmbg);
        }

        public User FindByEmail(string email)
        {
            return userService.FindByEmail(email);
        }
        public void Create(User user)
        {
            userService.Create(user);
        }

        public void DeleteUser(User user)
        {
            userService.DeleteUser(user);
        }

        public string GetRole(User user)
        {
            return userService.GetRole(user);
        }

        public bool IsUserExist(LoginDTO loginDTO)
        {
            return userService.IsUserExist(loginDTO);
        }

        public void BlockUser(User user)
        {
            userService.BlockUser(user);
        }

        public void UnblockUser(User user)
        {
            userService.UnblockUser(user);
        }

        public List<string> GetHostsJmbgs()
        {
           return userService.GetHostJmbgs();    
        }


    }
}
