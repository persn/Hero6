CONFIG=$1

if [ -x $CONFIG ]; then
  echo 'Missing parameter $CONFIG'
  exit 1
fi

if [[ "$OSTYPE" == "cygwin" || "$OSTYPE" == "msys" || "$OSTYPE" == "win32" ]]; then
  dotnet build $(dirname $0)/../src/Hero6.MonoGamePipeline.sln -c $CONFIG
else
  # xbuild $(dirname $0)/../src/Hero6.MonoGamePipeline.sln /t:restore /property:Configuration=$CONFIG
  xbuild $(dirname $0)/../src/Hero6.MonoGamePipeline.sln /property:Configuration=$CONFIG
fi
