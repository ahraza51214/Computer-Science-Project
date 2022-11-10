using System;
using AutoMapper;
using DataServiceLib;
using DataServiceLib.DBObjects;
using Microsoft.AspNetCore.Mvc;
using ProjectPortfolio2_Group11.Authentication.Attributes;
using ProjectPortfolio2_Group11.Model;

namespace ProjectPortfolio2_Group11.Controller
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly DataServiceFacade _dataServiceFacade;
        private readonly IMapper _mapper;
        private const int MaxPageSize = 100;

        public SearchController(DataServiceFacade dataServiceFacade, IMapper mapper)
        {
            _dataServiceFacade = dataServiceFacade;
            _mapper = mapper;
        }
        // uncomment the code below  for enabling authorization

        /* [Authorization]
         [HttpGet("{userId}")]
         public IActionResult GetSearchHistory(int userId)
         { 
             try
             {
                 var user = Request.HttpContext.Items["User"] as UsersForAuth;
                 var search = _dataServiceFacade.SearchDs.GetSearchHistory(user.UserId, userId);
                 if (search == null)
                 {
                     return NotFound();
                 }
                 return Ok(search);
             }
             catch (Exception)
             {
                 return Unauthorized();
             }
         } */

        [HttpGet("{userId}")] // comment the below HTTPGet method for enabling authorization
        public IActionResult GetSearchHistory(int userId)
        {
            var search = _dataServiceFacade.SearchDs.GetSearchHistory(userId);
            if (search.Count == 0)
            {
                return NotFound("Search history not found");
            }
            return Ok(search);
        } // comment the above HTTPGet method for enabling authorization



        [HttpPost("{userId}", Name = nameof(AddToSearchHistory))]
        public IActionResult AddToSearchHistory(int page, int pageSize, SearchHistoryDto searchDto)
        {
            pageSize = CheckPageSize(pageSize);
            var search = _dataServiceFacade.SearchDs.AddToSearchHistory(page, pageSize, searchDto.UserId, searchDto.SearchInput /*,searchDto.PrimaryTitle, searchDto.Characters, searchDto.PrimaryName */);
            //  var search2 = _dataServiceFacade.SearchDs.AddToSearchHistory(page, pageSize, searchDto.UserId, searchDto.SearchInput ,searchDto.PrimaryTitle, searchDto.Characters, searchDto.PrimaryName );
            var count = _dataServiceFacade.SearchDs.NumberOfElements(searchDto.UserId, searchDto.SearchInput);
            var navigationUrls = CreatePagingNavigation(page, pageSize, count);
            var result = new
            {
                navigationUrls.prev,
                navigationUrls.next,
                navigationUrls.current,
                count,
                search,//search2

            };
            if (search.Count == 0)
            {
               return NotFound("Search found no matches");
          
            }

            return Ok(result);
        }
         
        private int CheckPageSize(int pageSize)
        {
            return pageSize > MaxPageSize ? MaxPageSize : pageSize;
        }
        
        private (string prev, string current, string next) CreatePagingNavigation(int page, int pageSize, int count)
        {
            string prev = null;
            
            if (page > 0)
            {
                prev = Url.Link(nameof(AddToSearchHistory), new { page = page - 1, pageSize });
            }
            
            string next = null;
            
            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
                next = Url.Link(nameof(AddToSearchHistory), new { page = page + 1, pageSize });

            var current = Url.Link(nameof(AddToSearchHistory), new { page, pageSize });
            
            return (prev, current, next);
        }
    }
}
