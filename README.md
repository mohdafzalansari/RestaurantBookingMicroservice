# RestaurantBookingMicroservice
Created Restaurant Booking System using Microservice, Asp.Net Core, CQRS, DDD, Docker, Identity-Server4, Multi-Tenant and much more.

# Tools and Technologies
1. Asp.Net Core 3.1
2. MongoDb
3. DDD
4. Docker
5. CQRS
6. MediaterR
8. Swagger Document
9. Authentication and Authorization using Identity-Server4 and Policy based authorization
10. Multi-Tenant(Shared-Database)


# Microservice
1. ApiGetway:Used to redirect to actual services and work as mediator b/w UI and APIs
2. Restaurant: Supper admin can created multiple tennant(restaurant)
3. Reservation: restaurant(admin) can manage and create tables and booking table
4. Identity: authentication server

# How to run
1. verify docker is running [docker ps]
1. pull mongodb image [docker pull mongo]
3. start the mongoDb [docker run -d -p 27017:27017 --name restaurant-mongo mongo]


# How to Generate token
After start the mondoDb inside docker and applications(identity, restaurant, reservation,apigetway), Identity server will create default 3 user
- user name: admin1@gmail.com, password:123, role:admin
- user name: user1@gmail.com, password:123, role:user
- user name: customer@gmail.com, password:123, role:customer
![alt text](https://raw.githubusercontent.com/mohdafzalansari/RestaurantBookingMicroservice/main/Images/GenerateToken.png)

# All JWT details like aud, claims
![alt text](https://github.com/mohdafzalansari/RestaurantBookingMicroservice/blob/main/Images/Jwt.png)

# Apis Swagger
![alt text](https://raw.githubusercontent.com/mohdafzalansari/RestaurantBookingMicroservice/main/Images/Swagger.png)

