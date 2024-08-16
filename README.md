# Tiny Url Service
Application to serve as a proof of concept for a mock TinyURL style service
## Requirements
	.NET Framework 8.0
	Visual Studio 2022
## Steps to run the console application
### Clone the solution from repo.
```bash
git clone https://github.com/shekhar249/TinyUrlService.git	
```
### Restore nuget packages for solution
	Restore nuget packages - visual command
### Set console application Adroit.Services.TinyUrl.Console as default project
	Set as startup projct - visual command
### How to run the application
	- Press F5 to run the application with command line options 
	- Run tests from project Adroit.Services.TinyUrl.Tests to review app functionality with unit tests
 
# Assumptions
  - There are 200 million new URL shortening requests per month.
  - Each entry will have a maximum of five years of expiry time, unless explicitly deleted.
  - So for 5 years there will be approximately 12 billion url shortening requests  = 200 million x 12 months x 5  years 
  - So with base 36 encoder used in POC and short url length of 7 we should be able to generate approximately 78 billion short urls. This range should be sufficient for our requirement.
# Out of scope functionalities
  - Update: Users should be able to update the long URL associated with the short link, given the proper rights.
  - Expiry time: There must be a default expiration time for the short links, but users should be able to set the expiration time based on their requirements.
	
# Future enhancements to productionize the code
 - Validation rules for long URL's and manual short URL's
 - Precompute  and cache the keys in advance for better performance when request volume is high and possibility of key collision is high.
 - Purge old Urls and releave the keys for reuse
 

 
