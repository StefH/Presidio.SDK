rem This is PII
docker run --rm -it -p 5005:5000 --memory 8g --cpus 1 mcr.microsoft.com/azure-cognitive-services/textanalytics/ner:latest Eula=accept Billing=%LANGUAGE_URL% ApiKey=%LANGUAGE_KEY%