CONFIG=$1

if [ -x $CONFIG ]; then
  echo 'Missing parameter $CONFIG'
  exit 1
fi

dotnet restore $(dirname $0)/../src/Hero6.sln -c $CONFIG

if [[ "$OSTYPE" == "cygwin" || "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
  dotnet build $(dirname $0)/../src/Hero6.MonoGamePipeline/Hero6.MonoGamePipeline.csproj -c $CONFIG
else
  # msbuild $(dirname $0)/../src/Hero6.MonoGamePipeline.sln /t:restore /property:Configuration=$CONFIG
  msbuild $(dirname $0)/../src/Hero6.MonoGamePipeline/Hero6.MonoGamePipeline.csproj /property:Configuration=$CONFIG
fi
