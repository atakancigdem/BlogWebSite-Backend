using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogImagesController : ControllerBase
    {
        private readonly IBlogImageService _blogImageService;

        public BlogImagesController(IBlogImageService blogImageService)
        {
            _blogImageService = blogImageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _blogImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var result = _blogImageService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("blogid")]
        public IActionResult GetImagesByBlogId([FromForm(Name = ("BlogId"))] int blogId)
        {
            var result = _blogImageService.GetImagesByBlogId(blogId);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("Image"))] IFormFile file, [FromForm] BlogImage blogId)
        {
            var result = _blogImageService.Add(file, blogId);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm] int id)
        {
            var blogImage = _blogImageService.GetById(id).Data;
            var result = _blogImageService.Delete(blogImage);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, [FromForm(Name = ("Id"))] int Id)
        {
            var blogImage = _blogImageService.GetById(Id).Data;
            var result = _blogImageService.Update(file, blogImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
