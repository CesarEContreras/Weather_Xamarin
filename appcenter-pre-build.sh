PROJECT_NAME=Weather
CONFIG_FILE="$APPCENTER_SOURCE_DIRECTORY/$PROJECT_NAME/Helpers/settings.json"

echo "Injecting secrets..."
echo "Updating iOS secret"
echo $SECRETS | base64 --decode > $CONFIG_FILE
echo "Finished injecting secrets."