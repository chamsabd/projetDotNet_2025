dans projet sol 
docker-compose down
# redémarrer les services. sans reconstruire les image 
docker-compose up -d
# démarre les conteneurs, mais avant de les démarrer, elle construit les images
docker-compose up --build

docker-compose build --no-cache
# Arrête et supprime tous les containers + volumes
docker-compose down -v --remove-orphans
docker-compose up --build --watch

dans projet 
dotnet ef migrations remove

 dotnet ef migrations add Initial

 dotnet ef database update

dotnet add package BCrypt.Net-Next
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.1


