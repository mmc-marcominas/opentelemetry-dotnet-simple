ignore := 'test*|obj|bin'

# Usage samples:
# 
#   make clean
#   make build
#   make publish
#   make run
#   make all
#   make start

clean:
		@dotnet clean

build:
		@dotnet build --configuration Release

publish:
		@dotnet publish --configuration Release
		@tree . -I $(ignore)

run:
		@dotnet run

run-otel:
		@./otel-dotnet-run.sh

test:
		@curl localhost:8087/rolldice
		@sleep 3
		@curl localhost:8087/rolldice/MarcoMinas
		@sleep 4
		@curl localhost:8087/rolldice/TerezaLeia
		@sleep 7
		@curl localhost:8087/rolldice/Gata+Bella

all: clean build publish run
