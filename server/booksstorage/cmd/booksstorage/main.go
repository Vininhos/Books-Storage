package main

import (
	"booksstorage/internal/db"
	"booksstorage/internal/logger"
	"booksstorage/internal/models"
	"booksstorage/internal/routes"
	"context"
	"log"
	"log/slog"
	"net/http"
	"time"

	"github.com/joho/godotenv"
)

const defaultTimeout = time.Minute

func main() {
	ctx, cancel := context.WithTimeout(context.Background(), defaultTimeout)
	defer cancel()

	logger := logger.GetLogger()
	if err := godotenv.Load(); err != nil {
		logger.Info("No .env file found")
	}

	dbCred, err := models.MakeDbCred()
	if err != nil {
		panic(err)
	}

	db, err := db.ConnectToDB(dbCred, ctx)
	if err != nil {
		log.Fatalf("Error while connecting to DB: %s\n", err.Error())
	}
	defer db.DisconnectFromDB(ctx)

	slog.Info("Connected to DB!")

	r := routes.Routes(db, logger)
	slog.Info("Listening to port 8080...")
	log.Fatal(http.ListenAndServe(":8080", r))

	slog.Info("Disconnected from DB!")
}
