using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OAuth2.Web.API.Controllers
{

    [ApiController]
    public class LocationController : ControllerBase
    {

        public LocationController()
        {


        }


        [ActionName("ZipCodeLookup")]
        [HttpGet("api/[controller]/[action]/{zip_code}")]
        public async Task<ActionResult> ZipCodeLookup(string zip_code)
        {

            string locationJSON;
            HttpClient httpClient = new HttpClient();

            try
            {

                if (zip_code != string.Empty)
                {

                    locationJSON = await httpClient.GetStringAsync($"https://api.zippopotam.us/us/{zip_code}");

                    return Ok(locationJSON);

                }
                else
                {

                    return BadRequest();

                }

            }

            catch (Exception ex)
            {

                return StatusCode(500, new { message = "SecuredZipCodeLookup Error: " + ex.Message });

            }
        }
    }

       
    }
