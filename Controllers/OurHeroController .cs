using DotNet8WebApI.Model;
using DotNet8WebApI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OurHeroController : ControllerBase
    {
        private readonly IOurHeroService _HeroService;

        public OurHeroController( IOurHeroService ourHeroService)
        {
            _HeroService = ourHeroService;
        }

        [HttpGet]
        public IActionResult GetHero([FromQuery] bool? isActive = null) { 
            return Ok(_HeroService.GetAllHeros(isActive));  
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var hero = _HeroService.GetHerosByID(id);
            if (hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }

        [HttpPost]
        public IActionResult Post(AddUpdateOurHero heroObject)
        {
            var hero = _HeroService.AddOurHero(heroObject);

            if (hero == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Super Hero Created Successfully!!!",
                id = hero!.Id
            });
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] AddUpdateOurHero heroObject)
        {
            var hero = _HeroService.UpdateOurHero(id, heroObject);
            if (hero == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Super Hero Updated Successfully!!!",
                id = hero!.Id
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!_HeroService.DeleteHerosByID(id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Super Hero Deleted Successfully!!!",
                id = id
            });
        }

    }
}
