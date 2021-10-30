using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoFixture;
using AutoFixture.AutoMoq;
using EFCore.Controllers;
using EFCore.ExtendingDbContext;
using EFCore.ExtendingDbContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EFCore.ExtendedDbContext.Tests.Controllers
{
    [TestFixture]
    public class ArtistControllerTests
    {
        private IFixture _fixture;
        private MusicDbContext _inMemoryMusicDbContext;
        private ArtistController _sut;
        
        [SetUp]
        public void BeforeEachTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            _inMemoryMusicDbContext = new MusicDbContext(new DbContextOptionsBuilder<MusicDbContext>()
                    .UseInMemoryDatabase("TestDB")
                    .Options);
            _fixture.Inject<MusicDbContext>(_inMemoryMusicDbContext);

            _inMemoryMusicDbContext.Database.EnsureDeleted();
            _inMemoryMusicDbContext.Database.EnsureCreated();

            _inMemoryMusicDbContext.Artists.Add(new Artist{Name = "Artist1"});
            _inMemoryMusicDbContext.Artists.Add(new Artist{Name = "Artist2"});
            _inMemoryMusicDbContext.SaveChanges();

            _sut = _fixture
                .Build<ArtistController>()
                .OmitAutoProperties()
                .Create();
        }

        [Test]
        public void GetArtists_ReturnsAllArtists()
        {
            var result = _sut.GetArtists();
            
            Assert.That(result, Is.TypeOf<ActionResult<IEnumerable<Artist>>>());
            //var actionResult = result as ActionResult<IEnumerable<Artist>>;
            Assert.That(result?.Value, Is.Not.Null);
            Assert.That(result!.Value, Is.TypeOf<List<Artist>>());
            var artistsResult = result.Value as List<Artist>;
            Assert.That(
                artistsResult.Select(a => a.Name), 
                Is.EquivalentTo(new []{ "Artist1", "Artist2"}));
            
            TestContext.WriteLine(JsonSerializer.Serialize(artistsResult));
        }
    }
}