# shorturl
A Url Shortening Service.

This app is able to both long urls as input and shorten them as well as to inflate a shortened url to original form.

# Steps
Run update-database command under Ehasan.ShortUrl.DataRepositories project.

# Input/Output
  For valid long url app will provide short url. 
  For Valid short url app will provide long url. App treat url as short, if it is created by itself.
  
 For example, if service take https://www.linkedin.com/in/ehasanulhoque/ as input, after processing it will return https://shortenurl.com/12415186-25ee-4404-9925-2e283a2ed424 as output.
 
 
 # Dotnet CLI
 
 Dotnet ClI works on all .NET Core-supported platforms and doesn't require Visual Studio to restore package, build, test and build.
 
 To restore package
 
 <code>dotnet restore</code>
 
 Buiding application 
 
 <code>dotnet build</code>
 
 To run test 
 
 <code>dotnet test</code>
 
 Publishing into filesystem
 
 <code>dotnet publish -c Release -o C:\MyWebs\test </code>
 
  Publishing into network drive
 
 <code>dotnet publish -c Release /p:PublishDir=//r8/release/AdminWeb </code>
 
 publishing using profile
 
 <code>dotnet publish  /p:PublishProfile="src\Ehasan.ShortUrl.API\Properties\PublishProfiles\devprprofile.pubxml" </code>
  

  

