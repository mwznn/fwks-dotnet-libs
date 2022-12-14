libs-pack:
	rm artifacts -rdf
	dotnet restore
	dotnet build -c Release

	for project in libs/**/*.csproj; do\
		dotnet pack $${project} --no-restore --no-build -c Release -o artifacts/ --version-suffix alpha -p:Version=0.0.$$(date +"%y%j") ;\
	done