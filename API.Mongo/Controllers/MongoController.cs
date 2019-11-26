using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Context.Custom;
using SharedLibrary.Entities.Custom;
using System.Collections.Generic;
using static SharedLibrary.Entities.Custom.ExtrasDaEscola;

namespace API.Mongo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MongoController : ControllerBase
	{
		public readonly ExtrasDaEscolaContext _extrasContext;

		public MongoController(ExtrasDaEscolaContext context)
		{
			_extrasContext = context;
		}

		[HttpGet]
		public ActionResult<List<ExtrasDaEscola>> Get() =>
			_extrasContext.Get();

		[HttpGet("{ano}/{id}")]
		public ActionResult<ExtrasDaEscola> GetExtra(short ano, long id)
		{
			Indexer ind = new Indexer { Ano = ano, Cod_Entidade = id };
			var extra = _extrasContext.Get(ind);

			if (extra == null)
				return NotFound();

			return extra;
		}

		[HttpPost]
		public ActionResult PostExtra(ExtrasDaEscola extra)
		{
			_extrasContext.Upsert(extra.ID, extra);

			return Accepted();
		}

		[HttpPost("bulk")]
		public ActionResult PostExtra(List<ExtrasDaEscola> extras)
		{
			_extrasContext.Bulk(extras);

			return Accepted();
		}

		[HttpPut]
		public IActionResult PutExtra(ExtrasDaEscola extra)
		{
			var ex = _extrasContext.Get(extra.ID);

			if (ex == null)
			{
				return NotFound();
			}

			_extrasContext.Upsert(extra.ID, extra);

			return NoContent();
		}

		[HttpDelete("{ano}/{cod}")]
		public IActionResult DeleteExtra(short ano, long id)
		{
			Indexer ind = new Indexer { Ano = ano, Cod_Entidade = id };
			var extra = _extrasContext.Get(ind);

			if (extra == null)
				return NotFound();

			_extrasContext.Remove(extra.ID);

			return NoContent();
		}
	}
}
