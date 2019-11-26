using MongoDB.Driver;
using SharedLibrary.Entities.Custom;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static SharedLibrary.Entities.Custom.ExtrasDaEscola;

namespace SharedLibrary.Context.Custom
{
	public class ExtrasDaEscolaContext
	{
		private readonly IMongoCollection<ExtrasDaEscola> _extras;

		public ExtrasDaEscolaContext(IMongoDBSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_extras = database.GetCollection<ExtrasDaEscola>(settings.ExtrasCollectionName);
		}

		public List<ExtrasDaEscola> Get() =>
			_extras.Find(extra => true).ToList();
		public ExtrasDaEscola Get(Indexer id)
		{
			Expression<Func<ExtrasDaEscola, bool>> filter = 
				extra => extra.ID.Cod_Entidade.Equals(id.Cod_Entidade) &&
				extra.ID.Ano.Equals(id.Ano);
			return _extras.Find(filter).FirstOrDefault();
		}
		public void Upsert(Indexer iD, ExtrasDaEscola extraNovo) =>
			_extras.ReplaceOne(extra => extra.ID == extraNovo.ID, extraNovo, new UpdateOptions { IsUpsert = true });
		public void Bulk(List<ExtrasDaEscola> extras)
		{
			foreach (var extra in extras)
			{
				Upsert(extra.ID, extra);
			}
		}
		public void Remove(ExtrasDaEscola extraIn) =>
			_extras.DeleteOne(extra => extra.ID == extraIn.ID);
		public void Remove(Indexer extraId) =>
			_extras.DeleteOne(extra => extra.ID == extraId);
	}
}
