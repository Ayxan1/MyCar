using System.Collections.Generic;
using AutoMapper;
using Mycars.Data;
using Mycars.Dtos;
using Mycars.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;


namespace Mycars.Controllers
{

    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        private readonly MycarsContext _db; // <<=SEARCH.........
        private readonly IMycarsRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly int currentUserId;

        public BrandsController(IMycarsRepo repository, IMapper mapper, MycarsContext db, IHttpContextAccessor _httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _db = db;
            httpContextAccessor = _httpContextAccessor;
            currentUserId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst("Id_").Value);
        }

        // <<=SEARCH.........
        /*
                public IActionResult Index()  // <<=SEARCH.........
                {
                    var displaydata = _db.Brands.ToList();
                    return View(displaydata);
                }*/
        // <<=SEARCH.........

        [HttpGet("search/modelVSbrand")]   // <<=SEARCH.........
        public async Task<IActionResult> Index(string Brandsearch)
        {
           // View data["Getbrandsdetails"] = Brandsearch;
            var brandsquery = from x in _db.Brands select x;
            if (!String.IsNullOrEmpty(Brandsearch))
            {
                brandsquery = brandsquery.Where(x => x.model.Contains(Brandsearch) || x.brand.Contains(Brandsearch));
            }
            return View(await brandsquery.AsNoTracking().ToListAsync());
        }
        // <<=SEARCH.........

        private IActionResult View(List<Brands> displaydata)  // <<=SEARCH.........
        {
            throw new NotImplementedException();
        }
        // <<=SEARCH.........



        //GET api/brands
        [HttpGet("/brands/models")]

        [Authorize(Policy = Policies.User)]

        public ActionResult <IEnumerable<BrandReadDto>> GetAllBrands()
        {
            var brandItems = _repository.GetAllBrands();

            return Ok(_mapper.Map<IEnumerable<BrandReadDto>>(brandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name="GetBrandById")]
        public ActionResult <BrandReadDto> GetBrandById(int id)
        {
            var brandItem = _repository.GetBrandById(id);
            if(brandItem != null)
            {
                return Ok(_mapper.Map<BrandReadDto>(brandItem));
            }
            return NotFound();
        }

        //POST api/brands
        [HttpPost]
        public ActionResult <BrandReadDto> CreateBrand(BrandCreateDto brandCreateDto)
        {
            var brandModel = _mapper.Map<Brands>(brandCreateDto);
            _repository.CreateBrand(brandModel);
            _repository.SaveChanges();

            var brandReadDto = _mapper.Map<BrandReadDto>(brandModel);

            return CreatedAtRoute(nameof(GetBrandById), new { Id = brandReadDto.Id }, brandReadDto);
        }

        //PUT api/brands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateBrand(int id, BrandUpdateDto brandUpdateDto)
        {
            var brandModelFromRepo = _repository.GetBrandById(id);
            if(brandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(brandUpdateDto, brandModelFromRepo);

            _repository.UpdateBrand(brandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/brands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialBrandUpdate(int id, JsonPatchDocument<BrandUpdateDto> patchDoc)
        {
            var brandModelFromRepo = _repository.GetBrandById(id);
            if(brandModelFromRepo == null)
            {
                return NotFound();
            }

            var brandToPatch = _mapper.Map<BrandUpdateDto>(brandModelFromRepo);
            patchDoc.ApplyTo(brandToPatch, ModelState);

            if(!TryValidateModel(brandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(brandToPatch, brandModelFromRepo);

            _repository.UpdateBrand(brandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/brands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBrand(int id)
        {
            var brandModelFromRepo = _repository.GetBrandById(id);
            if(brandModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteBrand(brandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        
    }
}