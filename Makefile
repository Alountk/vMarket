.PHONY: docker-up run-api docker-down

docker-up:
	docker-compose up -d

run-api:
	cd Videogames.API && dotnet run

docker-down:
	docker-compose down
