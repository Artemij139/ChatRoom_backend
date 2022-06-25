# ChatRoom_backend
This is a real-time chat application. It consists of two projects: authorization service (based on <b>identity server 4</b>) and web-api (based on <b>signalR</b>).
IS 4 uses Authorization code flow under OpenID Connect. Access Token Lifetime is half-hour. Refresh Tookken is used to update the access token.
Chat API  uses Entity framework to Database connection. In database store users and their messages.
For Database use sql docker conteiner (https://hub.docker.com/_/microsoft-mssql-server).


Client part placed in the repository "ChatRoom_frontend" <b>expected that client app will be launched at  http://localhost:3000.</b>
To start this projects:
 - Run docker sql-server: docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyStrong@Passw0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
 - Using package manager in vs studio roll-up migrations for both projects(simply type "update-database").
 - Set multiple startup projects in solution and check both then start.

 
 
