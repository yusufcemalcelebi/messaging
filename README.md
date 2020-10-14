# Messaging Service

To start the application with the required dependencies

`docker-compose build`   
`docker-compose up`   

- This app consists of basic Messaging methods for a real-world scenario with the Authentication mechanisms.  
- JWT is used to implement Authentication and Authorization functionalities.
- .Net Core 3.1 Web Api is used with the layered architecture. EF Core is used to implement data access logic. 
- Code First Migration is used to create require tables in the SQL Server 2017. 
- Application is dockerized, docker-compose can be used to start the application and require database on docker.

## Open Endpoints

Open endpoints require no Authentication.

* [Login](docs/authentication/login.md) : `POST /api/v1/Authentication/Login`
* [Register](docs/authentication/register.md) : `POST /api/v1/Authentication/Register`
* HealthCheck : `GET /healthcheck`

## Endpoints that require Authentication

Closed endpoints require a valid Token in the Authorization header of the
request. A Token can be acquired from the Login or Register endpoints

### Message Endpoints

Message Controller includes the API's to get messages and to send message. 

* [Get Messages](docs/messaging/get.md) : `GET /api/v1/Messaging`
* [Send Message](docs/messaging/post.md) : `POST /api/v1/Messaging`

### Blocking

Blocking API's can be used to block and to unblock the target users. 

* [Create or Update Block](docs/blocking/post.md) : `POST /api/v1/Blocking`   



## TODOs that will improve the system  
- [ ] In memory caching mechanism for the blocked users
- [ ] Repository pattern for the data access layer to provide Seperation Of Concern. This can be great for unit tests, in this codebase we need to mock the DBContext at business layer.
- [ ] Split the MapperContainer for each of the required layer to manage dependencies
- [ ] Additional unit tests

