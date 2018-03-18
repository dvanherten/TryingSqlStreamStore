using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlStreamStore;
using SqlStreamStore.Streams;
using SqlStreamStoreTests.Sql.Infrastructure;
using SqlStreamStoreTests.Sql.Models;

namespace SqlStreamStoreTests.Sql.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly IStreamStore _store;
        private readonly ReadContext _context;

        public EventController(IStreamStore store, ReadContext context)
        {
            _store = store;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ReadModel> Get()
        {
            return _context.ReadModels.ToList();
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] Command value)
        {
            var streamId = $"streamId-{value.Id}";
            _store.AppendToStream(streamId, ExpectedVersion.Any, new NewStreamMessage(Guid.NewGuid(), "NoType", JsonConvert.SerializeObject(value)));
        }
    }

    public class Command
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}