using Domain.Data.Entities;
using Domain.Data;
using Infrastraction.Events;
using Infrastraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.MVVM
{
    internal class Model
    {
        public UserService userService;

        public Model() { 
            userService = new UserService();
        }

        public void BindInsertionEvent(UserInsertItemDelegate del)
        {
            userService.InsertUserEvent += del;
        }

        public void EditUser(UserEntity e)
        {
            using (MyDataContext context = new MyDataContext())
            {
                UserEntity entity = context.Users.FirstOrDefault(u => u.Id == e.Id);
                if (entity != null)
                {
                    entity.LastName = e.LastName;
                    entity.FistName = e.FistName;
                    entity.PhoneNumber = e.PhoneNumber;
                    entity.Email = e.Email;
                    entity.Image = e.Image;
                }
                context.SaveChanges();
            }
        }

        public Task AddRandomUsers(int count) => 
            userService.InsertRandomUserAsync(count);

    }
}
