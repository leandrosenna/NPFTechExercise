using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPFTechExercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DivisionController : ControllerBase
    {  
        [HttpPost("Method1")]
        public ActionResult<List<DivisibleBy11>> Method1([FromBody] Numbers listOfNumbers)
        {
            try
            {
                var listOfDivisibleBy11 = new List<DivisibleBy11>();

                foreach (var number in listOfNumbers.numbers)
                {
                    var newItem = new DivisibleBy11()
                    {
                        number = number
                    };

                    if (number % 11 == 0)
                    {
                        newItem.isMultiple = true;
                        listOfDivisibleBy11.Add(newItem);
                    }
                    else
                    {
                        newItem.isMultiple = false;
                        listOfDivisibleBy11.Add(newItem);
                    }
                }

                return Ok(listOfDivisibleBy11);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Method2")]
        public ActionResult<List<DivisibleBy11>> Method2([FromBody] Numbers listOfNumbers)
        {
            try
            {
                var listOfDivisibleBy11 = new List<DivisibleBy11>();

                foreach (var number in listOfNumbers.numbers)
                {
                    var charArray = number.ToString().ToCharArray();
                    var groups = charArray.Select((item, index) => new { Item = item, Index = index })
                             .GroupBy(x => x.Index % 2 == 0)
                             .ToDictionary(g => g.Key, g => g);

                    var oddsSum = groups[false].Sum(x => Convert.ToInt32(Char.GetNumericValue(x.Item)));
                    var evensSum = groups[true].Sum(x => Convert.ToInt32(Char.GetNumericValue(x.Item)));

                    var subtraction = evensSum - oddsSum;

                    var newItem = new DivisibleBy11()
                    {
                        number = number
                    };

                    if (subtraction % 11 == 0)
                    {
                        newItem.isMultiple = true;
                        listOfDivisibleBy11.Add(newItem);
                    }
                    else
                    {
                        newItem.isMultiple = false;
                        listOfDivisibleBy11.Add(newItem);
                    }
                }

                return Ok(listOfDivisibleBy11);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
