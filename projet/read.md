dans projet sol 
docker-compose down
# redémarrer les services. sans reconstruire les image 
docker-compose up -d
# démarre les conteneurs, mais avant de les démarrer, elle construit les images
docker-compose up --build


dans projet 
dotnet ef migrations remove

 dotnet ef migrations add Initial

 dotnet ef database update
