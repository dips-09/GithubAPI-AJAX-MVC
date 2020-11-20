using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Homework7.Models
{
    public class GithubRepo
    {
        public string Source { get; set; }
        public GithubRepo(string endpoint)
        {
			Source = endpoint;
        }

		public UserInfo GetUserInfo()
        {
			string jsonResponse = SendRequest(Source, "wou-cs");
			var obj = JsonConvert.DeserializeObject<UserInfo>(jsonResponse);
			return obj;
        }

		public List<Commit> GetCommits(string user)
        {
			string jsonResponse = SendRequest(Source, user);
			var obj = JsonConvert.DeserializeObject<List<Commit>>(jsonResponse);
			return obj;
		}

		public IEnumerable<UserRepository> GetAllRepositories()
        {
			string jsonResponse = SendRequest(Source, "wou-cs");
			Debug.WriteLine(jsonResponse);
			var obj = JsonConvert.DeserializeObject<List<Repository>>(jsonResponse);
			int count = (int)obj.Count;
			List<UserRepository> outRepo = new List<UserRepository>();
			for(int i = 0;i < count;i++)
            {
				string name = (string)obj[i].name;
				string cUrl = (string)obj[i].commits_url;
				string lastUpdate = (string)obj[i].updated_at;
				string created = (string)obj[i].created_at;
				DateTime date = DateTime.Parse(lastUpdate);
				var datediff = DateTime.Now - date;

				outRepo.Add(new UserRepository()
				{
					RepoName = name,
					CommitUrl = cUrl,
					LastUpdated = lastUpdate,
					CreateDate = created,
					LastUpdatedDays = datediff.Days
				});
            }
			return outRepo;
        }
		private static string SendRequest(string uri, string username)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			//request.Headers.Add("Authorization", "token " + credentials);
			request.UserAgent = username;       // Required, see: https://developer.github.com/v3/#user-agent-required
			request.Accept = "application/json";

			string jsonString = null;
			// TODO: You should handle exceptions here
			using (WebResponse response = request.GetResponse())
			{
				Stream stream = response.GetResponseStream();
				StreamReader reader = new StreamReader(stream);
				jsonString = reader.ReadToEnd();
				reader.Close();
				stream.Close();
			}
			return jsonString;
		}
	}
}
