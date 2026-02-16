
using MyFirstAiProject;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.user.json", optional: true, reloadOnChange: true)
    .Build();


var githubInference = new GithubInference(config);
githubInference.RunAi();