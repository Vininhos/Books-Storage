package main

import (
	"booksstorage/internal/db"
	"booksstorage/internal/models"
	"booksstorage/internal/routes"
	"context"
	"log"
	"log/slog"
	"net/http"
	"os"
	"time"

	"github.com/joho/godotenv"
)

const defaultTimeout = time.Minute

func init() {
	logger := slog.New(slog.NewJSONHandler(os.Stdout, nil))
	slog.SetDefault(logger)
}

func main() {
	ctx, cancel := context.WithTimeout(context.Background(), defaultTimeout)
	defer cancel()
	if err := godotenv.Load(); err != nil {
		slog.Info("No .env file found")
	}

	dbCred, err := models.MakeDbCred()
	if err != nil {
		panic(err)
	}

	db, err := db.ConnectToDB(dbCred, ctx)
	if err != nil {
		panic(err)
	}
	defer db.DisconnectFromDB(ctx)

	slog.Info("Connected to DB!")

	r := routes.Routes(db)
	slog.Info("Listening to port 8080...")
	log.Fatal(http.ListenAndServe(":8080", r))

	slog.Info("Stopping application and disconnecting from DB!")
}
