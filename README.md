# eCharge

eCharge is a sample application to evaluate some libraries and techniques. 
Main focus was building a microservice environment based on .Net Core 
with EventFlow as the core for event sourcing and CQRS and MithrilJS as the frontend library
for a single page application.

## The idea behind

Target of this application is following scenario:

**Users** have electric **cars**. They travel around the world and - as it is the point with 
electric cars - have to charge them now and then. They can charge their cars at **locations** owned by other
users inviting them to consume **charges** at their location.

To be more precise:

### Locations

* Locations are owned by users
* Locations have a name
* Locations have an address
* Locations have a latitude
* Locations have a longitude
* Locations have a price per kw
* Locations have allowed consumers (users) assigned
* Location owners can invite users by their email address

### Cars

* Cars are owned by users
* Cars have a name
* Cars have a maximum battery capacity in kw

### Charges

* Charges are done by a user
* Charges are done at a location for a certain price per kw (keep in mind: location owners can change the price)
* Charges are done for a car with a certain battery capacity at this time (keep in mind: batteries can be replaced with better ones)
* Charges are done at a date and time
* Charges are starting at a percentage
* Charges are stopped at a percentage
* Charges can be cleared

## Stuff used

* EventFlow (obviously)
* AutoMapper (all the way through... I simply cannot live without this library)
* AutoFac (same as with AutoMapper)
* Entity Framework Core (including how to use migrations alongside with EventFlow) with PostgreSQL
* FluentValidation
* MithrilJS

## How to get things running

First of all - there isn't ONE app to run but four of them. The solution exists of four runnable applications:

* The frontend (as a MithrilJS single page application)
* A microservice for locations
* A microservice for cars
* A microservice for charges

All of them have to run at once. So - if you run it locally without any custom hostnames - remind that it all runs as localhost with different ports.

### Preparation

1. Ensure to have an account at [Auth0](https://auth0.com/) . Create an application there or use one of your existing ones.
2. Ensure to have an Account at [SendGrid](https://sendgrid.com) .
3. Ensure to have a PostgreSQL server up and running.
    1. Create a database for "locations" microservice
    2. Create a database for "cars" microservice
    3. Create a database for "charges" microservice
4. `cd` into the directory `src/eCharge.Public.Frontend` and run `npm install`
5. Modify the app settings according to your needs by either directly editing `appsettings.json` or creating your own `appsettings.Development.json` (given, your dotnet environment is named "Development")
    1. `src/eCharge.Public.Frontend/appsettings.json`
        * "Application:Frontend": the full qualified uri to your frontend (e.g. "http://localhost:5000")
        * "Application:Api:Locations": the full qualified uri to the "locations" microservice (e.g. "http://localhost:5001")
        * "Application:Api:Cars": the full qualified uri to the "cars" microservice (e.g. "http://localhost:5002")
        * "Application:Api:Charges": the full qualified uri to the "charges" microservice (e.g. "http://localhost:5003")
    2. `src/eCharge.Services.Locations.Public.Api/appsettings.json`, `src/eCharge.Services.Cars.Public.Api/appsettings.json` and `src/eCharge.Services.Charges.Public.Api/appsettings.json`
        * "SendGrid:ApiKey": your api key at [SendGrid](https://sendgrid.com)
        * "SendGrid:SenderAddress": email address to use as sender
        * "SendGrid:SenderName": display name to use as sender
        * "Database:ConnectionStrings:Locations": database connection string to use with EF Core and PostgreSQL
        * "Auth0:Authority": Authority setting for Auth0 authentication
        * "Auth0:Audience": Audience setting for Auth0 authentication
        * "Application:Frontend": the full qualified uri to your frontend (e.g. "http://localhost:5000")
        * "Application:Api:Locations": the full qualified uri to the "locations" microservice (e.g. "http://localhost:5001")
        * "Application:Api:Cars": the full qualified uri to the "cars" microservice (e.g. "http://localhost:5002")
        * "Application:Api:Charges": the full qualified uri to the "charges" microservice (e.g. "http://localhost:5003")
6. `cd` into the root directory where the .sln file is located and create the database tables:
    1. `dotnet ef database update --startup-project ./src/eCharge.Services.Locations.Public.Api --project ./src/eCharge.Services.Locations.Domain`
    2. `dotnet ef database update --startup-project ./src/eCharge.Services.Cars.Public.Api --project ./src/eCharge.Services.Cars.Domain`
    3. `dotnet ef database update --startup-project ./src/eCharge.Services.Charges.Public.Api --project ./src/eCharge.Services.Charges.Domain`
        
### Run

Depending on your environment either start the apps with `dotnet run` or some direct debug option from within your IDE. Make sure that the apps are started with the urls as you have set them in your app settings.

## Some final words

The code is not guaranteed to be perfect or even close to this. I do not provide any warranty or such.
Building this stuff helped me to understand the two main libraries to evaluate - named MithrilJS and EventFlow.
The idea is that it might help others as well.

Splitting cars, locations and charges into separate microservices might appear overcomplicated and unneccessary - **I know**.
The reason behind this decision is a simple one - I also had been evaluating the usage of kubernetes as the hosting environment for multiple microservices.

In case of questions or feedback: head over to the [EventFlow Gitter Channel](https://gitter.im/eventflow/EventFlow). Please note, that it might take me a while to respond - well, I got a job on my own as everyone else :-)
