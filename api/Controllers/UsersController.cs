using api.Data;
using api.DTOs;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly ApplicationContext _context;
        public UsersController(ApplicationContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLogin user){
            var check = await _context.Users.SingleOrDefaultAsync(u => u.Email == user.email);

            if(check == null)
            {
                var newUser = new User(){
                    Email = user.email
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                check = newUser;
            }

            return check;
        }

        // public  bool Register(string email){
        //     var user = new User(){
        //         Email = email
        //     };
        //     var result =  _context.Users.Add(user);
        //     _context.SaveChanges();
        //     return true;
        // }
    }
}