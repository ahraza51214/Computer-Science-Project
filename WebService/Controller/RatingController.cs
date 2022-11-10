using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataServiceLib;
using DataServiceLib.DBObjects;
using Microsoft.AspNetCore.Mvc;
using ProjectPortfolio2_Group11.Authentication.Attributes;
using ProjectPortfolio2_Group11.Model;

namespace ProjectPortfolio2_Group11.Controller
{
    [ApiController]
    [Route("api/rating")]
    public class RatingController :  ControllerBase
    {
        private readonly  DataServiceFacade _dataServiceFacade;
        private readonly IMapper _mapper;

        public RatingController(DataServiceFacade dataServiceFacade, IMapper mapper)
        {
            _dataServiceFacade = dataServiceFacade;
            _mapper = mapper;
        }
       /* 
        [Authorization]
        [HttpGet]
        public IActionResult GetRatingList()
        {
            try
            {
                var user = Request.HttpContext.Items["User"] as UsersForAuth;
                var userTitleRates = _dataServiceFacade.RatingDs.GetRatingList(user.UserId);
                return Ok(_mapper.Map<IEnumerable<UserTitleRateDto>>(userTitleRates));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        } */
       
        [HttpGet("{userId}")]
        public IActionResult GetRating(int userId)
        {
            var userTitleRate = _dataServiceFacade.RatingDs.GetRating(userId);
            if (userTitleRate.Count==0  ) // JSON is returned, elements number
            {
                var response = " Rating not found";
                return NotFound(response);
            }
            return Ok(userTitleRate);
        }
        
        [HttpPost]
        public IActionResult CreateRating(UserTitleRateForCreationDto userTitleRateForCreationDto)
        {
            var userTitleRate = _mapper.Map<UserTitleRate>(userTitleRateForCreationDto);

            if (userTitleRate.TitleIndividRating > 10 || userTitleRate.TitleIndividRating <= 0)
            {
               return BadRequest("invalid rating");
            }
            return Ok(_dataServiceFacade.RatingDs.CreateRating(userTitleRate));
        }

     
    }
}