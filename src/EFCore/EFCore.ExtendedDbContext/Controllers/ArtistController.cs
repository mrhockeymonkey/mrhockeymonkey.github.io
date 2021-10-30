using System;
using System.Collections.Generic;
using System.Linq;
using EFCore.ExtendingDbContext;
using EFCore.ExtendingDbContext.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly MusicDbContext _context;

        public ArtistController(MusicDbContext context)
        {
           // _context = context as MusicDbContext; // this is wrong?
           _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Artist>> GetArtists()
        {
            return _context.Artists.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Artist> GetArtist(Guid id)
        {
            // another way to access the table is to use Set<>()
            return _context.Set<Artist>().Single(a => a.Id == id);
        }
        
        [HttpPost]
        public IActionResult PostSong(string name)
        {
            _context.Artists.Add(new Artist { Name = name });
            _context.SaveChanges();
            
            return Ok();
        }
    }
}