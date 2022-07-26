using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P225FirstApi.Data;
using P225FirstApi.Data.Entities;
using P225FirstApi.DTOs.BrandDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P225FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BrandsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBrands()
        {
            List<Brand> brands = _context.Brands.ToList();

            List<BrandGetDTO> brandGetDTOs = new List<BrandGetDTO>();

            foreach (Brand brand in brands)
            {
                brandGetDTOs.Add(_mapper.Map<BrandGetDTO>(brand));
            }

            return Ok(brandGetDTOs);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(BrandPostDTO brandPostDTO)
        {
            Brand brand = _mapper.Map<Brand>(brandPostDTO);

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            BrandGetDTO categoryGetDto = _mapper.Map<BrandGetDTO>(brand);

            return StatusCode(201, categoryGetDto);
        }

        [HttpDelete]
        [Route("{id?}")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest("Id Is Requeired");

            Brand dbBrand = await _context.Brands.FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);

            if (dbBrand == null) return NotFound("Id Is InCorrect");

            dbBrand.IsDeleted = true;
            dbBrand.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("{id?}")]
        public async Task<IActionResult> Put(int? id, BrandPutDTO brandPutDto)
        {
            if (id == null) return BadRequest("Id Is Requeired");


            Brand dbBrand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == (int) id);

            if (dbBrand == null) return NotFound("Id Is InCorrect");

            dbBrand.Name = brandPutDto.Name.Trim();

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
