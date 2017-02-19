using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace TestLogin.Models.Repository
{
    public class UserRepository : IDisposable
    {
        bool disposed = false;
        DataBase db = new DataBase(); 
        Safe sf = new Safe(Safe.Algorithm.sha512);      
        
        public void Create(string name, string passWord, bool role)
        {
            var pw = sf.HashGen(passWord);           
            var user = new User { UserName = name, Password = pw, Role = role };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Edit(User obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Task<List<User>> GetAll()
        {
            return Task.Run(async () => { return await db.Users.ToListAsync(); });
        }

        public Task<User> GetOne(int id)
        {
            return Task.Run(async () => { return await db.Users.FindAsync(id); });
        }

        public void Remove(User obj)
        {
            db.Users.Remove(obj);
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