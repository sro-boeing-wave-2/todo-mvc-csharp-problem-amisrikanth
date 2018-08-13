using Xunit;
using TodoLists.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using TodoLists;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WebApiTesting
{
    public class ApiIntegrationTest
    {
        private HttpClient _client;

        public ApiIntegrationTest()
        {
            var webHost = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>());
            _client = webHost.CreateClient();

        }

        readonly List<Note> note = new List<Note>() {
               new Note
           {
               Title = "first",
               Message = "Ft",
               Id = 1,
               Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
               CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
               Pinned = true
           },
               new Note
           {
               Title = "second",
               Message = "sec",
               Id = 2,
               Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
               CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
               Pinned = false
           }
           };

        public Note note1 = new Note
        {
            Title = "test",
            Id = 1,
            Message = "test1",
            Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
            CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
            Pinned = true
        };
        public Note putNote = new Note
        {
            Title = "Put_test",
            Id = 1,
            Message = "test1",
            Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
            CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
            Pinned = true
        };

        public Note note2 = new Note
        {
            Title = "second",
            Message = "sec",
            Id = 2,
            Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
            CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
            Pinned = false
        };

        List<Note> noteList = new List<Note>() {new Note
           {
            Title = "test",
            Id = 1,
            Message = "test1",
               Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
               CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
               Pinned = true
           } };

        [Fact]
        public async Task TestGetRequestAsync()
        {
            var response = await _client.GetAsync("/api/notes");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task TestGetById()
        {
            var response = await _client.GetAsync("/api/notes/1024");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task TestPostAsync()
        {
            var response = await _client.PostAsync("/api/notes", new StringContent(JsonConvert.SerializeObject(note1), UnicodeEncoding.UTF8, "application/json"));
            var getResponse = await _client.GetAsync("/api/notes?id=1");
            var content = await getResponse.Content.ReadAsStringAsync();
            var jObject = JArray.Parse(content);
            jObject[0]["title"].ToString().Should().Be("test");
        }

        [Fact]
        public async Task TestPutAsync()
        {
            var response = await _client.PostAsync("/api/notes", new StringContent(JsonConvert.SerializeObject(note1), UnicodeEncoding.UTF8, "application/json"));
            var content = new StringContent(JsonConvert.SerializeObject(putNote), UnicodeEncoding.UTF8, "application/json");
            await _client.PutAsync("/api/notes/1", content);
            var getResponseAfterPut = await _client.GetAsync("/api/notes?id=1");
            var contents = await getResponseAfterPut.Content.ReadAsStringAsync();
            var jObject = JArray.Parse(contents);
            jObject[0]["title"].ToString().Should().Be("Put_test");

        }

        [Fact]
        public async Task TestDeleteAsync()
        {
            await _client.PostAsync("/api/notes", new StringContent(JsonConvert.SerializeObject(note1), UnicodeEncoding.UTF8, "application/json"));
            await _client.PostAsync("/api/notes", new StringContent(JsonConvert.SerializeObject(note2), UnicodeEncoding.UTF8, "application/json"));
            var response = await _client.DeleteAsync("/api/notes/1");
            var getResponseAfterDelete = await _client.GetAsync("/api/notes?id=1");
            var contents = await response.Content.ReadAsStringAsync();
            var jObject = JArray.Parse(contents);
            jObject[0]["title"].ToString().Should().Be("test");
        }

    }
}
