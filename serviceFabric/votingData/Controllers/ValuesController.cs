using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;

namespace votingData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IReliableStateManager stateManeger;
        public ValuesController(IReliableStateManager stateManeger)
        {
            this.stateManeger = stateManeger;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "naresh", "Swathi" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{name}")]
        public async Task<IActionResult> Put( string name)
        {
            IReliableDictionary<string, int> votesDictionary =
                await this.stateManeger.GetOrAddAsync<IReliableDictionary<string, int>>("VotesDictionary");
            using (ITransaction tx = this.stateManeger.CreateTransaction())
            {
                await votesDictionary.AddOrUpdateAsync(tx, name, 1, (key, oldValue) => oldValue + 1);
            }
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
