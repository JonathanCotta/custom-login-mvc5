using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace TestLogin.Models.Repository
{
    public class UserRepository : IRepository<User>
    {

        DataBase db;
        bool disposed = false;

        Safe sf = new Safe(Safe.Algorithm.sha512);

        public UserRepository(DataBase d)
        {
            db = d;
        }

        public void Create(User item)
        {
            db.Users.Add(item);            
            db.SaveChanges();               
        }

        public void Create(string name, string passWord, bool role)
        {
            var pw = sf.HashGen(passWord);           
            Create(new User { UserName = name, Password = pw, Role = role });
        }

        public void Edit(User item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Task<List<User>> GetAll()
        {
            return Task.Run(async () => { return await db.Users.ToListAsync(); });
        }

        public Task<User> GetOne(int? id)
        {
            return Task.Run(async () => { return await db.Users.FindAsync(id); });
        }

        public void Remove(User item)
        {
            db.Users.Remove(item);
            db.SaveChanges();
        }

        public async void ChangePassWord(int id, string passWord)
        {
            User user = await GetOne(id);
            user.Password = sf.HashGen(passWord);
            Edit(user);
        }

        public User LogIn(string name, string password)
        {
            string passwordHash = sf.HashGen(password);

            try
            {
                User user = db.Users.Where(u => u.UserName == name & u.Password == passwordHash).Single();
                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                db.Dispose();
                sf.Dispose();
            }

            disposed = true; 
        }

    }
}