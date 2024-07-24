package main

import (
	"booksstorage/internal/db"
	"context"
	"log/slog"
	"os"
	"time"

	"github.com/joho/godotenv"
	"github.com/lmittmann/tint"
)

const defaultTimeout = time.Minute

func main() {
	ctx, cancel := context.WithTimeout(context.Background(), defaultTimeout)
	defer cancel()

	logger := slog.New(tint.NewHandler(os.Stdout, nil))

	slog.SetDefault(logger)

	if err := godotenv.Load(); err != nil {
		logger.Info("No .env file found")
	}

	if err := db.ConnectToDB(ctx); err != nil {
		logger.Error("Error while connecting to DB:", slog.String("Error", err.Error()))
	}

	slog.Info("Connected to DB!")

	if err := db.DisconnectFromDB(ctx); err != nil {
		logger.Error("Error while disconnecting from DB:", slog.String("Error", err.Error()))
	}

	slog.Info("Disconnected from DB!")
}
