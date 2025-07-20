# Build notes/tradeoffs
- I decided to remove the Swagger stuff because this was a really simple project and it was going to be running in a container anyways
- I decided to have the database container initialize its own database instead of relying on a second container for simplicity

# Build Instructions
- Run project: `docker-compose up`
	- Build and run project: `docker-compose up --build`
	- You can tack a `-d` onto the end to run the container in the background ("Detached mode")
	- Run just DB: `docker-compose up db`
	- BUILD just API: `docker-compose build api`
		- You can't run just the API since it depends on the db
- Shut down both containers: `docker-compose stop`
- Remove containers with `docker-compose down`
- Connect to running container: `docker exec -it <container_name_or_id> /bin/bash`

# Interacting with the service
### API
- GET /todos: `curl -X GET http://localhost:8080/todos`
- POST /todos:
```
curl -X POST http://localhost:8080/todos \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Test API with cURL",
    "description": "Testing the POST endpoint from command line",
    "isCompleted": false,
    "dueDate": "2025-07-25T15:30:00"
  }'
```
- GET /todos/{id}: `curl -X GET http://localhost:8080/todos/4`
- PUT /todos/{id}: 
```
curl -X PUT http://localhost:8080/todos/4 \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Test API with cURL",
    "description": "Now testing PUT /todos/{id}",
    "isCompleted": false,
    "dueDate": ""
  }'
```
- DELETE /todos/{id}: `curl -X DELETE http://localhost:8080/todos/4`
### DB
	- Connect directly to database: `sqlcmd -S localhost:1433 -U sa -P P@ssword1`
	- Check database contents: `sqlcmd -S localhost:1433 -U sa -P P@ssword1 -C -Q "USE TodoDb; SELECT * FROM Todos;"`
- Connecting to docker instances: `docker exec -it <container_name> bash`
