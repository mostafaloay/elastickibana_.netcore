using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace elastic.Controllers
{
    public class ElasticRepo : IElastic
    {
        public ElasticClient elasticClients;
        public ElasticRepo()
        {
            //Connection string for Elasticsearch
            /*connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200/")); //local PC
            elasticClients = new ElasticClients(connectionSettings);*/

            //Multiple node for fail over (cluster addresses)
            var nodes = new Uri[]
            {
                new Uri("http://192.168.99.100:9200/"),
                //new Uri("Add server 2 address")   //Add cluster addresses here
                //new Uri("Add server 3 address")
            };

            var connectionPool = new StaticConnectionPool(nodes);
            var connectionSettings = new ConnectionSettings(connectionPool);
            elasticClients = new ElasticClient(connectionSettings);
        }
        public void Dispose()
        {

        }

        public IndexResponse Add()
        {
            var user = new WeatherForecastController.User
            {
                ItemId = Guid.NewGuid(),
                Age = new Random().Next(0, 100),
                Name = "jasim loay"
            };
            var response =  elasticClients.Index(user,
                i => i.Index("users").Refresh(Refresh.True)
                    .Id(new Id(Guid.NewGuid().ToString()))); 
            return response;
        }

        public List<WeatherForecastController.User> Search()
        {
            return elasticClients.Search<WeatherForecastController.User>(s => s
                .Index("users")
                .Query(q => q.Term(x => x.Field("age").Value(3)))).Documents.ToList();
        }

        public UpdateResponse<WeatherForecastController.User> Update()
        {
            var response = elasticClients.Update<WeatherForecastController.User, WeatherForecastController.User>("4df1cffc-2e9c-418d-8ad8-ca649bbe008b", d => d
                .Index("users")
                .Doc(new WeatherForecastController.User
                {
                    ItemId = Guid.Parse("4df1cffc-2e9c-418d-8ad8-ca649bbe008b"),
                    Age = 4444
                }));
            //var responsee = elasticClients.Index(myJson, i => i
            //    .Index("users")
            //    .ItemId(Guid.Parse(""))
            //    .Refresh(Refresh.True));
            return response;
        }

        public DeleteResponse Delete()
        {
            var response = elasticClients.Delete<WeatherForecastController.User>(
                "4df1cffc-2e9c-418d-8ad8-ca649bbe008b",
                d => d.Index("users"));
            return response;
        }
    }
}
