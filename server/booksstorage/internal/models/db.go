package models

import (
	"errors"
	"os"

	"go.mongodb.org/mongo-driver/mongo/options"
)

type DbCred struct {
	Uri         string
	Collection  string
	Database    string
	Credentials options.Credential
}

func validateEnvVars(vars map[string]*string) []string {
	var errs []string
	for k, v := range vars {
		if *v == "" {
			errs = append(errs, k)
		}
	}
	return errs
}

func MakeDbCred() (DbCred, error) {
	dbCred := DbCred{
		Uri:        os.Getenv("MONGODB_URI"),
		Collection: os.Getenv("MONGODB_COLLECTION"),
		Database:   os.Getenv("MONGODB_DATABASE"),
		Credentials: options.Credential{
			Username: os.Getenv("MONGODB_USERNAME"),
			Password: os.Getenv("MONGODB_PASSWORD"),
		},
	}

	vars := map[string]*string{
		"MONGODB_URI":        &dbCred.Uri,
		"MONGODB_COLLECTION": &dbCred.Collection,
		"MONGODB_DATABASE":   &dbCred.Database,
		"MONGODB_USERNAME":   &dbCred.Credentials.Username,
		"MONGODB_PASSWORD":   &dbCred.Credentials.Password,
	}

	errs := validateEnvVars(vars)

	if len(errs) > 0 {
		errMsg := ""
		for _, err := range errs {
			errMsg += "One or more env variable could not be loaded. Please check the env keys on your .env or the K8s env manifests. Env variables that should be validated:\n" + err
		}
		err := errors.New(errMsg)
		return DbCred{}, err
	}

	return dbCred, nil
}
