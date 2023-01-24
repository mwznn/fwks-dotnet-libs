pack:
	rm artifacts -rdf
	dotnet restore
	dotnet build -c Release

	for project in src/**/*.csproj; do\
		dotnet pack $${project} --no-restore --no-build -c Release -o artifacts/ --version-suffix nuget-test1 -p:Version=0.0.$$(date +"%y%j") ;\
	done