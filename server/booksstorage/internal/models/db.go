package models

import (
	"errors"
	"os"

	"go.mongodb.org/mongo-driver/mongo/options"
)

type DbCred struct {
	Uri         string
	Collection  string
	Credentials options.Credential
}

func MakeDbCred() (DbCred, error) {
	var dbCred DbCred

	dbCred.Uri = os.Getenv("MONGODB_URI")

	if dbCred.Uri == "" {
		return DbCred{}, errors.New("URI environment could not be loaded. Please check the environment keys on your .env or the K8s env manifests")
	}

	dbCred.Collection = os.Getenv("MONGODB_COLLECTION")

	if dbCred.Collection == "" {
		return DbCred{}, errors.New("Collection environment could not be loaded. Please check the environment keys on your .env or the K8s env manifests")
	}

	dbCred.Credentials = options.Credential{
		Username: os.Getenv("MONGODB_USERNAME"),
		Password: os.Getenv("MONGODB_PASSWORD"),
	}

	if dbCred.Credentials.Username == "" || dbCred.Credentials.Password == "" {
		return DbCred{}, errors.New("URI environment could not be loaded. Please check the environment keys on your .env or the K8s env manifests")
	}

	return dbCred, nil
}
