ignore := 'test*|obj|bin'

# Usage samples:
# 
#   make clean
#   make build
#   make publish
#   make run
#   make all
#   make test
#   make otel

clean:
		@echo -e '\nCleaning .NET Core solution'
		@dotnet clean > /dev/null

build:
		@echo -e '\nBuilding .NET Core solution\n'
		@dotnet build --configuration Release

publish:
		@echo -e '\nPublishing .NET Core solution\n'
		@dotnet publish --configuration Release
		@tree . -I $(ignore)

run:
		@echo -e '\nRunning .NET Core solution\n'
		@dotnet run

otel:
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
